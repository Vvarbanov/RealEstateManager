using System;

namespace RealEstateManager.Repository.Interfaces
{
    public interface ICurrentIdentity
    {
        Guid Id { get; }
        string Name { get; }
    }
}
