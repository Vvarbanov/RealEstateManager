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
    public class BuildingInfoSet : ISet<BuildingInfo, Guid, BuildingInfoData, BuildingInfoData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public BuildingInfoSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public BuildingInfo Insert(BuildingInfoData data)
        {
            if (!ValidateInsertData(data))
                throw new InvalidOperationException("BuildingInfoData contains invalid properties.");

            var buildingInfo = new BuildingInfo
            {
                View = data.View,
                Act16 = data.Act16,
                Floors = data.Floors,
                Bedrooms = data.Bedrooms,
                Bathrooms = data.Bathrooms,
                Balconies = data.Balconies,
                Garages = data.Garages
            };
            var estate = _databaseContext.Estates.FirstOrDefault(x => x.Id == data.EstateId);
            if (estate == null)
                throw new InvalidOperationException("BuildingInfoData contains invalid properties. Estate id is incorrect");

            buildingInfo.Estates.Add(estate);

            _databaseContext.BuildingInfoes.Add(buildingInfo);
            _databaseContext.SaveChanges();

            return buildingInfo;
        }

        public IEnumerable<BuildingInfo> Get(
            Expression<Func<BuildingInfo, bool>> filter = null,
            Func<IQueryable<BuildingInfo>, IOrderedQueryable<BuildingInfo>> orderBy = null,
            string includeProperties = null)
        {
            var query = _databaseContext.BuildingInfoes.AsQueryable();

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

        public BuildingInfo GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.BuildingInfoes
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

        public void Update(Guid id, BuildingInfoData data)
        {
            var buildingInfo = GetById(id);

            if (buildingInfo == null)
                throw new InvalidOperationException($"Building Info with id {id} not found.");

            if (!ValidateInsertData(data))
                throw new InvalidOperationException("BuildingInfoData contains invalid properties.");

            buildingInfo.View = data.View;
            buildingInfo.Act16 = data.Act16;
            buildingInfo.Floors = data.Floors;
            buildingInfo.Bedrooms = data.Bedrooms;
            buildingInfo.Bathrooms = data.Bathrooms;
            buildingInfo.Balconies = data.Balconies;
            buildingInfo.Garages = data.Garages;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var buildingInfo = GetById(id);

            if (buildingInfo == null)
                throw new InvalidOperationException($"Building Info with id {id} not found.");

            _databaseContext.BuildingInfoes.Remove(buildingInfo);
            _databaseContext.SaveChanges();
        }

        private static bool ValidateInsertData(BuildingInfoData data)
        {
            return data.Floors > 0 &&
                   data.Bedrooms >= 0 &&
                   data.Bathrooms >= 0 &&
                   data.Balconies >= 0 &&
                   data.Garages >= 0;
        }
    }
}