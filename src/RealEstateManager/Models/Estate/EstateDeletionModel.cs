using System;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RealEstateManager.Models.EstateAccount;
using RealEstateManager.Repository.Data;

namespace RealEstateManager.Models.Estate
{
    public class EstateDeletionModel
    {
        [Required(
            ErrorMessageResourceName = "RequiredFieldError",
            ErrorMessageResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        public Guid? BuildingInfoId { get; set; }

        public List<EstateAccountModel> EstateAgents { get; set; }

        public EstateData ToData()
        {
            return new EstateData
            {
                Id = Id,
                EstateAgents = EstateAgents
            };
        }
    }
}