using System.ComponentModel.DataAnnotations;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountRegisterPublicUserModel
    {
        [Display(
            Name = "Public_AccountRegisterPublicUserModel_Username",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Display(
            Name = "Public_AccountRegisterPublicUserModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "Public_AccountRegisterPublicUserModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Display(
            Name = "Public_AccountRegisterPublicUserModel_FirstName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        [Display(
            Name = "Public_AccountRegisterPublicUserModel_LastName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string LastName { get; set; }

        [Display(
            Name = "Public_AccountRegisterPublicUserModel_PhoneNumber",
            ResourceType = typeof(Resources))]
        public string PhoneNumber { get; set; }

        public AccountInsertData ToData()
        {
            return new AccountInsertData
            {
                Username = Username,
                EmailAddress = EmailAddress,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Type = UserType.PublicUser,
            };
        }
    }
}
