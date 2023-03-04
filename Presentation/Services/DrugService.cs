using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class DrugService
	{
		private readonly DrugRepository _drugRepository;
		private readonly DrugstoreRepository _drugstoreRepository;
        private readonly DrugstoreService _drugstoreService;
		public DrugService()
		{
			_drugRepository = new DrugRepository();
			_drugstoreRepository = new DrugstoreRepository();
            _drugstoreService = new DrugstoreService();
		}

		public void GetAll()
		{
			var drugs = _drugRepository.GetAll();
			ConsoleHelper.WriteWithColor("--All Durgs--", ConsoleColor.Cyan);
			foreach (var drug in drugs)
			{
				ConsoleHelper.WriteWithColor($"Id: {drug.Id}, Name: {drug.Name}, Price {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore.Name}", ConsoleColor.Cyan);
			}
		}

		public void Get(Admin admin)
		{
            GetAll();
			OwnerDesc: ConsoleHelper.WriteWithColor("Enter Drug Id", ConsoleColor.Cyan);

            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Drug Id is not correct format!", ConsoleColor.Red);
                goto OwnerDesc;
            }
            var drug = _drugRepository.Get(id);
            if (drug is null)
            {
                ConsoleHelper.WriteWithColor("There is no any drug in this id", ConsoleColor.Red);
            }
            else
            {
                ConsoleHelper.WriteWithColor($"Id: {drug.Id}, Name:{drug.Name}, Price: {drug.Price}, Count: {drug.Count} , Drugstore:{drug.Drugstore}", ConsoleColor.Cyan);
            }
        }

		public void GetAllDrugsByDrugstore(Admin admin)
		{
			var drugstores = _drugstoreRepository.GetAll();
            {
                foreach (var dbDrugstore in drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Id: {dbDrugstore.Id}, Name: {dbDrugstore.Name}, Address: {dbDrugstore.Address}, Contact number: {dbDrugstore.ContactNumber}, Owner:{dbDrugstore.Owner} ", ConsoleColor.Cyan);
                }
            }

        DrugDesc: ConsoleHelper.WriteWithColor("Enter Drugstore id", ConsoleColor.Cyan);
            int drugstoreId;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out drugstoreId);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Drugstore id is not correct format", ConsoleColor.Red);
                goto DrugDesc;
            }
			var drugstore = _drugstoreRepository.Get(drugstoreId);
			if(drugstore is null)
			{
                ConsoleHelper.WriteWithColor("There is no any drugstore in this id", ConsoleColor.Red);
            }
			if(drugstore.Drugs.Count == 0)
			{
                ConsoleHelper.WriteWithColor("There is no drug in this group", ConsoleColor.Red);
            }
			else
			{
				foreach (var drug in drugstore.Drugs)
				{
					ConsoleHelper.WriteWithColor($"Id: {drug.Id}, Name: {drug.Name}, Price: {drug.Price}, Count: {drug.Count}", ConsoleColor.Cyan);
				}
			}
        }



        public void Filter(Admin admin)
        {
            GetAllDrugsByDrugstore(admin);

            FilterDesc: ConsoleHelper.WriteWithColor("Show drugs below than the Entered price", ConsoleColor.Cyan);
            decimal price;
            bool isSuccessed = decimal.TryParse(Console.ReadLine(), out price);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Price is not correct format", ConsoleColor.Red);
                goto FilterDesc;
            }
            var  filterPrice = _drugRepository.Filter(price);
            {
                foreach (var drugPrice in filterPrice)
                {
                    ConsoleHelper.WriteWithColor($"Id: {drugPrice.Id}, Name: {drugPrice.Name}, Price: {drugPrice.Price}, Count: {drugPrice.Count} ,Drugstore: {drugPrice.Drugstore} ", ConsoleColor.Cyan);
                }
            }
        }


        public void Create(Admin admin)
		{
			if (_drugstoreRepository.GetAll().Count == 0)
			{
                ConsoleHelper.WriteWithColor("You must create a drugstore first", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter drug name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            PriceDesc: ConsoleHelper.WriteWithColor("Enter drug price", ConsoleColor.Cyan);
			int price;
			bool isSuccessful = int.TryParse(Console.ReadLine(), out price);
			if (!isSuccessful)
			{
                ConsoleHelper.WriteWithColor("Price is not correct format", ConsoleColor.Red);
                goto  PriceDesc;
            }

		    CountDesc: ConsoleHelper.WriteWithColor("Enter drug count", ConsoleColor.Cyan);
            int count;
            isSuccessful = int.TryParse(Console.ReadLine(), out count);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("count is not correct format", ConsoleColor.Red);
                goto CountDesc;
            }
			if(count<= 0)
			{
                ConsoleHelper.WriteWithColor("Count must be upper than 0", ConsoleColor.Red);
                goto CountDesc;
            }

            _drugstoreService.GetAll();
			StoreDesc: ConsoleHelper.WriteWithColor("Enter drug's drugstore", ConsoleColor.Cyan);
			int id;
            isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Drugstore id  is not correct format", ConsoleColor.Red);
                goto StoreDesc;
            }
			var drugstore = _drugstoreRepository.Get(id);
			if(drugstore is null)
			{
                ConsoleHelper.WriteWithColor("Drugstore is not exist in this Id", ConsoleColor.Red);
                goto StoreDesc;
            }
            
			var drug = new Drug
			{
				Name = name,
				Price = price,
				Count = count,
				Drugstore = drugstore
			};

			drugstore.Drugs.Add(drug);
			_drugRepository.Add(drug);
            ConsoleHelper.WriteWithColor($"Name: {drug.Name},Price: {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore} is successfully added by {drug.CreatedBy}", ConsoleColor.Green);
        }

        public void Update(Admin admin)
		{
			GetAll();
            DrugDesc: ConsoleHelper.WriteWithColor("Enter drug Id", ConsoleColor.Cyan);
            int id;
            bool issuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!issuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted id is not correct format", ConsoleColor.Red);
                goto DrugDesc;
            }
            var drug = _drugRepository.Get(id);
            {
                if (drug is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any drug in this id ", ConsoleColor.Red);
                    goto DrugDesc;
                }
            }

            ConsoleHelper.WriteWithColor("Enter new drug name", ConsoleColor.Cyan);
            string name = Console.ReadLine();

        PriceDesc: ConsoleHelper.WriteWithColor("Enter new drug price", ConsoleColor.Cyan);
            int price;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out price);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Price is not correct format", ConsoleColor.Red);
                goto PriceDesc;
            }

        CountDesc: ConsoleHelper.WriteWithColor("Enter new drug count", ConsoleColor.Cyan);
            int count;
            isSuccessful = int.TryParse(Console.ReadLine(), out count);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("count is not correct format", ConsoleColor.Red);
                goto CountDesc;
            }
        StoreDesc: ConsoleHelper.WriteWithColor("Enter drug's new drugstore", ConsoleColor.Cyan);
            int storeid;
            isSuccessful = int.TryParse(Console.ReadLine(), out storeid);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Drugstore id  is not correct format", ConsoleColor.Red);
                goto StoreDesc;
            }
            var drugstore = _drugstoreRepository.Get(id);
            if (drugstore is null)
            {
                ConsoleHelper.WriteWithColor("Drugstore is not exist in this Id", ConsoleColor.Red);
                goto StoreDesc;
            }
            drug.Name = name;
            drug.Price = price;
            drug.Count = count;
            drug.Drugstore = drugstore;
            drug.ModifiedAt = DateTime.Now;

            _drugRepository.Update(drug);
            ConsoleHelper.WriteWithColor($" Name:{drug.Name}, Price: {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore}, Modified at: {drug.ModifiedAt} is successfully updated by {drug.ModifiedBy}", ConsoleColor.Green);
        }

		public void Delete()
		{
            GetAll();
            IdDesc: ConsoleHelper.WriteWithColor("Enter drug id", ConsoleColor.Cyan);
            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                goto IdDesc;
            }
            var drug = _drugRepository.Get(id);
            if(drug is null)
            {
                ConsoleHelper.WriteWithColor("There is no drug in this id", ConsoleColor.Red);
            }
            _drugRepository.Delete(drug);
            ConsoleHelper.WriteWithColor($" Name:{drug.Name}, Price: {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore} is successfully deleted", ConsoleColor.Green);

        }
    }
}

