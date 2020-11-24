using System;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Sets;

namespace RealEstateManager.Repository
{
    public sealed class EstatesContext
    {
        public RealEstateManagerDataModelContainer DatabaseContext { get; }
        public AgentSet Agents { get; }

        public EstatesContext()
        {
            DatabaseContext = new RealEstateManagerDataModelContainer();
            Agents = new AgentSet(DatabaseContext);
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
