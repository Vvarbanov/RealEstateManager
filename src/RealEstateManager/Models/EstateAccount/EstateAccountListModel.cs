using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Repository;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Models.EstateAccount
{
    public class EstateAccountListModel
    {
        public List<EstateAccountModel> EstateAgents { get; set; }

        public void GetEstateAccounts(EstatesContext db, Guid estateId)
        {
            var agents = db.Accounts.Get(x => x.Type == UserType.Agent, null, nameof(Account.Estates));

            EstateAgents = agents
                .Select(x => new EstateAccountModel
                {
                    EstateId = estateId,
                    UserId = x.Id,
                    Username = x.Username,
                    HasRights = x.Estates.Any(y => y.EstateId == estateId)
                })
                .ToList();
        }
    }
}