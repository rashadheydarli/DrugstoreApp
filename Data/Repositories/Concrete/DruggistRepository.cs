using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class DruggistRepository : IDruggistRepository
    {
        static int id;

        public List<Druggist> GetAll()
        {
            return DbContext.Druggists;
        }
        
        public Druggist Get(int id)
        {
            return DbContext.Druggists.FirstOrDefault(d => d.Id == id);
        }

        public List<Druggist> GetAllDruggistsByDrugstore(int id)
        {
            return DbContext.Druggists.Where(d => d.Drugstore.Id == id).ToList();
        }
        public void Add(Druggist druggist)
        {
            id++;
            druggist.Id = id;
            druggist.CreatedAt = DateTime.Now;
            DbContext.Druggists.Add(druggist);
        }

        public void Update(Druggist druggist)
        {
            var dbDruggist = DbContext.Druggists.FirstOrDefault(d => d.Id == druggist.Id);
            if(dbDruggist is not null)
            {
                dbDruggist.Name = druggist.Name;
                dbDruggist.Surname = druggist.Surname;
                dbDruggist.Age = druggist.Age;
                dbDruggist.Experience = druggist.Experience;
                dbDruggist.Drugstore = druggist.Drugstore;
            }
        }

        public void Delete(Druggist druggist)
        {
            DbContext.Druggists.Remove(druggist);
        }
    }
}

