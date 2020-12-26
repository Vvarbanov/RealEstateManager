using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using System;

namespace RealEstateManager.Models.Contact
{
    public class ContactCreationModel
    {
        [Display(
            Name = "EstateModel_Name",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public DateTime DateTime { get; set; }

        public ContactData ToData()
        {
            return new ContactData
            {
                DateTime = DateTime,
                Number = string.Empty,
                Outcome = string.Empty
            };
        }
    }
}
