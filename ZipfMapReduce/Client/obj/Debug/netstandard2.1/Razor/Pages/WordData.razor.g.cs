#pragma checksum "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee67e00ebe55a7f31e9ff98d782847db43f1d67c"
// <auto-generated/>
#pragma warning disable 1591
namespace ZipfMapReduce.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using ZipfMapReduce.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using ZipfMapReduce.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\_Imports.razor"
using Syncfusion.Blazor.Charts;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/wordrecords")]
    public partial class WordData : WordDataModel
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Word Data</h1>\n");
            __builder.AddMarkupContent(1, "<h3>Here you can see all words</h3>\n\n\n");
#nullable restore
#line 8 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
 if (wordList == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(2, "    ");
            __builder.AddMarkupContent(3, "<p><em>Loading...</em></p>\n");
#nullable restore
#line 11 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(4, "    ");
            __builder.OpenElement(5, "table");
            __builder.AddAttribute(6, "class", "table");
            __builder.AddMarkupContent(7, "\n        ");
            __builder.AddMarkupContent(8, "<thead>\n            <tr>\n                <th>Id</th>\n                <th>Value</th>\n                <th>Count</th>\n            </tr>\n        </thead>\n        ");
            __builder.OpenElement(9, "tbody");
            __builder.AddMarkupContent(10, "\n");
#nullable restore
#line 23 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
             foreach (var wrd in wordList)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(11, "                ");
            __builder.OpenElement(12, "tr");
            __builder.AddMarkupContent(13, "\n                    ");
            __builder.OpenElement(14, "td");
            __builder.AddContent(15, 
#nullable restore
#line 26 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
                         wrd.Id

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\n                    ");
            __builder.OpenElement(17, "td");
            __builder.AddContent(18, 
#nullable restore
#line 27 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
                         wrd.Value

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\n                    ");
            __builder.OpenElement(20, "td");
            __builder.AddContent(21, 
#nullable restore
#line 28 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
                         wrd.Count

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(23, "\n");
#nullable restore
#line 30 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(24, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\n");
#nullable restore
#line 33 "C:\Users\mylif\source\repos\Projects\Zipf Map Reduce\ZipfMapReduce\Client\Pages\WordData.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
