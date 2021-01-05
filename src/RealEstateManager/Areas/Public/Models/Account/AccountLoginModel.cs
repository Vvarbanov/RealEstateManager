using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountLoginModel
    {
        [Display(
            Name = "Public_AccountLoginModel_Email",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        [EmailAddress(
            ErrorMessageResourceName = "EmailFormatError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "Public_AccountLoginModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Display(
            Name = "Public_AccountLoginModel_RememberMe",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public bool RememberMe { get; set; }
    }
}
