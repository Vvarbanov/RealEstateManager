using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Models.Agent
{
    public class AgentForgottenPasswordModel
    {
        [Display(
            Name = "AgentForgottenPasswordModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }
    }
}
