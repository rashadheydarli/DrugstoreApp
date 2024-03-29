﻿using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IDrugRepository:IRepository<Drug>
	{
		List<Drug> GetAllDrugsByDrugstore(int drugstoreId);
	}
}

