//Contributor: Nicolas Muniere


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using Nop.Core;
using Nop.Services.Security;
using System.IO;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Catalog;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Catalog;
using Nop.Services.Customers;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Advanced.PostData
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class NopService : INopService
    {
        private readonly ICategoryService _categoryService;
        private readonly ICustomerLeadService _customerLeadService;

        public NopService()
        {
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            _customerLeadService = EngineContext.Current.Resolve<ICustomerLeadService>();
        }

        public void InsertCustomerLead(string source, string firstName, string lastName, string email, string ip)
        {
            var customerLead = new CustomerLead();
            customerLead.DateReceived = DateTime.Now;
            customerLead.Email = email;
            customerLead.FirstName = firstName;
            customerLead.LastName = lastName;
            customerLead.Source = source;
            customerLead.IP = ip;
            _customerLeadService.InsertCustomerLead(customerLead);
        }

        public void CreateCategory(string name, string description)
        {
            var category = new Category();
            category.Name = name;
            category.Description = description;
            category.CreatedOnUtc = DateTime.UtcNow;
            category.UpdatedOnUtc = DateTime.UtcNow;
            _categoryService.InsertCategory(category);
        }

        public void CreateContentFileExample(string contentfile)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/file");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            using (FileStream fs = new FileStream(path + "/test.txt", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(contentfile);
                sw.Flush();
            }
        }
    }
}
