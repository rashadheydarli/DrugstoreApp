using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IDrugstoreRepository : IRepository<Drugstore>
	{
        public bool IsDuplicateEmail(string email);
        public bool IsDuplicateNumber(string number);

    }

}

