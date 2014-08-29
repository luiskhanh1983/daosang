using Nop.Core.Domain.Localization;
using Nop.Services.Messages;
using Nop.Web.Models.CreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Common;
namespace Nop.Web.Controllers
{
    [RequiresSSL]
    public class CardController : Controller
    {
        //
        // GET: /Card/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CreditCardModel model)
        {
            return View();
        }
    }
}