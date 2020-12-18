using System;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Properties;

namespace RealEstateManager.Areas.Public.Models.Account
{
    public class AccountDeleteModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }
    }
}
