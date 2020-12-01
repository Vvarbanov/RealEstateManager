using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateManager.Properties;
using System.ComponentModel.DataAnnotations;
using RealEstateManager.Models.Data;

namespace RealEstateManager.Models.Estate
{
    public class EstateGetModel
    {
        public EstateGetModel() { }

        public EstateGetModel(string name, EstateType type, decimal price, EstateStatusType status, string publicDescription, double area)
        {
            Name = name;
            Type = type;
            Price = price;
            Status = status;
            PublicDescription = publicDescription;
            Area = area;
        }

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
            Name = "EstateModel_PublicDescription",
            ResourceType = typeof(Resources))]
        public string PublicDescription { get; set; }

        [Display(
            Name = "EstateModel_Area",
            ResourceType = typeof(Resources))]
        public double Area { get; set; }
    }
}