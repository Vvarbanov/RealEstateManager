using System;
using System.Collections.Generic;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;
using RealEstateManager.Utils;
using System.Linq;
using System.IO;

namespace RealEstateManager.Models.Contact
{
    public class ContactUpdateModel
    {
        public Guid Id { get; set; }

        public Guid EstateId { get; set; }

        [Display(
            Name = "ContactModel_DateTime",
            ResourceType = typeof(Resources))]
        public DateTime DateTime { get; set; }

        [Display(
            Name = "ContactModel_Outcome",
            ResourceType = typeof(Resources))]
        public string Outcome { get; set; }

        [Display(
            Name = "ContactModel_Number",
            ResourceType = typeof(Resources))]
        public string Number { get; set; }

        [Display(
            Name = "ContactUpdateModel_ImagePaths",
            ResourceType = typeof(Resources))]
        public List<string> ExistingImagePaths { get; set; }

        [Display(
            Name = "ContactUpdateModel_Images",
            ResourceType = typeof(Resources))]
        public List<HttpPostedFileBase> Images { get; set; }

        public ContactImageModel[] GetSafeImages(HttpServerUtilityBase server)
        {
            return Images
                .Where(x => x != null &&
                    MimeMapping.GetMimeMapping(x.FileName).StartsWith("image/") &&
                    !string.IsNullOrWhiteSpace(Path.GetExtension(x.FileName)))
                .Select(x => new ContactImageModel
                {
                    File = x,
                    SaveLocation = Path.Combine(
                        server.MapPath(ConfigReader.ImageUploadDirectory),
                        $"{Guid.NewGuid()}{Path.GetExtension(x.FileName)}")
                })
                .ToArray();
        }

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

        public ContactData ToData(string newAndExistingFilesPathsCSV = null)
        {
            return new ContactData
            {
                DateTime = DateTime,
                EstateId = EstateId,
                Number = Number,
                Outcome = Outcome,
                FilePathsCSV = newAndExistingFilesPathsCSV
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime < DateTime.Now)
            {
                yield return new ValidationResult(Localization.GetString("ContactCreation_IncorrectDate_Error"),
                    new[] { nameof(DateTime) });
            }
        }
    }
}