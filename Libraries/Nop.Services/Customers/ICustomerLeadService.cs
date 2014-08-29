using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer service interface
    /// </summary>
    public partial interface ICustomerLeadService
    {
        void InsertCustomerLead(CustomerLead customer);
	    IList<CustomerLead> GetAllCustomerLeads();
    }
}