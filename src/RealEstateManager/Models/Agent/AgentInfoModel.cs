using System;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Models.Agent
{
    public class AgentInfoModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        [Display(
            Name = "AgentInfoModel_Username",
            ResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Display(
            Name = "AgentInfoModel_EmailAddress",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string EmailAddress { get; set; }

        [Display(
            Name = "AgentInfoModel_PhoneNumber",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PhoneNumber { get; set; }

        [Display(
            Name = "AgentInfoModel_Password",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }

        public AgentUpdateData ToUpdateData()
        {
            return new AgentUpdateData
            {
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
            };
        }
    }
}
