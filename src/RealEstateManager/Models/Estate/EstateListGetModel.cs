using System.Collections.Generic;

namespace RealEstateManager.Models.Estate
{
    public class EstateListGetModel
    {
        public EstateListGetModel() { }

        public EstateListGetModel(List<EstateGetModel> estates)
        {
            Estates = estates;
        }

        public EstateListGetModel(IEnumerable<Data.Estate> estates)
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
                        PublicDescription = estate.PublicDescription,
                        Area = estate.Area,
                    });
            }
        }

        public List<EstateGetModel> Estates { get; set; }
    }
}
