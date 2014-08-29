using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Services.Configuration;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Nop.Services.Localization;
using Nop.Core.Domain.Media;

namespace Nop.Plugin.Misc.ThemeHelper
{
    public class ThemeHelperPlugin : BasePlugin, IMiscPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly MediaSettings _mediaSettings;

        public ThemeHelperPlugin(ISettingService settingService, 
            IWebHelper webHelper,MediaSettings mediaSettings)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._mediaSettings = mediaSettings;
        }

     

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ThemeHelper";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Misc.ThemeHelper.Controllers" }, { "area", null } };
        }
      
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
           //add locales
            this.AddOrUpdatePluginLocaleResource("ThemeHelper.Preconfigure", "Preconfigure");
            this.AddOrUpdatePluginLocaleResource("ThemeHelper.PreconfigureCompleted", "Preconfigure completed!");
            this.AddOrUpdatePluginLocaleResource("ThemeHelper.PreconfigureError", "Preconfigure error: ");
            this.AddOrUpdatePluginLocaleResource("MyWishlist", "My Wishlist");
            this.AddOrUpdatePluginLocaleResource("MyCart", "My Cart");
            this.AddOrUpdatePluginLocaleResource("shoppingcart.mini.recentlyaddeditems", "Recently added item(s)");
            this.AddOrUpdatePluginLocaleResource("ShoppingCart.HeaderLinkQuantity", "({0} ITEM)");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.description", "Description");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.generalinfo", "General Info");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.tags", "Tags");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.RelatedProducts", "Related Products");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.productsalsopurchased", "Products Also Purchased");
            this.AddOrUpdatePluginLocaleResource("product.details.tab.tierprices", "Tier Prices");
            this.AddOrUpdatePluginLocaleResource("ORIGINALPRODUCTS", "ORIGINAL PRODUCTS");
            this.AddOrUpdatePluginLocaleResource("FREESHIPPING", "FREE SHIPPING");
            this.AddOrUpdatePluginLocaleResource("CASHONDELIVERY", "CASH ON DELIVERY");
            this.AddOrUpdatePluginLocaleResource("30DAYRETURNS", "30-DAY RETURNS");
            this.AddOrUpdatePluginLocaleResource("shoppingcart.headerquantity", "{0} ITEM(S)");
                
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //locales
            this.DeletePluginLocaleResource("ThemeHelper.Preconfigure");
            this.DeletePluginLocaleResource("ThemeHelper.PreconfigureCompleted");
            this.DeletePluginLocaleResource("ThemeHelper.PreconfigureError");
            this.DeletePluginLocaleResource("MyWishlist");
            this.DeletePluginLocaleResource("MyCart");
            this.DeletePluginLocaleResource("shoppingcart.mini.recentlyaddeditems");
            this.DeletePluginLocaleResource("product.details.tab.description");
            this.DeletePluginLocaleResource("product.details.tab.generalinfo");
            this.DeletePluginLocaleResource("product.details.tab.tags");
            this.DeletePluginLocaleResource("product.details.tab.RelatedProducts");
            this.DeletePluginLocaleResource("product.details.tab.productsalsopurchased");
            this.DeletePluginLocaleResource("product.details.tab.tierprices");
            this.DeletePluginLocaleResource("ORIGINALPRODUCTS");
            this.DeletePluginLocaleResource("FREESHIPPING");
            this.DeletePluginLocaleResource("CASHONDELIVERY");
            this.DeletePluginLocaleResource("30DAYRETURNS");
            this.DeletePluginLocaleResource("shoppingcart.headerquantity");
            
            base.Uninstall();
        }

        public void Preconfigure()
        {
            //media settings
            _mediaSettings.ManufacturerThumbPictureSize = 420;
            _mediaSettings.ProductThumbPictureSize = 420;
            

            _settingService.SaveSetting(_mediaSettings);

        }
    }
}
