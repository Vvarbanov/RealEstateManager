using System;
using System.Collections.Generic;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Models.Data;
using RealEstateManager.Areas.Public.Models.BuildingInfo;

namespace RealEstateManager.Areas.Public.Models.Estate
{
    public class EstateGetModel
    {
        public Guid Id { get; set; }

        [Display(
            Name = "EstateModel_Name",
            ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(
            Name = "EstateModel_Type",
            ResourceType = typeof(Resources))]
        public EstateType Type { get; set; }

        [Display(
            Name = "EstateModel_Price",
            ResourceType = typeof(Resources))]
        public decimal Price { get; set; }

        [Display(
            Name = "EstateModel_Status",
            ResourceType = typeof(Resources))]
        public EstateStatusType Status { get; set; }

        [Display(
            Name = "EstateModel_Address",
            ResourceType = typeof(Resources))]
        public string Address { get; set; }

        [Display(
            Name = "EstateModel_PublicDescription",
            ResourceType = typeof(Resources))]
        public string PublicDescription { get; set; }

        [Display(
            Name = "EstateModel_Area",
            ResourceType = typeof(Resources))]
        public double Area { get; set; }

        [Display(
            Name = "EstateGetModel_ImagePaths",
            ResourceType = typeof(Resources))]
        public List<string> ExistingImagePaths { get; set; }

        public BuildingInfoGetModel BuildingInfoGetModel { get; set; }
    }
}
