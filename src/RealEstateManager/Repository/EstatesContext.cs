using System;
using System.Security.Principal;
using System.Web.Security;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Interfaces;
using RealEstateManager.Repository.Sets;
using RealEstateManager.Utils;

namespace RealEstateManager.Repository
{
    public sealed class EstatesContext
    {
        public RealEstateManagerDataModelContainer DatabaseContext { get; }
        public AgentSet Agents { get; }
        public EstateSet Estates { get; }
        public BuildingInfoSet BuildingInfoes { get; }

        public ICurrentIdentity GetCurrentIdentity(IPrincipal user)
        {
            if (user?.Identity == null)
                throw new ArgumentNullException(nameof(user));

            var userInfo = CryptoHelper.DecryptAgentName(user.Identity.Name);
            var userInfoArray = userInfo.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            ICurrentIdentity currentIdentity;

            if (bool.Parse(userInfoArray[2]))
            {
                currentIdentity = new AgentIdentity
                {
                    Id = Guid.Parse(userInfoArray[0]),
                    Name = userInfoArray[1],
                };
            }
            else
            {
                currentIdentity = new PublicUserIdentity
                {
                    Id = Guid.Parse(userInfoArray[0]),
                    Name = userInfoArray[1],
                };
            }

            return currentIdentity;
        }

        public void SetCurrentIdentityAgent(Guid id, string username, bool persistent)
        {
            var encryptedName = CryptoHelper.EncryptAgentName($"{id}|{username}|{true}");
            FormsAuthentication.SetAuthCookie(encryptedName, persistent);
        }

        public void SetCurrentIdentityPublicPorifle(Guid id, string username, bool persistent)
        {
            var encryptedName = CryptoHelper.EncryptAgentName($"{id}|{username}|{false}");
            FormsAuthentication.SetAuthCookie(encryptedName, persistent);
        }

        public EstatesContext()
        {
            DatabaseContext = new RealEstateManagerDataModelContainer();
            Agents = new AgentSet(DatabaseContext);
            Estates = new EstateSet(DatabaseContext);
            BuildingInfoes = new BuildingInfoSet(DatabaseContext);
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
