using System;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;

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
        public Guid UserId { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Username { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public bool HasRights { get; set; }
    }
}