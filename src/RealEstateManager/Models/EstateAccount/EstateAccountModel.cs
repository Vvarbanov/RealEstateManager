using System;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Models.EstateAccount
{
    public class EstateAccountModel
    {
        [Required(
           ErrorMessageResourceName = "RequiredFieldError",
           ErrorMessageResourceType = typeof(Resources))]
        public Guid EstateId { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid AccountId { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        [Display(
            Name = "EstateAccountModel_HasRights",
            ResourceType = typeof(Resources))]
        public bool HasRights { get; set; }

        public EstateAccountData ToData()
        {
            return new EstateAccountData
            {
                AccountId = AccountId,
                EstateId = EstateId,
                HasRights = HasRights,
            };
        }
    }
}
