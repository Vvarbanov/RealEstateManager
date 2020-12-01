using System;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManager.Models.Estate
{
    public class EstateDeletionModel
    {
        public EstateDeletionModel() { }

        public EstateDeletionModel(Guid id)
        {
            Id = id;
        }

        [Display(
            Name = "EstateModel_Id",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }
    }
}