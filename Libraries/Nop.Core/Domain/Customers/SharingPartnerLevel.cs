using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Shipping;

namespace Nop.Core.Domain.Customers
{
   public class SharingPartnerLevel : BaseEntity, ILocalizedEntity
    {
       public string SharingPartnerLevelName { get; set; }
    }
}
