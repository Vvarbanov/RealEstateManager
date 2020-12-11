using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountRegisterAdminModel : IValidatableObject
    {
        [Display(
            Name = "Public_AccountRegisterAdminModel_Username",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_FirstName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_LastName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string LastName { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_PhoneNumber",
            ResourceType = typeof(Resources))]
        public string PhoneNumber { get; set; }

        [Display(
            Name = "Public_AccountRegisterAdminModel_RegistrationKey",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string RegistrationKey { get; set; }

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
                Type = UserType.Admin,
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.TryParse(RegistrationKey, out var result))
            {
                if (result != ConfigReader.AdminRegistrationKey)
                {
                    yield return new ValidationResult(Localization.GetString("Public_AccountRegister_IncorrectKey_Error"),
                        new[] { nameof(RegistrationKey) });
                }
            }
            else
            {
                yield return new ValidationResult(Localization.GetString("Public_AccountRegister_IncorrectKey_Error"),
                    new[] { nameof(RegistrationKey) });
            }
        }
    }
}
