using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
                throw new InvalidOperationException("AgentInsertData contains invalid properties.");

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
                return query
                    .Include(includeProperties)
                    .FirstOrDefault();
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<Agent> Get(
            Expression<Func<Agent, bool>> filter = null,
            Func<IQueryable<Agent>, IOrderedQueryable<Agent>> orderBy = null, 
            string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, AgentUpdateData data)
        {
            var agent = GetById(id);

            if (agent == null)
                throw new InvalidOperationException($"Agent with id {id} not found.");

            agent.Username = data.Username;
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
                throw new InvalidOperationException("Username cannot be an empty string.");

            var trimmedUsername = username.Trim();

            return !_databaseContext.Agents
                .Any(x => x.Username == trimmedUsername);
        }

        public bool IsEmailAddressAvailable(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("E-mail address cannot be an empty string.");

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

        public bool IsValidAgent(string email, string password, out string username)
        {
            var agent = _databaseContext.Agents
                .FirstOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());

            if (agent == null || 
                !PasswordHelper.ValidateHash(password, agent.HashedPassword, agent.PasswordSalt))
            {
                var failedLoginThrottle = new Random().Next(180, 220);

                Thread.Sleep(failedLoginThrottle);

                username = null;
                return false;
            }

            username = agent.Username;
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
    }
}
