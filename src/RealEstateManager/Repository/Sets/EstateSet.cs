using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using RealEstateManager.Repository.Interfaces;

namespace RealEstateManager.Repository.Sets
{
    public class EstateSet : ISet<Estate, Guid, EstateData, EstateData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public EstateSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Estate Insert(EstateData data)
        {
            if (!ValidateInsertData(data))
                throw new InvalidOperationException("EstateData contains invalid properties.");

            if (!IsNameAvailable(data.Name))
                throw new InvalidOperationException($"Name {data.Name} is already registered.");

            if (!IsAddressAvailable(data.Address))
                throw new InvalidOperationException($"Address {data.Address} is already registered.");

            var estate = new Estate
            {
                Address = data.Address,
                Name = data.Name,
                PrivateDescription = data.PrivateDescription,
                PublicDescription = data.PublicDescription,
                Area = data.Area,
                BuildingInfoId = data.BuildingInfoId,
                Price = data.Price,
                Status = data.Status,
                Type = data.Type
            };

            _databaseContext.Estates.Add(estate);
            _databaseContext.SaveChanges();

            return estate;
        }

        public IEnumerable<Estate> Get(
            Expression<Func<Estate, bool>> filter = null,
            Func<IQueryable<Estate>, IOrderedQueryable<Estate>> orderBy = null,
            string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Estate GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.Estates
                .Where(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                return query
                    .Include(includeProperties)
                    .FirstOrDefault();
            }

            return query.FirstOrDefault();
        }

        public void Update(Guid id, EstateData data)
        {
            var estate = GetById(id);

            if (estate == null)
                throw new InvalidOperationException($"Estate with id {id} not found.");

            if (!ValidateInsertData(data))
                throw new InvalidOperationException("EstateData contains invalid properties.");

            if (!IsNameAvailable(data.Name, id))
                throw new InvalidOperationException($"Name {data.Name} is already registered.");

            if (!IsAddressAvailable(data.Address, id))
                throw new InvalidOperationException($"Address {data.Address} is already registered.");

            estate.PrivateDescription = data.PrivateDescription;
            estate.PublicDescription = data.PublicDescription;
            estate.BuildingInfoId = data.BuildingInfoId;
            estate.Address = data.Address;
            estate.Name = data.Name;
            estate.Area = data.Area;
            estate.Price = data.Price;
            estate.Status = data.Status;
            estate.Type = data.Type;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var estate = GetById(id);

            if (estate == null)
                throw new InvalidOperationException($"Estate with id {id} not found.");

            _databaseContext.Estates.Remove(estate);
            _databaseContext.SaveChanges();
        }

        // Checks if name is already registered
        // If id is given when updating, the same entity is excluded from the check
        public bool IsNameAvailable(string name, Guid id = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Name cannot be an empty string.");

            var trimmedName = name.Trim();

            return !_databaseContext.Estates
                .Any(x => x.Name == trimmedName && x.Id != id);
        }

        // Checks if address is already registered
        // If id is given when updating, the same entity is excluded from the check
        public bool IsAddressAvailable(string address, Guid id = default)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException("Address cannot be an empty string.");

            var trimmedAddress = address.Trim();

            return !_databaseContext.Estates
                .Any(x => x.Address == trimmedAddress && x.Id != id);
        }

        private static bool ValidateInsertData(EstateData data)
        {
            return !string.IsNullOrWhiteSpace(data.Address) &&
                   !string.IsNullOrWhiteSpace(data.Name) &&
                   !string.IsNullOrWhiteSpace(data.PrivateDescription) &&
                   !string.IsNullOrWhiteSpace(data.PublicDescription) &&
                   data.BuildingInfoId != Guid.Empty &&
                   data.Area > 0 &&
                   data.Price > 0;
        }
    }
}