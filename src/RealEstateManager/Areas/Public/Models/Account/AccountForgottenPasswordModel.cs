using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountForgottenPasswordModel
    {
        [Display(
            Name = "Public_AccountForgottenPasswordModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }
    }
}
