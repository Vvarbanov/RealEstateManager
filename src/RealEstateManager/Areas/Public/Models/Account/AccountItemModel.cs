using System;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountItemModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public UserType Type { get; set; }

        public string EmailAddress { get; set; }
    }
}
