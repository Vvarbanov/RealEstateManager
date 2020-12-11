using System;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Repository.Data
{
    public class BuildingInfoData
    {
        public BuildingViewType View { get; set; }

        public bool Act16 { get; set; }

        public int Floors { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int Balconies { get; set; }

        public int Garages { get; set; }

        public Guid EstateId { get; set; }
    }
}