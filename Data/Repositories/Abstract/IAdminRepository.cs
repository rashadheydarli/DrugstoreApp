using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IAdminRepository
	{
		Admin GetByUsernameAndPassword(string username, string passord);
	}
}

