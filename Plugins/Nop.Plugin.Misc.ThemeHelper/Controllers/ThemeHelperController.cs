using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Forums;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Plugin.Misc.ThemeHelper.Models;
using Nop.Services.Media;
using Nop.Web.Models.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Services.Orders;
using Nop.Core.Plugins;
using Nop.Services.Logging;
using Nop.Web.Framework.UI;

namespace Nop.Plugin.Misc.ThemeHelper.Controllers
{
    public partial class ThemeHelperController : Controller
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        private readonly CatalogSettings _catalogSettings;
        private readonly BlogSettings _blogSettings;
        private readonly ForumSettings _forumSettings;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly ILanguageService _languageService;
        private readonly LocalizationSettings _localizationSettings;

        private readonly IShoppingCartService _shoppingCartService;

        private readonly IPluginFinder _pluginFinder;
        private readonly ILogger _logger;
        private readonly ILocalizationService _localizationService;


        #endregion

        #region Ctor

        public ThemeHelperController(IWorkContext workContext, IStoreContext storeContext,
            IStoreService storeService, ISettingService settingService, ICacheManager cacheManager,
            CatalogSettings catalogSettings, BlogSettings blogSettings, ForumSettings forumSettings,
            ICategoryService categoryService, IProductService productService, IPictureService pictureService,
            ILanguageService languageService, LocalizationSettings localizationSettings, 
            IShoppingCartService shoppingCartService,
            IPluginFinder pluginFinder,ILogger logger,
            ILocalizationService localizationService)
        {
            this._workContext = workContext;
            this._storeContext=storeContext;
            this._storeContext = storeContext;
            this._settingService = settingService;
            this._cacheManager = cacheManager;

            this._catalogSettings = catalogSettings;
            this._blogSettings = blogSettings;
            this._forumSettings = forumSettings;
            this._categoryService = categoryService;
            this._productService = productService;
            this._pictureService = pictureService;
            this._languageService = languageService;
            this._localizationSettings = localizationSettings;

            this._shoppingCartService = shoppingCartService;
            this._pluginFinder = pluginFinder;
            this._logger = logger;
            this._localizationService = localizationService;
        }

        #endregion 


        #region Notification code copied from Nop.Admin.Controllers.BaseNopController

        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("nop.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        #endregion

        #region Configure

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            return View("Nop.Plugin.Misc.ThemeHelper.Views.ThemeHelper.Configure");
        }


        //preconfigure
        [HttpPost, ActionName("Configure")]
        [FormValueRequired("preconfigure")]
        public ActionResult Preconfigure()
        {
            var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName("Misc.ThemeHelper");
            if (pluginDescriptor == null)
                throw new Exception("Cannot load the plugin");

            //plugin
            var plugin = pluginDescriptor.Instance() as ThemeHelperPlugin;
            if (plugin == null)
                throw new Exception("Cannot load the plugin");

            //preconfigure
            try
            {
                plugin.Preconfigure();
                var message = _localizationService.GetResource("ThemeHelper.PreconfigureCompleted");
                AddNotification(NotifyType.Success, message, true);
                _logger.Information(message);
            }
            catch (Exception exception)
            {
                var message = _localizationService.GetResource("ThemeHelper.PreconfigureError") + exception;
                AddNotification(NotifyType.Error, message, true);
                _logger.Error(message);
            }

            return Configure();
        }

        #endregion

        #region TopMenu
        
        /// <summary>
        /// Prepare category (simple) models
        /// </summary>
        /// <param name="rootCategoryId">Root category identifier</param>
        /// <param name="loadSubCategoriesForIds">Load subcategories only for the specified category IDs; pass null to load subcategories for all categories</param>
        /// <param name="level">Current level</param>
        /// <param name="levelsToLoad">A value indicating how many levels to load (max)</param>
        /// <param name="validateIncludeInTopMenu">A value indicating whether we should validate "include in top menu" property</param>
        /// <returns>Category models</returns>
        [NonAction]
        protected IList<CategorySimpleModel> PrepareCategorySimpleModels(int rootCategoryId,
            IList<int> loadSubCategoriesForIds, int level, int levelsToLoad, bool validateIncludeInTopMenu)
        {
            var result = new List<CategorySimpleModel>();
            foreach (var category in _categoryService.GetAllCategoriesByParentCategoryId(rootCategoryId))
            {
                if (validateIncludeInTopMenu && !category.IncludeInTopMenu)
                {
                    continue;
                }

                var categoryModel = new CategorySimpleModel()
                {
                    Id = category.Id,
                    Name = category.GetLocalized(x => x.Name),
                    SeName = category.GetSeName(),
                    PictureUrl = _pictureService.GetPictureUrl(category.PictureId),
                    Description = category.Description
                };

                //product number for each category
                if (_catalogSettings.ShowCategoryProductNumber)
                {
                    categoryModel.NumberOfProducts = GetCategoryProductNumber(category.Id);
                }

                //load subcategories?
                bool loadSubCategories = false;
                if (loadSubCategoriesForIds == null)
                {
                    //load all subcategories
                    loadSubCategories = true;
                }
                else
                {
                    //we load subcategories only for certain categories
                    for (int i = 0; i <= loadSubCategoriesForIds.Count - 1; i++)
                    {
                        if (loadSubCategoriesForIds[i] == category.Id)
                        {
                            loadSubCategories = true;
                            break;
                        }
                    }
                }
                if (levelsToLoad <= level)
                {
                    loadSubCategories = false;
                }
                if (loadSubCategories)
                {
                    var subCategories = PrepareCategorySimpleModels(category.Id, loadSubCategoriesForIds, level + 1, levelsToLoad, validateIncludeInTopMenu);
                    categoryModel.SubCategories.AddRange(subCategories);
                }
                result.Add(categoryModel);
            }

            return result;
        }

        [NonAction]
        protected int GetCategoryProductNumber(int categoryId)
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_NUMBER_OF_PRODUCTS_MODEL_KEY,
                string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id, categoryId);
            var cachedModel = _cacheManager.Get(cacheKey, () =>
            {
                var categoryIds = new List<int>();
                categoryIds.Add(categoryId);
                //include subcategories
                if (_catalogSettings.ShowCategoryProductNumberIncludingSubcategories)
                    categoryIds.AddRange(GetChildCategoryIds(categoryId));
                var numberOfProducts = _productService
                    .SearchProducts(categoryIds: categoryIds,
                    storeId: _storeContext.CurrentStore.Id,
                    visibleIndividuallyOnly: true,
                    pageSize: 1)
                    .TotalCount;
                return numberOfProducts;
            });
            return cachedModel;
        }

        [NonAction]
        protected List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_CHILD_IDENTIFIERS_MODEL_KEY, parentCategoryId, string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            return _cacheManager.Get(cacheKey, () =>
            {
                var categoriesIds = new List<int>();
                var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId);
                foreach (var category in categories)
                {
                    categoriesIds.Add(category.Id);
                    categoriesIds.AddRange(GetChildCategoryIds(category.Id));
                }
                return categoriesIds;
            });
        }

        [ChildActionOnly]
        public ActionResult TopMenu(int currentCategoryId, int currentProductId)
        {
            var activeCategory = _categoryService.GetCategoryById(currentCategoryId);
            if (activeCategory == null && currentProductId > 0)
            {
                var productCategories = _categoryService.GetProductCategoriesByProductId(currentProductId);
                if (productCategories.Count > 0)
                    activeCategory = productCategories[0].Category;
            }
            int activeCategoryId = activeCategory != null ? activeCategory.Id : 0;
            var customerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_MENU_MODEL_KEY, _workContext.WorkingLanguage.Id,
                string.Join(",", customerRolesIds), _storeContext.CurrentStore.Id);
            var cachedModel = _cacheManager.Get(cacheKey, () =>
            {
                return PrepareCategorySimpleModels(0, null, 0, _catalogSettings.TopCategoryMenuSubcategoryLevelsToDisplay, true).ToList();
            });

            var model = new TopMenuModel()
            {
                Categories = cachedModel,
                RecentlyAddedProductsEnabled = _catalogSettings.RecentlyAddedProductsEnabled,
                BlogEnabled = _blogSettings.Enabled,
                ForumEnabled = _forumSettings.ForumsEnabled,
                CurrentCategoryId=activeCategoryId
            };
            
            return PartialView(model);
        }

        
        #endregion

        #region Language Selector

        [NonAction]
        protected LanguageSelectorModel PrepareLanguageSelectorModel()
        {
            
            var availableLanguages = _cacheManager.Get(string.Format(ModelCacheEventConsumer.AVAILABLE_LANGUAGES_MODEL_KEY, _storeContext.CurrentStore.Id), () =>
            {
                var result = _languageService
                    .GetAllLanguages(storeId: _storeContext.CurrentStore.Id)
                    .Select(x => new LanguageModel()
                    {
                        Id = x.Id,
                        Name = x.UniqueSeoCode,
                        FlagImageFileName = x.FlagImageFileName,
                    })
                    .ToList();
                return result;
            });

            var model = new LanguageSelectorModel()
            {
                CurrentLanguageId = _workContext.WorkingLanguage.Id,
                AvailableLanguages = availableLanguages,
                UseImages = _localizationSettings.UseImagesForLanguageSelection
            };
            return model;
        }

        //language
        [ChildActionOnly]
        public ActionResult LanguageSelector()
        {
            var model = PrepareLanguageSelectorModel();
            return PartialView(model);
        }
        
        #endregion

        #region Fly-Out Shopping Cart

        [ValidateInput(false)]
        [FormValueRequired(FormValueRequirement.StartsWith, "removefromcart-")]
        public ActionResult RemoveCartItem(FormCollection form)
        {
            //get shopping cart item identifier
            int sciId = 0;
            foreach (var formValue in form.AllKeys)
                if (formValue.StartsWith("removefromcart-", StringComparison.InvariantCultureIgnoreCase))
                    sciId = Convert.ToInt32(formValue.Substring("removefromcart-".Length));
            //get shopping cart item
            var cart = _workContext.CurrentCustomer.ShoppingCartItems
                .Where(x => x.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .Where(x => x.StoreId == _storeContext.CurrentStore.Id)
                .ToList();
            var sci = cart.FirstOrDefault(x => x.Id == sciId);
            if (sci == null)
            {
                return RedirectToRoute("ShoppingCart");
            }

            //remove the cart item
            _shoppingCartService.DeleteShoppingCartItem(sci, ensureOnlyActiveCheckoutAttributes: true);


            //updated cart
            cart = _workContext.CurrentCustomer.ShoppingCartItems
                .Where(x => x.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .Where(x => x.StoreId == _storeContext.CurrentStore.Id)
                .ToList();
          
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        #endregion

    }
}
