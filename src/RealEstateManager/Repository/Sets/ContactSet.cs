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
    public class ContactSet : ISet<Contact, Guid, ContactData, ContactData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public ContactSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Contact Insert(ContactData data)
        {
            var contact = new Contact
            {
                DateTime = data.DateTime,
                Number = data.Number,
                Outcome = data.Outcome,
                FilePathsCSV = data.FilePathsCSV
            };

            _databaseContext.Contacts.Add(contact);
            _databaseContext.SaveChanges();

            return contact;
        }

        public IQueryable<Contact> Get(
            Expression<Func<Contact, bool>> filter = null,
            Func<IQueryable<Contact>, IOrderedQueryable<Contact>> orderBy = null,
            string includeProperties = null)
        {
            var query = _databaseContext.Contacts.AsQueryable();

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

        public Contact GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.Contacts
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

        public void Update(Guid id, ContactData data)
        {
            var contact = GetById(id);

            if (contact == null)
                throw new InvalidOperationException($"Contact with id {id} not found.");

            if (!ValidateInsertData(data))
                throw new InvalidOperationException("ContactData contains invalid properties.");

            contact.DateTime = data.DateTime;
            contact.Number = data.Number;
            contact.Outcome = data.Outcome;
            contact.FilePathsCSV = data.FilePathsCSV;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var contact = GetById(id);

            if (contact == null)
                throw new InvalidOperationException($"Contact with id {id} not found.");

            _databaseContext.Contacts.Remove(contact);
            _databaseContext.SaveChanges();
        }

        private static bool ValidateInsertData(ContactData data)
        {
            return !string.IsNullOrWhiteSpace(data.Outcome) &&
                   !string.IsNullOrWhiteSpace(data.Number);
        }
    }
}