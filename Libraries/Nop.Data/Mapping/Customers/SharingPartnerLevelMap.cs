using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Customers;
namespace Nop.Data.Mapping.Customers
{
    public partial class SharingPartnerLevelMap : EntityTypeConfiguration<SharingPartnerLevel>
    {
        public SharingPartnerLevelMap()
        {
            this.ToTable("SharingPartnerLevel");
            this.HasKey(u => u.Id);
            this.Property(c => c.SharingPartnerLevelName).IsRequired().HasMaxLength(50);
        }
    }
}
