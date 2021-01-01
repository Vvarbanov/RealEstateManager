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
           Name = "ContactModel_DateTime",
           ResourceType = typeof(Resources))]
        [Required(
           ErrorMessageResourceName = "RequiredFieldError",
           ErrorMessageResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-y HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ContactDateTime { get; set; }

        public Guid EstateId { get; set; }

        public ContactData ToData()
        {
            return new ContactData
            {
                DateTime = ContactDateTime.Value,
                EstateId = EstateId,
                Number = string.Empty,
                Outcome = string.Empty,
                FilePathsCSV = null
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ContactDateTime.HasValue)
            {
                yield return new ValidationResult(Localization.GetString("ContactCreation_MissingDate_Error"),
                    new[] { nameof(ContactDateTime) });
            }
            else if (ContactDateTime.Value < DateTime.Now)
            {
                yield return new ValidationResult(Localization.GetString("ContactCreation_IncorrectDate_Error"),
                    new[] { nameof(ContactDateTime) });
            }
        }
    }
}
