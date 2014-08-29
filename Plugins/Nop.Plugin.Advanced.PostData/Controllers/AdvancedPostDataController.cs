using System.Web.Mvc;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Advanced.PostData.Controllers
{
    [AdminAuthorize]
    public class AdvancedPostDataController : Controller
    {
        public ActionResult Configure()
        {
            return View("Nop.Plugin.Advanced.PostData.Views.AdvancedPostData.Configure");
        }
    }
}
