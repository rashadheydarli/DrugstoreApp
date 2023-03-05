using System;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Services
{
	public class DrugstoreService
	{
		private readonly DrugstoreRepository _drugstoreRepository;
        private readonly DrugRepository _drugRespository;
		private readonly OwnerRepository _ownerRepository;
		
        
      
		public DrugstoreService()
		{
			_drugstoreRepository = new DrugstoreRepository();
            _drugRespository = new DrugRepository();
			_ownerRepository = new OwnerRepository();
			
		}

		public void GetAll()
		{
			var drugstores = _drugstoreRepository.GetAll();
			ConsoleHelper.WriteWithColor("--All Drugstores--", ConsoleColor.Cyan);
			foreach (var drugstore in drugstores)
			{
				ConsoleHelper.WriteWithColor($"Id:{drugstore.Id}, Name:{drugstore.Name}, Address: {drugstore.Address}, Owner: {drugstore.Owner?.Name}", ConsoleColor.Cyan);
			}
		}

		public void Get(Admin admin)
		{
			_drugstoreRepository.GetAll();

		    StoreDesc: ConsoleHelper.WriteWithColor("Enter Drugstore Id", ConsoleColor.Cyan);

			int storeId;
			bool isSuccessed = int.TryParse(Console.ReadLine(), out storeId);
			if (!isSuccessed)
			{
				ConsoleHelper.WriteWithColor(" Drugstore Id is not correct format ! ", ConsoleColor.Red);
				goto StoreDesc;
			}

			var drugstore = _drugstoreRepository.Get(storeId);
			if(drugstore is null)
			{
                ConsoleHelper.WriteWithColor(" There is no any drugstore in this id ", ConsoleColor.Red);
            }
			ConsoleHelper.WriteWithColor($"Id: {drugstore.Id}, Name: {drugstore.Name}, Drugs: {drugstore.Drugs}, Owner: {drugstore.Owner}", ConsoleColor.Cyan);
        }


        public void Create(Admin admin)
		{
            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create a owner first", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter drugstore name", ConsoleColor.Cyan);
			string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter drugstore address", ConsoleColor.Cyan);
			string address = Console.ReadLine();

        NumberDesc: ConsoleHelper.WriteWithColor("Enter contact number", ConsoleColor.Cyan);
            string contact = Console.ReadLine();
            if (!(contact.IsNumber()))
            {
                ConsoleHelper.WriteWithColor("Number is not correct format!", ConsoleColor.Red);
                goto NumberDesc;
            }
            if (_drugstoreRepository.IsDuplicateNumber(contact))
            {
                ConsoleHelper.WriteWithColor("This number already used", ConsoleColor.Red);
                goto NumberDesc;
            }
            

        EmailDesc: ConsoleHelper.WriteWithColor("Enter Drugstore Email", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if(!(email.IsEmail()))
            {
                ConsoleHelper.WriteWithColor("Email is not correct format!", ConsoleColor.Red);
                goto EmailDesc;
            }
            if (_drugstoreRepository.IsDuplicateEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email already used", ConsoleColor.Red);
                goto EmailDesc;
            }


        OwnerDesc:ConsoleHelper.WriteWithColor("Enter drugstore owner", ConsoleColor.Cyan);
			var owners= _ownerRepository.GetAll();
            foreach (var item in owners)
            {
                ConsoleHelper.WriteWithColor($"Id:{item.Id}, Name: {item.Name} ,Surname: {item.Surname}, Owner: {item.Drugstores} Created at : {item.CreatedAt} is successfully added", ConsoleColor.Green);

            }
            int ownerId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Owner id is not correct format!", ConsoleColor.Red);
                goto OwnerDesc;
            }
			var owner = _ownerRepository.Get(ownerId);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("Owner is not exist in this Id", ConsoleColor.Red);
                goto OwnerDesc;
            }
			var drugstore = new Drugstore
			{
				Name = name,
				Address = address,
				ContactNumber = contact,
				Owner = owner,
                CreatedBy=admin.Username
			};
			owner.Drugstores.Add(drugstore);
			_drugstoreRepository.Add(drugstore);
            ConsoleHelper.WriteWithColor($"Id: {drugstore.Id}, Name: {drugstore.Name} ,Address: {drugstore.Address}, Owner: {drugstore.Owner?.Name} Created at : {drugstore.CreatedAt} is successfully added by {drugstore.CreatedBy}", ConsoleColor.Green);

        }

		public void Update(Admin admin)
		{
			GetAll();
            StoreDesc:  ConsoleHelper.WriteWithColor("Enter Drugstore Id", ConsoleColor.Cyan);
            int id;
            bool issuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!issuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
                goto StoreDesc;
            }
			var drugstore = _drugstoreRepository.Get(id);
			{
				if(drugstore is null)
				{
                    ConsoleHelper.WriteWithColor("There is no any drugstore in this id ", ConsoleColor.Red);
                    goto StoreDesc;
                }
			}

            ConsoleHelper.WriteWithColor("Enter new drugstore name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter new drugstore address", ConsoleColor.Cyan);
            string address = Console.ReadLine();

			ConsoleHelper.WriteWithColor("Enter new contact number", ConsoleColor.Cyan);
            string contact= Console.ReadLine();

            OwnerDesc: ConsoleHelper.WriteWithColor("Enter drugstore owner", ConsoleColor.Cyan);
            var owners= _ownerRepository.GetAll();
            foreach (var item in owners)
            {
                ConsoleHelper.WriteWithColor($"{item.Id}, Name: {item.Name} ,Surname: {item.Surname}, Drugstore: {item.Drugstores} Created at : {item.CreatedAt} is successfully added", ConsoleColor.Green);

            }
            int ownerId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Owner id is not correct format!", ConsoleColor.Red);
                goto OwnerDesc;
            }
            var owner = _ownerRepository.Get(ownerId);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("Owner is not exist in this Id", ConsoleColor.Red);
                goto OwnerDesc;
            }
            drugstore.Name = name;
            drugstore.Address = address;
            drugstore.ContactNumber = contact;
            drugstore.Owner = owner;
            drugstore.ModifiedBy = admin.Username;
            drugstore.ModifiedAt = DateTime.Now;

            _drugstoreRepository.Update(drugstore);
            ConsoleHelper.WriteWithColor($"Id:{drugstore.Id}, Name: {drugstore.Name} ,Address: {drugstore.Address}, Owner: {drugstore.Owner} Modified at : {drugstore.ModifiedAt} is successfully updated by {drugstore.ModifiedBy}" , ConsoleColor.Green);

        }

        public void Delete()
        {
            GetAll();
            IdDesc: ConsoleHelper.WriteWithColor("Enter drugstore id", ConsoleColor.Cyan);
            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                goto IdDesc;
            }
            var drugstore = _drugstoreRepository.Get(id);
            if(drugstore is null)
            {
                ConsoleHelper.WriteWithColor("There is no drugstore is in this id", ConsoleColor.Red);
            }
            _drugstoreRepository.Delete(drugstore);
            ConsoleHelper.WriteWithColor($"{drugstore.Name} ,Address: {drugstore.Address}, Owner: {drugstore.Owner}  is successfully deleted", ConsoleColor.Green);
        }

        public void GetAllDrugstoresByOwner(Admin admin)
        {
            var owners= _ownerRepository.GetAll();
            foreach (var item in owners)
            {
                ConsoleHelper.WriteWithColor($"id: {item.Id}, Name: {item.Name} ,Surname: {item.Surname}, Owner: {item.Drugstores} Created at : {item.CreatedAt} is successfully added", ConsoleColor.Green);

            }
        OwnerDesc: ConsoleHelper.WriteWithColor("Enter onwer id", ConsoleColor.Cyan);

            int ownerId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Owner id is not correct format", ConsoleColor.Red);
                goto OwnerDesc;
            }
            var owner = _ownerRepository.Get(ownerId);
            if(owner is null)
            {
                ConsoleHelper.WriteWithColor("There is no any owner in this id", ConsoleColor.Red);
            }
            if(owner.Drugstores.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no Drugstore in this owner", ConsoleColor.Red);
            }
            else
            {
                foreach (var drugstore in owner.Drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Id:{drugstore.Id}, Name: {drugstore.Name}, Address: {drugstore.Address}", ConsoleColor.Cyan);
                }
            }
        }

        public void Sale()
        {
            GetAll();
        StoreDesc: ConsoleHelper.WriteWithColor("Enter Drugstore Id", ConsoleColor.Cyan);

            int storeId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out storeId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor(" Drugstore Id is not correct format ! ", ConsoleColor.Red);
                goto StoreDesc;
            }

            var drugs = _drugRespository.GetAllDrugsByDrugstore(storeId);
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"{drug.Id}, Name: {drug.Name}, Price {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore.Name}", ConsoleColor.Cyan);
            }

        DrugDesc: ConsoleHelper.WriteWithColor("Enter Drug Id", ConsoleColor.Cyan);

            int drugId;
             isSuccessed = int.TryParse(Console.ReadLine(), out drugId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor(" Drug Id is not correct format ! ", ConsoleColor.Red);
                goto DrugDesc;
            }

            var dbDrug=_drugRespository.Get(drugId);
            if(dbDrug is null)
            {
                ConsoleHelper.WriteWithColor("There is no any drug in this Id ! ", ConsoleColor.Red);
                goto DrugDesc;
            }

        CountDesc: ConsoleHelper.WriteWithColor("Enter Drug Count", ConsoleColor.Cyan);
            int count;
            isSuccessed = int.TryParse(Console.ReadLine(), out count);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor(" Count is not correct format ! ", ConsoleColor.Red);
                goto CountDesc;
            }
            if ( dbDrug.Count == 0)
            {
                ConsoleHelper.WriteWithColor(" Drug out of stock ", ConsoleColor.Red);
                goto CountDesc;
            }
            if (count > dbDrug.Count)
            {
                ConsoleHelper.WriteWithColor(" There is no enough drug ", ConsoleColor.Red);
                goto CountDesc;
            }
            else
            {
                var totalPrice = count * dbDrug.Price;
                ConsoleHelper.WriteWithColor($" Total Price: {totalPrice} ", ConsoleColor.Cyan);
            }

            ConsoleHelper.WriteWithColor($" Count:{dbDrug.Count}", ConsoleColor.Red);

            var dbstock = dbDrug.Count - count;
            dbDrug.Count = dbstock;
        }
    }
}

