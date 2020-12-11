using System;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Repository
{
    public class CurrentIdentity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
    }
}
