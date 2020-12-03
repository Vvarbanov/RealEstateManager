using System;
using RealEstateManager.Repository.Interfaces;

namespace RealEstateManager.Repository
{
    public class PublicUserIdentity : ICurrentIdentity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
