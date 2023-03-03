using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class OwnerService
	{
		private readonly OwnerRepository _ownerRepository;
		private readonly DrugstoreRepository _drugstoreRepository;
		private readonly DrugstoreService _drugstoreService;
		public OwnerService()
		{
			_ownerRepository = new OwnerRepository();
			_drugstoreRepository = new DrugstoreRepository();
			_drugstoreService = new DrugstoreService();
		}

		public void GetAll()
		{
			var owners = _ownerRepository.GetAll();
            ConsoleHelper.WriteWithColor("--All Owners--", ConsoleColor.Cyan);
			foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Id: {owner.Id}, Name:{owner.Name}, Surname: {owner.Surname}, Drugstores: {owner.Drugstores} ", ConsoleColor.Cyan);
            }
        }

		public void Get(Admin admin)
		{
			GetAll();
            OwnerDesc: ConsoleHelper.WriteWithColor("Enter Owner Id", ConsoleColor.Cyan);

			int id;
			bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
			if (!isSuccessful)
			{
                ConsoleHelper.WriteWithColor("Owner Id is not correct format!", ConsoleColor.Red);
				goto OwnerDesc;
            }
			var owner = _ownerRepository.Get(id);
			if(owner is null)
			{
                ConsoleHelper.WriteWithColor("There is no any owner in this id", ConsoleColor.Red);
            }
			else
			{
                ConsoleHelper.WriteWithColor($"Id: {owner.Id}, Name:{owner.Name}, Surname: {owner.Surname}, Drugstores: {owner.Drugstores} ", ConsoleColor.Cyan);
            }

        }

		public void Create(Admin admin)
		{
			ConsoleHelper.WriteWithColor("Enter Owner Name", ConsoleColor.Cyan);
			string name = Console.ReadLine();
			ConsoleHelper.WriteWithColor("Enter Owner Surname", ConsoleColor.Cyan);
			string surname = Console.ReadLine();
			
			var owner = new Owner
			{
                Name = name,
                Surname = surname,
                CreatedAt = DateTime.Now,
            };
			_ownerRepository.Add(owner);
            ConsoleHelper.WriteWithColor($"Id: {owner.Id}, Name: {owner.Name} {owner.Surname}, Created at : {owner.CreatedAt} is successfully added", ConsoleColor.Green);
        }

		public void Update(Admin admin)
		{
			UpdateDesc: GetAll();
			ConsoleHelper.WriteWithColor("Enter Owner Id", ConsoleColor.Cyan);
			int id;
			bool issuccessed = int.TryParse(Console.ReadLine(), out id);
			if (!issuccessed)
			{
				ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
				goto UpdateDesc;
			}
			var owner = _ownerRepository.Get(id);
			{
				if (owner is null)
				{
					ConsoleHelper.WriteWithColor("There is no any owner in this id ", ConsoleColor.Red);
					goto UpdateDesc;
				}
			}

			ConsoleHelper.WriteWithColor("Enter new  owner name", ConsoleColor.Cyan);
			string name = Console.ReadLine();

			ConsoleHelper.WriteWithColor("Enter new owner surname", ConsoleColor.Cyan);
			string surname = Console.ReadLine();

            var drugstores = _drugstoreRepository.GetAll();
			ConsoleHelper.WriteWithColor("Enter new  Drugstore Id", ConsoleColor.Cyan);
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"Id: {drugstore.Id}, Name:{drugstore.Name}, Address: {drugstore.Address} ", ConsoleColor.Cyan);

            }

			IdDesc: ConsoleHelper.WriteWithColor("Enter Durgstore id", ConsoleColor.Cyan);
            int storeId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out storeId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Drugstore id is not correct format!", ConsoleColor.Red);
                goto IdDesc;
            }

			owner.Name = name;
			owner.Surname = surname;
			owner.Drugstores = drugstores;
			owner.ModifiedAt = DateTime.Now;

			_ownerRepository.Update(owner);
            ConsoleHelper.WriteWithColor($" Name: {owner.Name} {owner.Surname},is successfully updated", ConsoleColor.Green);


        }

		public void Delete()
		{
			GetAll();
			if(_ownerRepository.GetAll().Count == 0)
			{
                ConsoleHelper.WriteWithColor("There is no owner", ConsoleColor.Red);
            }
			else
			{
                List: ConsoleHelper.WriteWithColor("Enter owner Id", ConsoleColor.Cyan);
                int id;
                bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                    goto List;
                }
				var owner = _ownerRepository.Get(id);
				if(owner is null)
				{
                    ConsoleHelper.WriteWithColor("There is no any owner in this id", ConsoleColor.Red);
                }
				_ownerRepository.Delete(owner);
                ConsoleHelper.WriteWithColor($"{owner.Name} {owner.Surname} is successfully deleted", ConsoleColor.Green);
            }
        }
    }
}

