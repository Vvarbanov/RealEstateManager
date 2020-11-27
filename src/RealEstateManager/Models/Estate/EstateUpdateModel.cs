using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Utils;

namespace RealEstateManager.Models.Estate
{
    public class EstateUpdateModel : IValidatableObject
    {
        [Display(
            Name = "EstateCreationModel_Id",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        [Display(
            Name = "EstateCreationModel_Name",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(
            Name = "EstateCreationModel_Type",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public EstateType Type { get; set; }

        [Display(
            Name = "EstateCreationModel_Address",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Address { get; set; }

        [Display(
            Name = "EstateCreationModel_Price",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public decimal Price { get; set; }

        [Display(
            Name = "EstateCreationModel_Status",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public EstateStatusType Status { get; set; }

        [Display(
            Name = "EstateCreationModel_PublicDescription",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PublicDescription { get; set; }

        [Display(
            Name = "EstateCreationModel_PrivateDescription",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PrivateDescription { get; set; }

        [Display(
            Name = "EstateCreationModel_Area",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public double Area { get; set; }

        public EstateData ToData()
        {
            return new EstateData
            {
                Name = Name,
                Address = Address,
                Area = Area,
                Price = Price,
                PrivateDescription = PrivateDescription,
                PublicDescription = PublicDescription,
                Status = Status,
                Type = Type
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Area < 0)
            {
                yield return new ValidationResult(Localization.GetString("EstateCreation_IncorrectPrice_Error"),
                    new[] { nameof(Area) });
            }

            if (Price < 0)
            {
                yield return new ValidationResult(Localization.GetString("EstateCreation_IncorrectArea_Error"),
                    new[] { nameof(Price) });
            }
        }
    }
}