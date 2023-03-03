using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
	public class DrugRepository:IDrugRepository
	{
		static int id;

		public List<Drug> GetAll()
		{
			return DbContext.Drugs;
		}

        public Drug Get(int id)
        {
            return DbContext.Drugs.FirstOrDefault(d => d.Id == id);
        }

        public List<Drug> GetAllDrugsByDrugstore(int id)
		{
			return DbContext.Drugs.Where(d => d.Drugstore.Id == id).ToList();
		}

		public void Add(Drug drug)
		{
			id++;
			drug.Id = id;
			DbContext.Drugs.Add(drug);
		}

		public void Update(Drug drug)
		{
			var dbDrug = DbContext.Drugs.FirstOrDefault(d => d.Id == drug.Id);
			if(dbDrug is not null)
			{
				dbDrug.Name = drug.Name;
				dbDrug.Price = drug.Price;
				dbDrug.Count = drug.Count;
				dbDrug.Drugstore = drug.Drugstore;
			}
		}
		public List<Drug> Filter(decimal price)
		{
			return DbContext.Drugs.Where(d => d.Price <= price).ToList();
		}

		public void Delete(Drug drug)
		{
			DbContext.Drugs.Remove(drug);
		}
        public List<Drug> Sale(int drugstoreId)
        {
            return DbContext.Drugs.Where(d => d.Drugstore.Id == drugstoreId).ToList();
        }
    }
}

