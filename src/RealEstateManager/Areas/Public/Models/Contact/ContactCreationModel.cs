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
    public class ContactCreationModel
    {
        [Display(
            Name = "EstateModel_Name",
            ResourceType = typeof(Resources))]
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public string Number { get; set; }

        public string Outcome { get; set; }

        public ContactData ToData()
        {
            return new ContactData
            {
                DateTime = DateTime,
                Number = Number,
                Outcome = Outcome,
            };
        }
    }
}
