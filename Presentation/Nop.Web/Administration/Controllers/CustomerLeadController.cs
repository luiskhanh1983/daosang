using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Models.Customers;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Telerik.Web.Mvc;

namespace Nop.Admin.Controllers
{
    [AdminAuthorize]
    public partial class CustomerLeadController : BaseNopController
    {
        #region Fields

        private readonly ICustomerLeadService _customerLeadService;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Constructors

        public CustomerLeadController(ICustomerLeadService customerLeadService,
            ILocalizationService localizationService, ICustomerActivityService customerActivityService,
            IPermissionService permissionService)
        {
            this._customerLeadService = customerLeadService;
            this._permissionService = permissionService;
        }

        #endregion

        #region Customer Leads

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            return View();
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            var customerLeads = _customerLeadService.GetAllCustomerLeads();
            var gridModel = new GridModel<CustomerLeadModel>
            {
                Data = customerLeads.Select(x => new CustomerLeadModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    IP = x.IP,
                    Source = x.Source,
                    DateReceived = x.DateReceived
                }),
                Total = customerLeads.Count()
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        #endregion
    }
}
