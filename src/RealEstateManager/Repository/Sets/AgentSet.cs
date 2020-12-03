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
    public class AgentSet : ISet<Agent, Guid, AgentInsertData, AgentUpdateData>
    {
        private readonly RealEstateManagerDataModelContainer _databaseContext;

        public AgentSet(RealEstateManagerDataModelContainer databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Agent Insert(AgentInsertData data)
        {
            if (!ValidateInsertData(data))
                throw new InvalidOperationException("AgentInsertData contains invalid values.");

            if (!IsUsernameAvailable(data.Username))
                throw new InvalidOperationException($"Username {data.Username} is already registered.");

            if (!IsEmailAddressAvailable(data.EmailAddress))
                throw new InvalidOperationException($"E-mail address {data.EmailAddress} is already registered.");

            var hashedPassword = PasswordHelper.GetHashedPassword(data.Password, out var salt);

            var agent = new Agent
            {
                Username = data.Username,
                EmailAddress = data.EmailAddress,
                HashedPassword = hashedPassword,
                PasswordSalt = salt,
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
            };

            _databaseContext.Agents.Add(agent);
            _databaseContext.SaveChanges();

            return agent;
        }

        public Agent GetById(Guid id, string includeProperties = null)
        {
            var query = _databaseContext.Agents
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

        public IEnumerable<Agent> Get(
            Expression<Func<Agent, bool>> filter = null,
            Func<IQueryable<Agent>, IOrderedQueryable<Agent>> orderBy = null, 
            string includeProperties = null)
        {
            var query = _databaseContext.Agents.AsQueryable();

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

        public void Update(Guid id, AgentUpdateData data)
        {
            if (!ValidateUpdateData(data))
                throw new InvalidOperationException("AgentUpdateData contains invalid values.");

            var agent = GetById(id);

            if (agent == null)
                throw new InvalidOperationException($"Agent with id {id} not found.");

            agent.EmailAddress = data.EmailAddress;
            agent.PhoneNumber = data.PhoneNumber;

            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var agent = GetById(id);

            if (agent != null)
            {
                _databaseContext.Agents.Remove(agent);
                _databaseContext.SaveChanges();
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            var trimmedUsername = username.Trim();

            return !_databaseContext.Agents
                .Any(x => x.Username == trimmedUsername);
        }

        public bool IsEmailAddressAvailable(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var trimmedEmail = email.Trim();

            return !_databaseContext.Agents
                .Any(x => x.EmailAddress == trimmedEmail);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            var agent = GetById(id);

            if (agent == null)
                throw new InvalidOperationException($"Agent with id {id} not found.");

            var hashedPassword = PasswordHelper.GetHashedPassword(newPassword, out var salt);

            agent.HashedPassword = hashedPassword;
            agent.PasswordSalt = salt;

            _databaseContext.SaveChanges();
        }

        public bool IsValidAgent(string email, string password, out string username, out Guid id)
        {
            var agent = _databaseContext.Agents
                .FirstOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());

            username = agent?.Username;

            if (agent == null || 
                !PasswordHelper.ValidateHash(password, agent.HashedPassword, agent.PasswordSalt))
            {
                var failedLoginThrottle = new Random().Next(180, 220);

                Thread.Sleep(failedLoginThrottle);

                id = Guid.Empty;
                username = null;
                return false;
            }

            id = agent.Id;
            username = agent?.Username;
            return true;
        }

        public async Task<bool> TrySendForgottenPasswordEmailAsync(string email, string token, string recoveryUrl)
        {
            var trimmedEmail = email.Trim();

            var agent = Get(x => x.EmailAddress.ToLower() == trimmedEmail.ToLower())
                .FirstOrDefault();

            if (agent == null)
                return false;

            agent.ForgottenPasswordToken = token;

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

            var agent = Get(x => x.ForgottenPasswordToken.ToLower() == trimmedToken.ToLower())
                .FirstOrDefault();

            if (agent == null)
                return false;

            var hashedPassword = PasswordHelper.GetHashedPassword(trimmedPassword, out var salt);

            agent.HashedPassword = hashedPassword;
            agent.PasswordSalt = salt;
            agent.ForgottenPasswordToken = string.Empty;

            _databaseContext.SaveChanges();

            return true;
        }

        private static bool ValidateInsertData(AgentInsertData data)
        {
            return !string.IsNullOrWhiteSpace(data.LastName) &&
                   !string.IsNullOrWhiteSpace(data.FirstName) &&
                   !string.IsNullOrWhiteSpace(data.EmailAddress) &&
                   !string.IsNullOrWhiteSpace(data.Password) &&
                   !string.IsNullOrWhiteSpace(data.Username);
        }

        private static bool ValidateUpdateData(AgentUpdateData data)
        {
            return !string.IsNullOrWhiteSpace(data.EmailAddress) &&
                   !string.IsNullOrWhiteSpace(data.PhoneNumber);
        }
    }
}
