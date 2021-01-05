using RealEstateManager.Models.Data;

namespace RealEstateManager.Repository.Data
{
    public class EstateData
    {
        public string Name { get; set; }

        public EstateType Type { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public EstateStatusType Status { get; set; }

        public string PublicDescription { get; set; }

        public string PrivateDescription { get; set; }

        public double Area { get; set; }

        public string FilePathsCSV { get; set; }
    }
}
