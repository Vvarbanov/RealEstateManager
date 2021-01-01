using System;
using System.Collections.Generic;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Models.Data;
using RealEstateManager.Models.BuildingInfo;
using RealEstateManager.Models.EstateAccount;

namespace RealEstateManager.Models.Contact
{
    public class ContactGetModel
    {
        public Guid Id { get; set; }

        public Guid EstateId { get; set; }

        [Display(
            Name = "ContactModel_DateTime",
            ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-y HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ContactDateTime { get; set; }

        [Display(
            Name = "ContactModel_Outcome",
            ResourceType = typeof(Resources))]
        public string Outcome { get; set; }

        [Display(
            Name = "ContactModel_Number",
            ResourceType = typeof(Resources))]
        public string Number { get; set; }

        [Display(
            Name = "ContactModel_ImagePaths",
            ResourceType = typeof(Resources))]
        public List<string> ImagePaths { get; set; }

        public List<ContactAccount> ContactAccounts { get; set; }
    }
}