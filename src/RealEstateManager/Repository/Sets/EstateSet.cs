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

            var estate = new Estate
            {
                Address = data.Address,
                Name = data.Name,
                PrivateDescription = data.PrivateDescription,
                PublicDescription = data.PublicDescription,
                Area = data.Area,
                Price = data.Price,
                Status = data.Status,
                Type = data.Type,
                UpdateDate = DateTime.Now
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
            var query = _databaseContext.Estates.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var includePropsArray = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProp in includePropsArray)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public Estate GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.Estates
                .Where(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var includePropsArray = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProp in includePropsArray)
                {
                    query = query.Include(includeProp);
                }
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

            estate.PrivateDescription = data.PrivateDescription;
            estate.PublicDescription = data.PublicDescription;
            estate.Address = data.Address;
            estate.Name = data.Name;
            estate.Area = data.Area;
            estate.Price = data.Price;
            estate.Status = data.Status;
            estate.Type = data.Type;
            estate.UpdateDate = DateTime.Now;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var estate = GetById(id, "BuildingInfo");

            if (estate == null)
                throw new InvalidOperationException($"Estate with id {id} not found.");

            _databaseContext.Estates.Remove(estate);
            _databaseContext.SaveChanges();
        }

        private static bool ValidateInsertData(EstateData data)
        {
            return !string.IsNullOrWhiteSpace(data.Address) &&
                   !string.IsNullOrWhiteSpace(data.Name) &&
                   !string.IsNullOrWhiteSpace(data.PrivateDescription) &&
                   !string.IsNullOrWhiteSpace(data.PublicDescription) &&
                   data.Area > 0 &&
                   data.Price > 0;
        }
    }
}