using System;
using System.Collections.Generic;

namespace RealEstateManager.Models.EstateAccount
{
    public class EstateAccountListModel
    {
        public Guid EstateId { get; set; }

        public List<EstateAccountModel> EstateAccounts { get; set; }
    }
}
