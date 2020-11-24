using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using RealEstateManager.Utils;

namespace RealEstateManager.Models.Agent
{
    public class AgentRegisterModel : IValidatableObject
    {
        [Display(
            Name = "AgentRegisterModel_Username",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Display(
            Name = "AgentRegisterModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "AgentRegisterModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Display(
            Name = "AgentRegisterModel_FirstName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        [Display(
            Name = "AgentRegisterModel_LastName",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string LastName { get; set; }

        [Display(
            Name = "AgentRegisterModel_PhoneNumber",
            ResourceType = typeof(Resources))]
        public string PhoneNumber { get; set; }

        [Display(
            Name = "AgentRegisterModel_RegistrationKey",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string RegistrationKey { get; set; }

        public AgentInsertData ToData()
        {
            return new AgentInsertData
            {
                Username = Username,
                EmailAddress = EmailAddress,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guid.TryParse(RegistrationKey, out var result))
            {
                if (result != ConfigReader.AgentRegistrationKey)
                {
                    yield return new ValidationResult(Localization.GetString("AgentRegister_IncorrectKey_Error"),
                        new[] {nameof(RegistrationKey)});
                }
            }
            else
            {
                yield return new ValidationResult(Localization.GetString("AgentRegister_IncorrectKey_Error"),
                    new[] { nameof(RegistrationKey) });
            }
        }
    }
}
