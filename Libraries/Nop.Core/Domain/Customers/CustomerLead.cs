using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Customers
{
	public class CustomerLead : BaseEntity
	{

		public string Email { set; get; }

		public string Source { set; get; }

		public string FirstName { set; get; }

		public string LastName { set; get; }
		
		public string IP { set; get; }

		public DateTime DateReceived { set; get; }
	}
}
