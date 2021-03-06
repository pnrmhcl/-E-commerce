#pragma checksum "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b312e3247231a87b276866fc5aa63f30f9a8e74"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Orders), @"mvc.1.0.view", @"/Views/User/Orders.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\_ViewImports.cshtml"
using WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\_ViewImports.cshtml"
using WebUI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\_ViewImports.cshtml"
using WebUI.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\_ViewImports.cshtml"
using WebUI.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b312e3247231a87b276866fc5aa63f30f9a8e74", @"/Views/User/Orders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eae80a835332a863363f053a525eb274284c227e", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Orders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Order>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<section id=\"cart_items\">\r\n    <div class=\"container\">\r\n");
#nullable restore
#line 5 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
         if (!String.IsNullOrEmpty(ViewBag.Success))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-success\">\r\n                ");
#nullable restore
#line 8 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
           Write(ViewBag.Success);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 10 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""table-responsive cart_info"">
            <table class=""table table-condensed"">
                <thead>
                    <tr class=""cart_menu"">
                        <td class=""image"">Sipari?? Kodu</td>
                        <td class=""description"">Sipari?? Tarihi</td>
                        <td class=""price"">??r??n Adedi</td>
                        <td class=""quantity"">Toplam</td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 23 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td class=\"cart_description\">\r\n                                <h4>");
#nullable restore
#line 27 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                               Write(item.OrderCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            </td>\r\n                            <td class=\"cart_description\">\r\n                                <h4>");
#nullable restore
#line 30 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                               Write(item.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            </td>\r\n                            <td class=\"cart_description\">\r\n                                <h4>");
#nullable restore
#line 33 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                               Write(item.OrderItems.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            </td>\r\n                            <td class=\"cart_description\">\r\n                                <h4>");
#nullable restore
#line 36 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                               Write(item.TotalPrice.ToString("##.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral("???</h4>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 39 "C:\Users\pmihc\Desktop\EnstrumanECommerce.Web\EnstrumanECommerce.Web\WebUI\WebUI\Views\User\Orders.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</section> <!--/#cart_items-->\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
