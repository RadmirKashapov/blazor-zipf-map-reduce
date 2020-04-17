using ILGPU;
using ILGPU.IR;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ZipfMapReduce.Shared.Services
{
    public struct KeyValuePair
    {
        public ArrayView<Char> Key;
        public int Value;
    };
    public struct KeyValue
    {
        public string Key;
        public int Value;
    };
    public struct Input
    {
        public ArrayView<Char> Key;
    };
    public class MapReduceILGPU
    {
        private static readonly char[] separators = { ' ' };
        private static int StrCmp(ArrayView<char> str1, ArrayView<char> str2)
        {
            if (str1.Length <= str2.Length)
                for (int i=0; i<str1.Length; i++)
                {
                    if (str1[i] == str2[i])
                        continue;
                    else
                    {
                        if (str1[i] < str2[i])
                            return -1;
                        else return 1;
                    }
                }
            else
            {
                for (int i = 0; i < str2.Length; i++)
                {
                    if (str1[i] == str2[i])
                        continue;
                    else
                    {
                        if (str1[i] < str2[i])
                            return -1;
                        else return 1;
                    }
                }
                return 1;
            }
            return 0;
        }

        static void MapKernel(Index1 index, ArrayView<Input> input, ArrayView<KeyValuePair> pairs, int NUM_KEYS)
        {
            for (var i = Group.IdxX * Group.DimX + index.X;i < input.Length; i += Group.DimX * Grid.DimX)
            {
                Map(input[i], pairs[i * NUM_KEYS]);
            }
        }

        static void reduceKernel(Index1 index, ArrayView<KeyValuePair> pairs, ArrayView<KeyValuePair>  output, int NUM_KEYS, int NUM_OUTPUT)
        {
            for (var i = Group.IdxX * Group.DimX + index.X; i < NUM_OUTPUT; i += Group.DimX * Grid.DimX)
            {
                int startIndex = 0;
                int count = 0;
                int valueSize = 0;
                int j;

                for (j = 1; j < pairs.Length * NUM_KEYS; j++)
                {
                    if (StrCmp(pairs[j - 1].Key, pairs[j].Key) == -1)
                    {
                        if (count == i)
                        {
                            break;
                        }
                        else
                        {
                            count++;
                            startIndex = j;
                        }
                    }
                }

                if (count < i)
                {
                    return;
                }

                valueSize = j + startIndex;

                // Run the reducer
                Reduce(pairs, valueSize, output);
            }
        }

       static void Map(Input input, KeyValuePair pairs)
        {
            pairs.Value = 1;
            for (int i = 0; input.Key[i] != '\0'; i++)
            {

                pairs.Key[i] = input.Key[i];

            }
        }

        static void Reduce(ArrayView<KeyValuePair> pairs, int len, ArrayView<KeyValuePair> output)
        {
            for (int k = 0; k < (len - 1); k++)
            {
                int wordCount = 0;
                int stringCount = 1;
                int size = 0;
                int duplicatWordCount = 0;
                int duplicateCount = 1;

                for (int w = 0; ((pairs[k].Key[w]) != '\0'); w++)
                {
                    size++;
                }
                for (int l = k + 1; l < len; l++)
                {
                    wordCount = 0;
                    for (int m = 0; m < size; m++)
                    {
                        if ((((pairs[k].Key[m]) == (pairs[l].Key[m]))))
                        {
                            if ((pairs[l].Key[size]) != '\0')
                            {
                                break;
                            }
                            else
                            {
                                wordCount++;
                            }

                        }
                        if (size == wordCount)
                        {
                            stringCount++;
                        }
                    }
                }
                for (int v = k - 1; v >= 0; v--)
                {
                    duplicatWordCount = 0;
                    for (int m = 0; m < size; m++)
                    {
                        if (((pairs[k].Key[m]) == (pairs[v].Key[m])))
                        {
                            if ((pairs[v].Key[size]) != '\0')
                            {

                            }
                            else
                            {
                                duplicatWordCount++;
                            }

                        }
                        if (size == duplicatWordCount)
                        {
                            duplicateCount++;
                        }
                    }

                }
                if (duplicateCount == 1)
                {
                    output[k].Value = stringCount;
                    for (int i = 0; (pairs[k].Key[i]) != '\0'; i++)
                    {
                        output[k].Key[i] = pairs[k].Key[i];
                    }
                }
                else
                {
                    output[k].Value = 0;
                    for (int i = 0; (pairs[k].Key[i]) != '\0'; i++)
                    {
                        output[k].Key[i] = '\0';
                    }
                }

            }
        }

        private ArrayView<char> ToCharArrayView(string str)
        {
            var arr = new ArrayView<char>();
            for (int i=0; i<str.Length; i++)
            {
                arr[i] = str[i];
            }
            return arr;
        }
        public Task<Dictionary<string, int>> Execute(int NUM_KEYS, List<string> Lines)
        {
            KeyValuePair[] result = new KeyValuePair[Lines.Count];
            Parallel.ForEach(Lines, line =>
            {
                var words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var w_input = new ArrayView<Input>();
                for(var i=0; i < words.Length; i++)
                {
                    w_input[i].Key = ToCharArrayView(words[i]);
                }
                KeyValuePair[] result = new KeyValuePair[words.Length];
                using (var context = new Context())
                {
                    // For each available accelerator...
                    foreach (var acceleratorId in Accelerator.Accelerators)
                    {
                        // Create default accelerator for the given accelerator id
                        using (var accelerator = Accelerator.Create(context, acceleratorId))
                        {

                            using (var input = accelerator.Allocate<Input>(words.Length))
                            using (var bufferOut = accelerator.Allocate<KeyValuePair>(words.Length))
                            using (var secondStream = accelerator.CreateStream())
                            {
                                input.CopyTo(w_input, Index1.Zero);
                                var kernelMap = accelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<Input>, ArrayView<KeyValuePair>, int>(MapKernel);
                                // Launch buffer.Length many threads and pass a view to buffer
                                kernelMap(input.Length, input, bufferOut.View, 1);

                                // Wait for the kernel to finish...
                                accelerator.Synchronize();
                                var target = bufferOut;

                                bufferOut.MemSetToZero();

                                var kernelReduce = secondStream.Accelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<KeyValuePair>, ArrayView<KeyValuePair>, int, int>(reduceKernel);
                                kernelReduce(input.Length, target, bufferOut.View, NUM_KEYS, words.Length);

                                secondStream.Synchronize();

                                result = bufferOut.GetAsArray();
                            }
                        }
                    }
                }
            });
            return Task.FromResult(result.ToDictionary(p => p.Key.ToString(), p => p.Value));
        }
    }
}
