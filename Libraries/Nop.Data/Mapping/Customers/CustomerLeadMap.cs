using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public partial class CustomerLeadMap : EntityTypeConfiguration<CustomerLead>
    {
		public CustomerLeadMap()
        {
            this.ToTable("CustomerLead");
            this.HasKey(c => c.Id);
            this.Property(u => u.Email).HasMaxLength(1000);
        }
    }
}