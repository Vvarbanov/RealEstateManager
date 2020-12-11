using System.Collections.Generic;

namespace RealEstateManager.Areas.Public.Models.Estate
{
    public class EstateListGetModel
    {
        public EstateListGetModel() { }

        public EstateListGetModel(List<EstateGetModel> estates)
        {
            Estates = estates;
        }

        public EstateListGetModel(IEnumerable<RealEstateManager.Models.Data.Estate> estates)
        {
            Estates = new List<EstateGetModel>();

            foreach (var estate in estates)
            {
                Estates.Add(
                    new EstateGetModel
                    {
                        Id = estate.Id,
                        Name = estate.Name,
                        Type = estate.Type,
                        Price = estate.Price,
                        Status = estate.Status,
                        Address = estate.Address,
                        PublicDescription = estate.PublicDescription,
                        Area = estate.Area
                    });
            }
        }

        public List<EstateGetModel> Estates { get; set; }
    }
}