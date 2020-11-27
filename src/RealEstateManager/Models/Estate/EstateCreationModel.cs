using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManager.Models.Estate
{
    public class EstateCreationModel : IValidatableObject
    {
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

        [Display(
            Name = "EstateCreationModel_BuildingInfoId",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid BuildingInfoId { get; set; }

        public EstateData ToData()
        {
            return new EstateData
            {
                Name = Name,
                Address = Address,
                Area = Area,
                BuildingInfoId = BuildingInfoId,
                Price = Price,
                PrivateDescription = PrivateDescription,
                PublicDescription = PublicDescription,
                Status = Status,
                Type = Type
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}