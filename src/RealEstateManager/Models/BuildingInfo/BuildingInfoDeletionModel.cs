using System;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManager.Models.BuildingInfo
{
    public class BuildingInfoDeletionModel
    {
        public BuildingInfoDeletionModel() { }

        public BuildingInfoDeletionModel(Guid id)
        {
            Id = id;
        }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }
    }
}