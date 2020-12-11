using RealEstateManager.Models.Data;

namespace RealEstateManager.Repository.Data
{
    public class AccountInsertData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }

    public class AccountUpdateData
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
