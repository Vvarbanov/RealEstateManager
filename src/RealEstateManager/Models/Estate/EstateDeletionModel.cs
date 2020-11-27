using System;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManager.Models.Estate
{
    public class EstateDeletionModel
    {
        [Display(
            Name = "EstateCreationModel_Id",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid id { get; set; }
    }
}