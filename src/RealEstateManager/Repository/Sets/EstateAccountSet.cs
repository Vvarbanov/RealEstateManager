using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using RealEstateManager.Models.EstateAccount;

namespace RealEstateManager.Repository.Sets
{
    public class EstateAccountSet
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public EstateAccountSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public EstateAccount Insert(EstateAccountData data)
        {
            var estateAccount = new EstateAccount
            {
                AccountId = data.AccountId,
                EstateId = data.EstateId
            };

            _databaseContext.EstateAccounts.Add(estateAccount);
            _databaseContext.SaveChanges();

            return estateAccount;
        }

        public IEnumerable<EstateAccount> Get(
            Expression<Func<EstateAccount, bool>> filter = null,
            Func<IQueryable<EstateAccount>, IOrderedQueryable<EstateAccount>> orderBy = null,
            string includeProperties = null)
        {
            var query = _databaseContext.EstateAccounts.AsQueryable();

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

        public EstateAccount GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.EstateAccounts
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

        public EstateAccount getExisting(Guid estateId, Guid accountId, string includeProperties = null)
        {
            var query = _databaseContext.EstateAccounts
                .Where(x => x.EstateId == estateId && x.AccountId == accountId);

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

        public void Update(EstateAccountListModel data)
        {
            foreach (var model in data.EstateAgents)
            {
                if (model.HasRights &&
                    getExisting(model.EstateId, model.UserId) == null)
                {
                    Insert(new EstateAccountData
                    {
                        EstateId = model.EstateId,
                        AccountId = model.UserId
                    });
                }

                if (!model.HasRights &&
                    getExisting(model.EstateId, model.UserId) != null)
                {
                    Delete(new EstateAccountData
                    {
                        EstateId = model.EstateId,
                        AccountId = model.UserId
                    });
                }
            }

            _databaseContext.SaveChanges();
        }

        public void Delete(EstateAccountData data)
        {
            var estateAccount = getExisting(data.EstateId, data.AccountId);

            if (estateAccount == null)
                throw new InvalidOperationException($"EstateAccount not found.");

            _databaseContext.EstateAccounts.Remove(estateAccount);
            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var estateAccount = GetById(id);

            if (estateAccount == null)
                throw new InvalidOperationException($"EstateAccount with id {id} not found.");

            _databaseContext.EstateAccounts.Remove(estateAccount);
            _databaseContext.SaveChanges();
        }
    }
}
