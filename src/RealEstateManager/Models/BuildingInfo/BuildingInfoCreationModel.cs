using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Utils;

namespace RealEstateManager.Models.BuildingInfo
{
    public class BuildingInfoCreationModel : IValidatableObject
    {
        [Display(
            Name = "BuildingInfoModel_View",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public BuildingViewType View { get; set; }

        [Display(
            Name = "BuildingInfoModel_Act16",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public bool Act16 { get; set; }

        [Display(
            Name = "BuildingInfoModel_Floors",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public int Floors { get; set; }

        [Display(
            Name = "BuildingInfoModel_Bedrooms",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public int Bedrooms { get; set; }

        [Display(
            Name = "BuildingInfoModel_Bathrooms",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public int Bathrooms { get; set; }

        [Display(
            Name = "BuildingInfoModel_Balconies",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public int Balconies { get; set; }

        [Display(
            Name = "BuildingInfoModel_Garages",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public int Garages { get; set; }

        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid EstateId { get; set; }

        public BuildingInfoData ToData()
        {
            return new BuildingInfoData
            {
                View = View,
                Act16 = Act16,
                Floors = Floors,
                Bedrooms = Bedrooms,
                Bathrooms = Bathrooms,
                Balconies = Balconies,
                Garages = Garages,
                EstateId = EstateId
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Floors <= 0)
            {
                yield return new ValidationResult(Localization.GetString("BuildingInfoCreation_IncorrectFloors_Error"),
                    new[] { nameof(Floors) });
            }
            if (Bedrooms < 0)
            {
                yield return new ValidationResult(Localization.GetString("BuildingInfoCreation_IncorrectBedrooms_Error"),
                    new[] { nameof(Bedrooms) });
            }
            if (Bathrooms < 0)
            {
                yield return new ValidationResult(Localization.GetString("BuildingInfoCreation_IncorrectBathrooms_Error"),
                    new[] { nameof(Bathrooms) });
            }
            if (Balconies < 0)
            {
                yield return new ValidationResult(Localization.GetString("BuildingInfoCreation_IncorrectBalconies_Error"),
                    new[] { nameof(Balconies) });
            }
            if (Garages < 0)
            {
                yield return new ValidationResult(Localization.GetString("BuildingInfoCreation_IncorrectGarages_Error"),
                    new[] { nameof(Garages) });
            }
        }
    }
}