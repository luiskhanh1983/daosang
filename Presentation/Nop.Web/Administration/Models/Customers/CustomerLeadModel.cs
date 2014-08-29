using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Customers
{
	public class CustomerLeadModel
	{
		public int Id { set; get; }
		
		public string Email { set; get; }

		public string Source { set; get; }

		public string FirstName { set; get; }

		public string LastName { set; get; }

		public string IP { set; get; }

		public DateTime DateReceived { set; get; }

	}
}