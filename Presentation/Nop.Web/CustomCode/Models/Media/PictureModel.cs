using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Media
{
    public partial class PictureModel : BaseNopModel
    {
        public string SecondImageUrl { get; set; }

        public string SecondFullSizeImageUrl { get; set; }

        public string SecondTitle { get; set; }

        public string SecondAlternateText { get; set; }

        public string DefaultImageUrl { get; set; }
    }
}