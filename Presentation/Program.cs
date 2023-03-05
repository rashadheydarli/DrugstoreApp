using Core.Helpers;
using Presentation.Services;
using Core.Constants;
using Core.Entities;
using System.Text;
using Data.Repositories;

namespace Presentation;

public static class Program
{
    private readonly static DrugstoreService _drugstoreService;
    private readonly static OwnerService _ownerService;
    private readonly static DrugService _drugService;
    private readonly static DruggistService _druggistService;
    private readonly static AdminService _adminService;

    static Program()
    {
        Console.OutputEncoding = Encoding.UTF8;

        _drugstoreService = new DrugstoreService();
        _ownerService = new OwnerService();
        _drugService = new DrugService();
        _druggistService = new DruggistService();
        _adminService = new AdminService();
        DbInitializer.SeadAdmins();
    }
    static void Main()
    {
        Authorize: var admin = _adminService.Authorize();
        if(admin is not null)
        {
            while (true)
            {
                ConsoleHelper.WriteWithColor($"--- welcome,{admin.Username} ---", ConsoleColor.Cyan);

            MainMenuDesc: ConsoleHelper.WriteWithColor("1-Owner", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("2-Drugstore", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("3-Druggist", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("4-Drug", ConsoleColor.Yellow);
                ConsoleHelper.WriteWithColor("0-Log out", ConsoleColor.Yellow);
                int number;
                bool isSuccessed = int.TryParse(Console.ReadLine(), out number);
                if (!isSuccessed)
                {
                    ConsoleHelper.WriteWithColor("Inputted number is not correct format", ConsoleColor.Cyan);
                    goto MainMenuDesc;
                }
                else
                {
                    switch (number)
                    {
                        case (int)MainMenuOptions.Owner:
                            while (true)
                            {
                            OwnerDesc: ConsoleHelper.WriteWithColor("1-Create Owner", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("2-Update Owner", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("3-Delete Owner", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("4-Get All Owner", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("5-Get Owner by Id", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("0-Back to Main Menu", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);
                                isSuccessed = int.TryParse(Console.ReadLine(), out number);
                                if (!isSuccessed)
                                {
                                    ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)OwnerOptions.CreateOwner:
                                            _ownerService.Create(admin);
                                            break;
                                        case (int)OwnerOptions.UpdateOwner:
                                            _ownerService.Update(admin);
                                            break;
                                        case (int)OwnerOptions.DeleteOwner:
                                            _ownerService.Delete();
                                            break;
                                        case (int)OwnerOptions.GetAllOwner:
                                            _ownerService.GetAll();
                                            break;
                                        case (int)OwnerOptions.GetOwnerById:
                                            _ownerService.Get(admin);
                                            break;
                                        case (int)OwnerOptions.BackToMainMenu:
                                            goto MainMenuDesc;
                                        default:
                                            ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                                            goto OwnerDesc;
                                            break;
                                    }
                                }

                            }
                            break;
                        case (int)MainMenuOptions.Drugstore:
                            while (true)
                            {
                            StoreDesc: ConsoleHelper.WriteWithColor("1 - Create Drugstore", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("2 - Update Drugstore", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("3 - Delete Drugstore", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("4 - Get all Drugstores", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("5 - Get Drugstore by Id", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("6 - Get all Drugstores by owner", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("7 - Sale", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("0 - Go to Main Menu", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);

                                isSuccessed = int.TryParse(Console.ReadLine(), out number);
                                if (!isSuccessed)
                                {
                                    ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                    goto StoreDesc;
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)DrugstoreOptions.CreateDurgstore:
                                            _drugstoreService.Create(admin);
                                            break;
                                        case (int)DrugstoreOptions.UpdateDrugstore:
                                            _drugstoreService.Update(admin);
                                            break;
                                        case (int)DrugstoreOptions.DeleteDrugstore:
                                            _drugstoreService.Delete();
                                            break;
                                        case (int)DrugstoreOptions.GetAllDrugstores:
                                            _drugstoreService.GetAll();
                                            break;
                                        case (int)DrugstoreOptions.GetDrugstoreById:
                                            _drugstoreService.Get(admin);
                                            break;
                                        case (int)DrugstoreOptions.GetAllDrugstoresByOwner:
                                            _drugstoreService.GetAllDrugstoresByOwner(admin);
                                            break;
                                        case (int)DrugstoreOptions.Sale:
                                            _drugstoreService.Sale();
                                            break;
                                        case (int)DrugstoreOptions.GoToMainMenu:
                                            goto MainMenuDesc;
                                            break;
                                        default:
                                            ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                                            goto StoreDesc;
                                            break;
                                    }
                                }
                            }
                            break;
                        case (int)MainMenuOptions.Druggist:
                            while (true)
                            {
                            DruggistDesc: ConsoleHelper.WriteWithColor("1-Create Druggist", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("2-Update Druggist", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("3-Delete Druggist", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("4-Get All Druggist", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("5-Get All Druggist By Drugstore", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("0-Back to Main Menu", ConsoleColor.Yellow);

                                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);

                                isSuccessed = int.TryParse(Console.ReadLine(), out number);
                                if (!isSuccessed)
                                {
                                    ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)DruggistOptions.CreateDruggist:
                                            _druggistService.Create(admin);
                                            break;
                                        case (int)DruggistOptions.UpdateDruggist:
                                            _druggistService.Update(admin);
                                            break;
                                        case (int)DruggistOptions.DeleteDruggist:
                                            _druggistService.Delete();
                                            break;
                                        case (int)DruggistOptions.GetAllDruggist:
                                            _druggistService.GetAll();
                                            break;
                                        case (int)DruggistOptions.GetAllDruggistByDrugstore:
                                            _druggistService.GetAllDruggistsByDrugstore(admin);
                                          
                                            break;
                                        case (int)DruggistOptions.BackToMainMenu:
                                            goto MainMenuDesc;
                                    }
                                }
                            }
                            break;
                        case (int)MainMenuOptions.Drug:
                            while (true)
                            {
                            DrugDesc: ConsoleHelper.WriteWithColor("1-Create Drug", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("2-Update Drug", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("3-Delete Drug", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("4-Get All Drugs", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("5-Get Drug by id", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("6-Get Drug by drugstore", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("7-Filter", ConsoleColor.Yellow);
                                ConsoleHelper.WriteWithColor("0-Back to Main Menu", ConsoleColor.Yellow);


                                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);

                                isSuccessed = int.TryParse(Console.ReadLine(), out number);
                                if (!isSuccessed)
                                {
                                    ConsoleHelper.WriteWithColor("Inputted number is not correct format!", ConsoleColor.Red);
                                }
                                else
                                {
                                    switch (number)
                                    {
                                        case (int)DrugOptions.CreateDrug:
                                            _drugService.Create(admin);
                                            break;
                                        case (int)DrugOptions.UpdateDrug:
                                            _drugService.Update(admin);
                                            break;
                                        case (int)DrugOptions.DeleteDrug:
                                            _drugService.Delete();
                                            break;
                                        case (int)DrugOptions.GetAllDrugs:
                                            _drugService.GetAll();
                                            break;
                                        case (int)DrugOptions.GetDrugById:
                                            _drugService.Get(admin);
                                            break;
                                        case (int)DrugOptions.GetAllDrugsByDrugstore:
                                            _drugService.GetAllDrugsByDrugstore(admin);
                                            break;
                                        case (int)DrugOptions.Filter:
                                            _drugService.Filter(admin);
                                            break;
                                        case (int)DrugOptions.BackToMainMenu:
                                            goto MainMenuDesc;

                                        default:
                                            ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                                            goto DrugDesc;
                                            break;
                                    }
                                }
                            }
                            break;
                        case (int)MainMenuOptions.Logout:
                            goto Authorize;
                        default:
                            ConsoleHelper.WriteWithColor("Inputted number is not exist!", ConsoleColor.Red);
                            goto MainMenuDesc;
                    }
                }

            }
        }
        

       
    }
}

