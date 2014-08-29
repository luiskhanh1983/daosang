using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Shipping;
using Nop.Services.Common;
using Nop.Services.Events;

namespace Nop.Services.Customers
{
	/// <summary>
	/// Customer service
	/// </summary>
	public partial class CustomerLeadService : ICustomerLeadService
	{
		#region Fields

		private readonly IRepository<CustomerLead> _customerLeadRepository;
		private readonly ICacheManager _cacheManager;

		#endregion

		#region Ctor
		public CustomerLeadService(ICacheManager cacheManager,
			IRepository<CustomerLead> customerLeadRepository
			)
		{
			this._cacheManager = cacheManager;
			this._customerLeadRepository = customerLeadRepository;
		}

		#endregion

		#region Methods
		public virtual void InsertCustomerLead(CustomerLead customerLead)
		{
			if (customerLead == null)
				throw new ArgumentNullException("customerLead");
			_customerLeadRepository.Insert(customerLead);
		}
		public virtual IList<CustomerLead> GetAllCustomerLeads()
		{
			return _customerLeadRepository.Table.ToList();
		}

		#endregion
	}
}