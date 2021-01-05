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
                FilePathsCSV = data.FilePathsCSV,
                UpdateDate = DateTime.Now,
            };

            _databaseContext.Estates.Add(estate);
            _databaseContext.SaveChanges();

            return estate;
        }

        public IQueryable<Estate> Get(
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
            estate.FilePathsCSV = data.FilePathsCSV;
            estate.UpdateDate = DateTime.Now;

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

        public void UpdateRights(Guid estateId, List<EstateAccountData> data)
        {
            var estate = _databaseContext.Estates
                .Include(nameof(Estate.EstateAccounts))
                .FirstOrDefault(x => x.Id == estateId);

            if (estate == null)
                throw new InvalidOperationException($"Estate with id {estateId} not found.");

            if (data.All(x => x.EstateId != estateId))
                throw new InvalidOperationException(
                    $"All elements in the {nameof(EstateAccountData)} " +
                    $"List must have the same {nameof(EstateAccountData.EstateId)}.");

            var estateAccountsToRemove = new List<EstateAccount>();

            var estateAccountsToAdd = new List<EstateAccount>();

            foreach (var item in data)
            {
                var estateAccountToUpdate = estate.EstateAccounts
                    .FirstOrDefault(x => x.EstateId == item.EstateId && x.AccountId == item.AccountId);

                if (estateAccountToUpdate != null && !item.HasRights)
                {
                    estateAccountsToRemove.Add(estateAccountToUpdate);
                }
                else if (estateAccountToUpdate == null && item.HasRights)
                {
                    estateAccountsToAdd.Add(new EstateAccount
                    {
                        AccountId = item.AccountId,
                        EstateId = item.EstateId,
                    });
                }
            }

            _databaseContext.EstateAccounts.RemoveRange(estateAccountsToRemove);
            _databaseContext.EstateAccounts.AddRange(estateAccountsToAdd);
            _databaseContext.SaveChanges();
        }

        public bool IsAccountAuthorized(CurrentIdentity account, Guid estateId)
        {
            var estate = _databaseContext.Estates
                .Include(nameof(Estate.EstateAccounts))
                .FirstOrDefault(x => x.Id == estateId);

            if (estate == null || account == null || account.Type == UserType.PublicUser)
                return false;

            if (account.Type == UserType.Admin)
                return true;

            return estate.EstateAccounts.Any(x => x.AccountId == account.Id);
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
