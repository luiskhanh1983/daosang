using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using Nop.Web.Models.Customer;
namespace Nop.Web.Models.Catalog
{
    public partial class CategoryModel : BaseNopEntityModel
    {
        public CategoryModel()
        {
            PictureModel = new PictureModel();
            FeaturedProducts = new List<ProductOverviewModel>();
            Products = new List<ProductOverviewModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
            SubCategories = new List<SubCategoryModel>();
            CategoryBreadcrumb = new List<CategoryModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        
        public PictureModel PictureModel { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public bool DisplayCategoryBreadcrumb { get; set; }
        public IList<CategoryModel> CategoryBreadcrumb { get; set; }
        
        public IList<SubCategoryModel> SubCategories { get; set; }

        public IList<ProductOverviewModel> FeaturedProducts { get; set; }
        public IList<ProductOverviewModel> Products { get; set; }
        public LivewebinarsModel Livewebinar { get; set; }
        public BybbstatsModel Bybbstats { get; set; }

		#region Nested Classes

        public partial class SubCategoryModel : BaseNopEntityModel
        {
            public SubCategoryModel()
            {
                PictureModel = new PictureModel();
            }

            public string Name { get; set; }

            public string SeName { get; set; }

            public PictureModel PictureModel { get; set; }
        }
        public partial class LivewebinarsModel
        {
            public string Email { set; get; }
            public string FirstName { set; get; }
            public string ContryCode { set; get; }
            public string ContryName { set; get; }
            public string PhoneNumber { set; get; }

        }
        public partial class BybbstatsModel
        {
            public string BUILDAUTOMATORSSold { set; get; }
            public string GroupLimit { set; get; }
            public string MembershipsFilled { set; get; }
            public string MembershipsRemaining { set; get; }
            public string PARTNERPROSPECTSordered { set; get; }
            public string GRATISPARTNERPROSPECTSgarnered { set; get; }
            public string ALPHAPARTNERPROSPECTSgarnered { get; set; }

        }

		#endregion
    }
}