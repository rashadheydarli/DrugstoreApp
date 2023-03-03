using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
	public class OwnerRepository:IOwnerRepository

	{
        static int id;

        public List<Owner> GetAll()
        {
            return DbContext.Owners;
        }

        public Owner Get(int id)
        {
            return DbContext.Owners.FirstOrDefault(o => o.Id == id);
        }


        public void Add(Owner owner)
        {
            id++;
            owner.Id = id;
            owner.CreatedAt = DateTime.Now;
            DbContext.Owners.Add(owner);
        }
         
        public void Update(Owner owner)
        {
            var dbOwner = DbContext.Owners.FirstOrDefault(o => o.Id == owner.Id);
            if (dbOwner is not null)
            {
                dbOwner.Name = owner.Name;
                dbOwner.Surname = owner.Surname;
                dbOwner.Drugstores = owner.Drugstores;
            }
        }
        public void Delete(Owner owner)
        {
            DbContext.Owners.Remove(owner);
        }


    }
}

