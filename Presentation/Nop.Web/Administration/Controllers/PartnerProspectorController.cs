using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Models.Catalog;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Orders;
namespace Nop.Admin.Controllers
{
    public class PartnerProspectorController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public PartnerProspectorController(ICategoryService categoryService, IOrderService orderService, IProductService productService)
        {
            this._categoryService = categoryService;
            this._orderService = orderService;
            this._productService = productService;
        }
        //
        // GET: /PartnerProspector/
        public ActionResult bybbstats()
        {
            var category = _categoryService.GetCategoryById(45);
            var model = category.ToModel();
            Product productBA = _productService.GetProductByName("Build Automator 100");
            Product productAG = _productService.GetProductByName("Alpha Group Membership");
            Nop.Admin.Models.Catalog.CategoryModel.BybbstatsModel bybbstatsmodel = new Nop.Admin.Models.Catalog.CategoryModel.BybbstatsModel();
            IList<OrderItem> listoderitemsBA = _orderService.GetOrderItemsFromProductIdWithAllStatus(productBA.Id);
            bybbstatsmodel.BUILDAUTOMATORSSold = listoderitemsBA.Count().ToString();
            IList<OrderItem> listoderitemsAG = _orderService.GetOrderItemsFromProductIdWithAllStatus(productAG.Id);
            var countlistoderitemsAG = listoderitemsAG.Where(x => x.PriceExclTax == 0 && x.PriceInclTax == 0 && x.UnitPriceExclTax == 0 && x.UnitPriceInclTax == 0).Count();
            int group = 300;
            bybbstatsmodel.GroupLimit = group.ToString();
            bybbstatsmodel.MembershipsFilled = countlistoderitemsAG.ToString();
            bybbstatsmodel.MembershipsRemaining = (group - countlistoderitemsAG).ToString();
            bybbstatsmodel.PARTNERPROSPECTSordered = (listoderitemsBA.Count() * 100).ToString();
            bybbstatsmodel.GRATISPARTNERPROSPECTSgarnered = ((1000 - productBA.StockQuantity) * 100).ToString();
            bybbstatsmodel.ALPHAPARTNERPROSPECTSgarnered = (listoderitemsAG.Count() * 100).ToString();
            bybbstatsmodel.TOTALORDERED = ((listoderitemsBA.Count() * 100) + ((1000 - productBA.StockQuantity) * 100) + (listoderitemsAG.Count() * 100)).ToString();
            var totalDelivered = 0;
            bybbstatsmodel.TOTALDELIVERED = totalDelivered.ToString();

            model.Bybbstats = bybbstatsmodel;
            return View("bybbstats", model);
        }
    }
}