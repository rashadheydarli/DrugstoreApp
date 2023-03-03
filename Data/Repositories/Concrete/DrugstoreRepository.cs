using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
namespace Data.Repositories.Concrete
{
    public class DrugstoreRepository : IDrugstoreRepository
    {
        static int id;

        public Drugstore Get(int id)
        {
            return DbContext.Drugstores.FirstOrDefault(d=>d.Id == id);
        }

        public List<Drugstore> GetAll()
        {
            return DbContext.Drugstores;
        }

        public List<Drugstore> GetAllDrugstoreByOwner(int id)
        {
            return DbContext.Drugstores.Where(d => d.Owner.Id == id).ToList();
        }
       


        public void Add(Drugstore  drugstore)
        {
            id++;
            drugstore.Id = id;
            drugstore.CreatedAt = DateTime.Now;
            DbContext.Drugstores.Add(drugstore);
        }

        public void Update(Drugstore drugstore)
        {
            var dbDrugstore = DbContext.Drugstores.FirstOrDefault(d=>d.Id== drugstore.Id);
            if( dbDrugstore is not null)
            {
                dbDrugstore.Name = drugstore.Name;
                dbDrugstore.Address = drugstore.Address;
                dbDrugstore.ContactNumber = drugstore.ContactNumber;
                dbDrugstore.Email = drugstore.Email;
                dbDrugstore.Druggists = drugstore.Druggists;
                dbDrugstore.Drugs = drugstore.Drugs;
                dbDrugstore.Owner = drugstore.Owner;
            }
        }

        public void Delete(Drugstore drugstore)
        {
            DbContext.Drugstores.Remove(drugstore);
        }

        public bool IsDuplicateEmail(string email)
        {
           return DbContext.Drugstores.Any(d=>d.Email == email);
        }

       


    }
}

