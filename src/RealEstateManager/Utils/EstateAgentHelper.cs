using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using RealEstateManager.Models.EstateAccount;
using RealEstateManager.Repository;

namespace RealEstateManager.Utils
{
    public static class EstateAgentHelper
    {
        public static bool IsAccountAuthorized(
            CurrentIdentity currentIdentity, Guid estateId, List<EstateAccountModel> estateAccounts)
        {
            if (currentIdentity == null || currentIdentity.Type == UserType.PublicUser)
                return false;

            if (currentIdentity.Type == UserType.Admin)
                return true;

            return estateAccounts != null &&
                estateAccounts.Any(x => x.EstateId == estateId && x.AccountId == currentIdentity.Id);
        }

        public static bool IsAccountAuthorized(
            CurrentIdentity currentIdentity, Guid estateId, EstatesContext db)
        {
            return db.Estates.IsAccountAuthorized(currentIdentity, estateId);
        }

        public static bool IsAccountAdmin(CurrentIdentity currentIdentity)
        {
            return currentIdentity != null &&
                currentIdentity.Type == UserType.Admin;
        }
    }
}
