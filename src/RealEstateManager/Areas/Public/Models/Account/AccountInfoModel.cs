using System;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountInfoModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        [Display(
            Name = "Public_AccountInfoModel_Username",
            ResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Display(
            Name = "Public_AccountInfoModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "Public_AccountInfoModel_PhoneNumber",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PhoneNumber { get; set; }

        [Display(
            Name = "Public_AccountInfoModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        public AccountUpdateData ToData()
        {
            return new AccountUpdateData
            {
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
            };
        }
    }
}
