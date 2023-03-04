using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class DruggistService
	{
		private readonly DruggistRepository _druggistRepository;
		private readonly DrugstoreRepository _drugstoreRepository;
        private readonly DrugstoreService _drugstoreService;

		public DruggistService()
		{
			_druggistRepository = new DruggistRepository();
			_drugstoreRepository = new DrugstoreRepository();
            _drugstoreService = new DrugstoreService();
		}

		public void GetAll()
		{
			var druggists = _druggistRepository.GetAll();
			
			foreach (var druggist in druggists)
			{
                ConsoleHelper.WriteWithColor($" Id: {druggist.Id},Fullname: {druggist.Name} {druggist.Surname}, Age: {druggist.Age}, Experience:{ druggist.Experience}, Drugstore: {druggist.Drugstore}", ConsoleColor.Cyan);

            }
        }

        public void GetAllDruggistsByDrugstore(Admin admin)
        {
            _drugstoreService.GetAll();
            IdDesc: ConsoleHelper.WriteWithColor("Enter Drugstore id", ConsoleColor.Cyan);
            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Yellow);
                goto IdDesc;
            }

            var druggist = _drugstoreRepository.Get(id);
            if(druggist is null)
            {
                ConsoleHelper.WriteWithColor("there is no druggist  in this id", ConsoleColor.Red);
                    goto IdDesc;
            }
            else
            {
                foreach (var item in druggist.Druggists)
                {
                    ConsoleHelper.WriteWithColor($"Id: {item.Id}, Name: {item.Name}, Age:{item.Age}, Experience: {item.Experience}, Drugstore: {item.Drugstore} ,Created at : {item.CreatedAt} ", ConsoleColor.Magenta);

                }

            }

        }


        public void Create(Admin admin)
		{
			if(_drugstoreRepository.GetAll().Count == 0)
			{
                ConsoleHelper.WriteWithColor("You must create a drugstore before", ConsoleColor.Yellow);
            }
			else
			{
				ConsoleHelper.WriteWithColor("-- Enter  druggist name --", ConsoleColor.Cyan);
				string name = Console.ReadLine();
                ConsoleHelper.WriteWithColor("-- Enter druggist surname --", ConsoleColor.Cyan);
                string surname = Console.ReadLine();

				AgeDesc:  ConsoleHelper.WriteWithColor("-- Enter druggist age --", ConsoleColor.Cyan);
                int age;
				bool isSuccessful = int.TryParse(Console.ReadLine(), out age);
				if (!isSuccessful)
				{
                    ConsoleHelper.WriteWithColor("Age is not correct format!", ConsoleColor.Yellow);
					goto AgeDesc;
                }

                ExpDesc:  ConsoleHelper.WriteWithColor("-- Enter druggist experience --", ConsoleColor.Cyan);
				int experience;
                isSuccessful = int.TryParse(Console.ReadLine(), out experience);
                if (!isSuccessful)
                {
                    ConsoleHelper.WriteWithColor("Experience is not correct format!", ConsoleColor.Yellow);
                    goto ExpDesc;
                }
				if (!(experience <= (age - 18)))
				{
                    ConsoleHelper.WriteWithColor("Experience does not reflect reality!", ConsoleColor.Yellow);
                    goto ExpDesc;
                }

				_drugstoreService.GetAll();
				IdDesc: ConsoleHelper.WriteWithColor("-- Enter drugstore's id --", ConsoleColor.Cyan);
                int id;
                isSuccessful = int.TryParse(Console.ReadLine(), out id);
                if (!isSuccessful)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Yellow);
                    goto IdDesc;
                }
                var drugstore = _drugstoreRepository.Get(id);
                if (drugstore is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any drugstore in this id ", ConsoleColor.Yellow);
                    goto IdDesc;
                }
                var druggist = new Druggist
				{
					Name = name,
					Surname = surname,
					Age = age,
					Experience = experience,
					Drugstore = drugstore,
					CreatedAt= DateTime.Now,
				};

				drugstore.Druggists.Add(druggist);
				_druggistRepository.Add(druggist);
                ConsoleHelper.WriteWithColor($"{druggist.Name} {druggist.Surname} , Age: {druggist.Age}, Experience: {druggist.Experience}, Drugstore: {druggist.Drugstore} is successfully added by {druggist.CreatedBy}", ConsoleColor.Green);

            }
        }

		public void Update(Admin admin)
		{
			GetAll();
            UpdateDesc: ConsoleHelper.WriteWithColor("Enter  druggist id", ConsoleColor.Cyan);
            int id;
            bool isSuccessed = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessed)
            {
                ConsoleHelper.WriteWithColor("Inputted Id is not correct format!", ConsoleColor.Red);
                goto UpdateDesc;
            }
			var druggist = _druggistRepository.Get(id);
			{
				if(druggist is null)
				{
                    ConsoleHelper.WriteWithColor("There is no any druggist in this id ", ConsoleColor.Red);
                    goto UpdateDesc;
                }
			}

            ConsoleHelper.WriteWithColor("-- Enter  New druggist name --", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("-- Enter New druggist surname --", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

            AgeDesc: ConsoleHelper.WriteWithColor("-- Enter New age --", ConsoleColor.Cyan);
            int age;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out age);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Age is not correct format!", ConsoleColor.Yellow);
                goto AgeDesc;
            }

            ExpDesc: ConsoleHelper.WriteWithColor("-- Enter druggist experience --", ConsoleColor.Cyan);
            int experience;
            isSuccessful = int.TryParse(Console.ReadLine(), out experience);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Experience is not correct format!", ConsoleColor.Yellow);
                goto ExpDesc;
            }
            if (!(experience <= (age - 18)))
            {
                ConsoleHelper.WriteWithColor("Experience does not reflect reality!", ConsoleColor.Yellow);
                goto ExpDesc;
            }

           _drugstoreService.GetAll();

            IdDesc: ConsoleHelper.WriteWithColor("-- Enter  druggist drugstore's id --", ConsoleColor.Cyan);
            int storeid;
            isSuccessful = int.TryParse(Console.ReadLine(), out storeid);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Yellow);
                goto IdDesc;
            }

            var drugstore = _drugstoreRepository.Get(id);
            if(drugstore is null)
            {
                ConsoleHelper.WriteWithColor("There is no any drugstore in this id ", ConsoleColor.Yellow);
                goto IdDesc;
            }


            druggist.Name = name;
            druggist.Surname = surname;
            druggist.Age = age;
            druggist.Experience = experience;
            druggist.Drugstore = drugstore;
            druggist.ModifiedAt = DateTime.Now;

            _druggistRepository.Update(druggist);
            ConsoleHelper.WriteWithColor($"{druggist.Name} {druggist.Surname} ,Age: {druggist.Age}, Experience: {druggist.Experience}, Drugstore: {druggist.Drugstore} , Modified at: {druggist.ModifiedAt} is successfully updated by {druggist.ModifiedBy}", ConsoleColor.Green);
        }

        public void Delete()
        {
            GetAll();

             IdDesc: ConsoleHelper.WriteWithColor(" Enter ID ", ConsoleColor.Cyan);

            int id;
            bool isSuccessful = int.TryParse(Console.ReadLine(), out id);
            if (!isSuccessful)
            {
                ConsoleHelper.WriteWithColor("ID is not correct format!", ConsoleColor.Red);
                goto IdDesc;
            }
            var dbDruggist = _druggistRepository.Get(id);
            if(dbDruggist is null)
            {
                ConsoleHelper.WriteWithColor("there is no  druggist in this id", ConsoleColor.Red);
            }
            _druggistRepository.Delete(dbDruggist);
            ConsoleHelper.WriteWithColor($"{dbDruggist.Name} {dbDruggist.Surname} is successfully deleted", ConsoleColor.Green);

        }

    }
}

