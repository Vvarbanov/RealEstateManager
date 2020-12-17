using RealEstateManager.Models.Data;
using RealEstateManager.Models.EstateAccount;
using RealEstateManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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
                        Address = estate.Address,
                        PublicDescription = estate.PublicDescription,
                        PrivateDescription = estate.PrivateDescription,
                        Area = estate.Area,
                        EstateAgents = estate.Account
                        .Select(x => new EstateAccountGetModel
                        {
                            EstateId = x.EstateId,
                            AccountId = x.AccountId
                        })
                        .ToList()
                    });
            }
        }

        public List<EstateGetModel> Estates { get; set; }
    }
}
