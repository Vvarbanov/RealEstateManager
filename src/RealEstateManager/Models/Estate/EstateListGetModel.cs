using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            foreach (Data.Estate estate in estates)
            {
                Estates.Add(
                    new EstateGetModel(
                        estate.Name,
                        estate.Type,
                        estate.Price,
                        estate.Status,
                        estate.PublicDescription,
                        estate.Area
                        )
                    );
            }
        }

        public List<EstateGetModel> Estates { get; set; }
    }
}