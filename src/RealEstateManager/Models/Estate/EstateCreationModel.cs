using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManager.Models.Estate
{
    public class EstateCreationModel : IValidatableObject
    {
        public string Name { get; set; }

        public EstateType Type { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public EstateStatusType Status { get; set; }

        public string PublicDescription { get; set; }

        public string PrivateDescription { get; set; }

        public double Area { get; set; }

        public Guid BuildingInfoId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}