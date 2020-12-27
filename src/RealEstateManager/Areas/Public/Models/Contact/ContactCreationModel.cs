using System.Collections.Generic;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;
using RealEstateManager.Utils;
using System;

namespace RealEstateManager.Areas.Public.Models.Contact
{
    public class ContactCreationModel : IValidatableObject
    {
        [Display(
           Name = "EstateModel_Name",
           ResourceType = typeof(Resources))]
        [Required(
           ErrorMessageResourceName = "RequiredFieldError",
           ErrorMessageResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public Guid EstateId { get; set; }

        public ContactData ToData()
        {
            return new ContactData
            {
                DateTime = DateTime,
                EstateId = EstateId,
                Number = string.Empty,
                Outcome = string.Empty,
                FilePathsCSV = null
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
