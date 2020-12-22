using System;
using System.Collections.Generic;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;
using RealEstateManager.Utils;

namespace RealEstateManager.Models.Estate
{
    public class EstateUpdateModel : EstateBaseModel, IValidatableObject
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        [Display(
            Name = "EstateModel_Name",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(
            Name = "EstateModel_Type",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public EstateType Type { get; set; }

        [Display(
            Name = "EstateModel_Address",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string Address { get; set; }

        [Display(
            Name = "EstateModel_Price",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public decimal Price { get; set; }

        [Display(
            Name = "EstateModel_Status",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public EstateStatusType Status { get; set; }

        [Display(
            Name = "EstateModel_PublicDescription",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PublicDescription { get; set; }

        [Display(
            Name = "EstateModel_PrivateDescription",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public string PrivateDescription { get; set; }

        [Display(
            Name = "EstateModel_Area",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public double Area { get; set; }

        [Display(
            Name = "EstateUpdateModel_ImagePaths",
            ResourceType = typeof(Resources))]
        public List<string> ExistingImagePaths { get; set; }

        [Display(
            Name = "EstateUpdateModel_Images",
            ResourceType = typeof(Resources))]
        public override List<HttpPostedFileBase> Images { get; set; }

        public Guid? BuildingInfoId { get; set; }

        public string ExistingImagePathsAsCSV(HttpServerUtilityBase server)
        {
            string filesPathCSV = null;

            if (ExistingImagePaths != null)
            {
                for (var i = 0; i < ExistingImagePaths.Count; ++i)
                {
                    var saveLocation = server.MapPath(ExistingImagePaths[i]);

                    if (i < ExistingImagePaths.Count - 1)
                        filesPathCSV += saveLocation + ",";
                    else
                        filesPathCSV += saveLocation;
                }
            }

            return filesPathCSV;
        }

        public EstateData ToData(string newAndExistingFilesPathsCSV = null)
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
                Type = Type,
                FilePathsCSV = newAndExistingFilesPathsCSV,
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
