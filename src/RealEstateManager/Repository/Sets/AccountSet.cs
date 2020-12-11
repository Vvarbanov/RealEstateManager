using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateManager.Models.Data;
using RealEstateManager.Repository.Data;
using RealEstateManager.Repository.Interfaces;
using RealEstateManager.Utils;

namespace RealEstateManager.Repository.Sets
{
    public class AccountSet : ISet<Account, Guid, AccountInsertData, AccountUpdateData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public AccountSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Account Insert(AccountInsertData data)
        {
            if (!ValidateInsertData(data))
                throw new InvalidOperationException($"{nameof(AccountInsertData)} contains invalid values.");

            if (!IsUsernameAvailable(data.Username))
                throw new InvalidOperationException($"Username {data.Username} is already registered.");

            if (!IsEmailAddressAvailable(data.EmailAddress))
                throw new InvalidOperationException($"E-mail address {data.EmailAddress} is already registered.");

            var hashedPassword = PasswordHelper.GetHashedPassword(data.Password, out var salt);

            var account = new Account
            {
                Username = data.Username,
                EmailAddress = data.EmailAddress,
                HashedPassword = hashedPassword,
                PasswordSalt = salt,
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
                Type = data.Type,
            };

            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();

            return account;
        }

        public Account GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.Accounts
                .Where(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var includePropsArray = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProp in includePropsArray)
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<Account> Get(
            Expression<Func<Account, bool>> filter = null,
            Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = null, 
            string includeProperties = null)
        {
            var query = _databaseContext.Accounts.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                var includePropsArray = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProp in includePropsArray)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public void Update(Guid id, AccountUpdateData data)
        {
            if (!ValidateUpdateData(data))
                throw new InvalidOperationException($"{nameof(AccountUpdateData)} contains invalid values.");

            var account = GetById(id);

            if (account == null)
                throw new InvalidOperationException($"Account with id {id} not found.");

            account.EmailAddress = data.EmailAddress;
            account.PhoneNumber = data.PhoneNumber;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var account = GetById(id);

            if (account != null)
            {
                _databaseContext.Accounts.Remove(account);
                _databaseContext.SaveChanges();
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            var trimmedUsername = username.Trim();

            return !_databaseContext.Accounts
                .Any(x => x.Username == trimmedUsername);
        }

        public bool IsEmailAddressAvailable(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var trimmedEmail = email.Trim();

            return !_databaseContext.Accounts
                .Any(x => x.EmailAddress == trimmedEmail);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            var account = GetById(id);

            if (account == null)
                throw new InvalidOperationException($"Account with id {id} not found.");

            var hashedPassword = PasswordHelper.GetHashedPassword(newPassword, out var salt);

            account.HashedPassword = hashedPassword;
            account.PasswordSalt = salt;

            _databaseContext.SaveChanges();
        }

        public bool IsValidUser(string email, string password, out Guid id)
        {
            var account = _databaseContext.Accounts
                .SingleOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());

            if (account == null || 
                !PasswordHelper.ValidateHash(password, account.HashedPassword, account.PasswordSalt))
            {
                var failedLoginThrottle = new Random().Next(180, 220);

                Thread.Sleep(failedLoginThrottle);

                id = Guid.Empty;
                return false;
            }

            id = account.Id;
            return true;
        }

        public async Task<bool> TrySendForgottenPasswordEmailAsync(string email, string token, string recoveryUrl)
        {
            var trimmedEmail = email.Trim();

            var account = Get(x => x.EmailAddress.ToLower() == trimmedEmail.ToLower())
                .FirstOrDefault();

            if (account == null)
                return false;

            account.ForgottenPasswordToken = token;

            // TODO: Send asynchronous email

            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public bool TryRecoverAccount(string token, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(newPassword))
                return false;

            var trimmedToken = token.Trim();
            var trimmedPassword = newPassword.Trim();

            var account = Get(x => x.ForgottenPasswordToken.ToLower() == trimmedToken.ToLower())
                .FirstOrDefault();

            if (account == null)
                return false;

            var hashedPassword = PasswordHelper.GetHashedPassword(trimmedPassword, out var salt);

            account.HashedPassword = hashedPassword;
            account.PasswordSalt = salt;
            account.ForgottenPasswordToken = string.Empty;

            _databaseContext.SaveChanges();

            return true;
        }

        private static bool ValidateInsertData(AccountInsertData data)
        {
            return !string.IsNullOrWhiteSpace(data.LastName) &&
                   !string.IsNullOrWhiteSpace(data.FirstName) &&
                   !string.IsNullOrWhiteSpace(data.EmailAddress) &&
                   !string.IsNullOrWhiteSpace(data.Password) &&
                   !string.IsNullOrWhiteSpace(data.Username);
        }

        private static bool ValidateUpdateData(AccountUpdateData data)
        {
            return !string.IsNullOrWhiteSpace(data.EmailAddress) &&
                   !string.IsNullOrWhiteSpace(data.PhoneNumber);
        }
    }
}
