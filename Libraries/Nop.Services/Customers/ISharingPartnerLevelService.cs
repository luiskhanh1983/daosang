using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
namespace Nop.Services.Customers
{
    public partial interface ISharingPartnerLevelService
    {
        IList<SharingPartnerLevel> GetAllSharingPartnerLevel();
    }
}
