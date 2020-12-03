using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Models.Agent
{
    public class AgentRecoverPasswordModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Token { get; set; }

        [Display(
            Name = "AgentRecoverPasswordModel_NewPassword",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string NewPassword { get; set; }
    }
}
