using Nop.Core.Domain.Localization;
using Nop.Services.Messages;
using Nop.Web.Models.BeYoung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.ViewModels;
using Nop.Web.Common;
namespace Nop.Web.Controllers
{

    public class BeYoungController : Controller
    {
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        public BeYoungController(IWorkflowMessageService workflowMessageService, LocalizationSettings localizationSettings)
        {
            _workflowMessageService = workflowMessageService;
            _localizationSettings = localizationSettings;
        }

        public ActionResult Index()
        {
            var beyoungModel = new BeYoungModel();
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Sharing Partner",
                Value = "Sharing Partner",
                Selected = true
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Emerald",
                Value = "Emerald"
              
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Sapphire",
                Value = "Sapphire"
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Ruby",
                Value = "Ruby"
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Topaz",
                Value = "Topaz"
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Amethyst",
                Value = "Amethyst"
            });
            beyoungModel.AvailableLevels.Add(new SelectListItem
            {
                Text = "Diamond",
                Value = "Diamond"
            });
            for (int i = 0; i < 15; i++)
            {
                string year = Convert.ToString(DateTime.Now.Year + i);
                beyoungModel.ExpireYears.Add(new SelectListItem()
                {
                    Text = year,
                    Value = year,
                });
            }

            //months
            for (int i = 1; i <= 12; i++)
            {
                string text = (i < 10) ? "0" + i.ToString() : i.ToString();
                beyoungModel.ExpireMonths.Add(new SelectListItem()
                {
                    Text = text,
                    Value = i.ToString(),
                });
            }

            ViewBag.messageerror = "";
            ViewBag.formactivity = "";
            return View("Index", beyoungModel);
        }
        [HttpPost]
        public ActionResult engage(BeYoungModel model)
        {
            Session["BeyoungModelform1"] = model;

            Random r = new Random();
            string valicode = r.Next(100000, 999999).ToString();
            Session["validcode"] = valicode;
            _workflowMessageService.SendBeyoungValidCode(model.FirstName, model.LastName, valicode, model.Email, _localizationSettings.DefaultAdminLanguageId);
            ActionResult actionResult = Index();
            ViewBag.formactivity = "screen1";
            return actionResult;
        }

        [HttpPost]
        public ActionResult engaged(BeYoungModel model)
        {
            ActionResult actionResult = Index();
            if (Session["validcode"] == null || Session["validcode"].ToString() != model.VaidCode)
            {
                ViewBag.formactivity = "screen1";
                ViewBag.messageerror = "invalid code";
            }
            else
            {
                ViewBag.formactivity = "screen2";
            }

            return actionResult;
        }
        [HttpPost]
        public ActionResult form5(BeYoungModel model)
        {
            Session["BeyoungModelform5"] = model;

            ActionResult actionResult = Index();
            BeYoungModel modelsave = Session["BeyoungModelform1"] as BeYoungModel;
            model.FirstName = modelsave.FirstName;
            model.LastName = modelsave.LastName;
            model.Level = modelsave.Level;
            model.SharingPartnerID = modelsave.SharingPartnerID;
            model.Email = modelsave.Email;

            _workflowMessageService.SendBeyoungDataCustomer(model.FirstName, model.LastName, model.SharingPartnerID, model.Level, model.Email, model.UserName, model.Password, _localizationSettings.DefaultAdminLanguageId);

            ViewBag.formactivity = "screen5";
            return actionResult;

        }
        [HttpPost]

        public ActionResult form6(BeYoungModel model)
        {
            ActionResult actionResult = Index();
            BeYoungModel modelsave = Session["BeyoungModelform1"] as BeYoungModel;
            BeYoungModel modelsave2 = Session["BeyoungModelform5"] as BeYoungModel;
            model.FirstName = modelsave.FirstName;
            model.LastName = modelsave.LastName;
            model.Level = modelsave.Level;
            model.SharingPartnerID = modelsave.SharingPartnerID;
            model.Email = modelsave.Email;
            model.UserName = modelsave2.UserName;
            model.Password = modelsave2.Password;
            _workflowMessageService.SendBeyoungDataCustomerCard(model.FirstName, model.LastName, model.SharingPartnerID, model.Level, model.Email, model.UserName, model.Password, model.CardNumber, model.ExpireMonth, model.ExpireYear, model.CardCVV, "partner@bybbmarketing.com", _localizationSettings.DefaultAdminLanguageId);
            ViewBag.formactivity = "screen6";
            //xx
            return actionResult;

        }

    }
}