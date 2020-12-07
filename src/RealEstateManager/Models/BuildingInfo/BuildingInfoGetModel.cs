using System;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.Models.Data;
using RealEstateManager.Properties;
using RealEstateManager.Repository.Data;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Utils;
namespace RealEstateManager.Models.BuildingInfo
{
    public class BuildingInfoGetModel
    {
        [Display(
            Name = "BuildingInfoModel_View",
            ResourceType = typeof(Resources))]
        public BuildingViewType View { get; set; }

        [Display(
            Name = "BuildingInfoModel_Act16",
            ResourceType = typeof(Resources))]
        public bool Act16 { get; set; }

        [Display(
            Name = "BuildingInfoModel_Floors",
            ResourceType = typeof(Resources))]
        public int Floors { get; set; }

        [Display(
            Name = "BuildingInfoModel_Bedrooms",
            ResourceType = typeof(Resources))]
        public int Bedrooms { get; set; }

        [Display(
            Name = "BuildingInfoModel_Bathrooms",
            ResourceType = typeof(Resources))]
        public int Bathrooms { get; set; }

        [Display(
            Name = "BuildingInfoModel_Balconies",
            ResourceType = typeof(Resources))]
        public int Balconies { get; set; }

        [Display(
            Name = "BuildingInfoModel_Garages",
            ResourceType = typeof(Resources))]
        public int Garages { get; set; }
    }
}