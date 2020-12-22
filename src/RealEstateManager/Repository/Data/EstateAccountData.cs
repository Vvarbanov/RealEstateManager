using System;

namespace RealEstateManager.Repository.Data
{
    public class EstateAccountData
    {
        public Guid EstateId { get; set; }

        public Guid AccountId { get; set; }

        public bool HasRights { get; set; }
    }
}
