using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountRecoverPasswordModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Token { get; set; }

        [Display(
            Name = "Public_AccountRecoverPasswordModel_NewPassword",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string NewPassword { get; set; }
    }
}
