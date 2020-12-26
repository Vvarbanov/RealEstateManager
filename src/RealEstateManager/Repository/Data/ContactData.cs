using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateManager.Repository.Data
{
    public class ContactData
    {
        public DateTime DateTime { get; set; }

        public string Number { get; set; }

        public string Outcome { get; set; }

        public Guid EstateId { get; set; }

        public string FilePathsCSV { get; set; }
    }
}