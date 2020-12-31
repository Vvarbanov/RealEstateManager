using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using RealEstateManager.Repository.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RealEstateManager.Repository.Sets
{
    public class ContactAccountSet : ISet<ContactAccount, Guid, ContactAccountData, ContactAccountData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public ContactAccountSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ContactAccount Insert(ContactAccountData data)
        {
            if (Get(x => x.AccountId == data.AccountId && x.ContactId == data.ContactId).Any())
                throw new InvalidOperationException($"ContactAccount for this contact id ${data.ContactId} and user id ${data.AccountId} already exists.");

            var contactAccount = new ContactAccount
            {
                AccountId = data.AccountId,
                ContactId = data.ContactId
            };

            _databaseContext.ContactAccounts.Add(contactAccount);
            _databaseContext.SaveChanges();

            return contactAccount;
        }

        public IQueryable<ContactAccount> Get(
            Expression<Func<ContactAccount, bool>> filter = null,
            Func<IQueryable<ContactAccount>, IOrderedQueryable<ContactAccount>> orderBy = null,
            string includeProperties = null)
        {
            var query = _databaseContext.ContactAccounts.AsQueryable();

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

        public ContactAccount GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.ContactAccounts
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

        public void Update(Guid id, ContactAccountData data)
        {
            var contactAccount = GetById(id);

            if (contactAccount == null)
                throw new InvalidOperationException($"ContactAccount with id {id} not found.");

            contactAccount.AccountId = data.AccountId;
            contactAccount.ContactId = data.ContactId;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var contactAccount = GetById(id);

            if (contactAccount == null)
                throw new InvalidOperationException($"ContactAccount with id {id} not found.");

            _databaseContext.ContactAccounts.Remove(contactAccount);
            _databaseContext.SaveChanges();
        }
    }
}