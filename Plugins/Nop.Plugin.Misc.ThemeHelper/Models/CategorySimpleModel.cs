using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.ThemeHelper.Models
{
    public class CategorySimpleModel : BaseNopEntityModel
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }

        public string Name { get; set; }

        public string SeName { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public int? NumberOfProducts { get; set; }

        public List<CategorySimpleModel> SubCategories { get; set; }
    }
}