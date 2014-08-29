using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Services.Events;

namespace Nop.Services.Customers
{
    public partial class SharingPartnerLevelService : ISharingPartnerLevelService
    {
        private readonly IRepository<SharingPartnerLevel> _sharingPartnerLevelRepository;
        public SharingPartnerLevelService(IRepository<SharingPartnerLevel> sharingPartnerRepository)
        {
            _sharingPartnerLevelRepository = sharingPartnerRepository;
        }
        public virtual IList<SharingPartnerLevel> GetAllSharingPartnerLevel()
        {
            return _sharingPartnerLevelRepository.Table.ToList();
        }
    }
}
