using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.ThemeHelper
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //IPN
            routes.MapRoute("FlyOutShoppingCart",
                 "flyoutcart/",
                 new { controller = "ThemeHelper", action = "RemoveCartItem" },
                 new[] { "Nop.Plugin.Misc.ThemeHelper.Controllers" }
            );       
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
