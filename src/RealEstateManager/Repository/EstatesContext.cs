using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Sets;

namespace RealEstateManager.Repository
{
    public sealed class EstatesContext
    {
        public RealEstateManagerDataModelContainer DatabaseContext { get; }
        public AccountSet Accounts { get; }
        public EstateSet Estates { get; }
        public BuildingInfoSet BuildingInfoes { get; }
        public ContactSet Contacts { get; }
        public ContactAccountSet ContactAccounts { get; }

        public EstatesContext()
        {
            DatabaseContext = new RealEstateManagerDataModelContainer();
            Accounts = new AccountSet(DatabaseContext);
            Estates = new EstateSet(DatabaseContext);
            BuildingInfoes = new BuildingInfoSet(DatabaseContext);
            Contacts = new ContactSet(DatabaseContext);
            ContactAccounts = new ContactAccountSet(DatabaseContext);
        }

        public bool TryGetCurrentIdentity(IPrincipal user, out CurrentIdentity identity)
        {
            identity = null;

            if (user?.Identity == null)
                return false;

            if (!Guid.TryParse(user.Identity.Name, out var userId))
                return false;

            var account = DatabaseContext.Accounts
                .SingleOrDefault(x => x.Id == userId);

            if (account == null)
                return false;

            identity = new CurrentIdentity
            {
                Id = account.Id,
                Name = account.Username,
                Type = account.Type,
            };

            return true;
        }

        public void SetCurrentIdentity(Guid id, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(id.ToString(), persistent);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: Dispose
            }
        }

        ~EstatesContext()
        {
            Dispose(false);
        }
    }
}
