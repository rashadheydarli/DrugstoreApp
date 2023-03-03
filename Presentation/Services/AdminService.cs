﻿using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class AdminService
	{
		private readonly AdminRepository _adminRepository;
		public AdminService()
		{
			_adminRepository = new AdminRepository();
		}

		public Admin Authorize()
		{
			LoginDesc: ConsoleHelper.WriteWithColor("Login", ConsoleColor.Blue);

            ConsoleHelper.WriteWithColor("-Enter username-", ConsoleColor.Cyan);
            string username = Console.ReadLine();

            ConsoleHelper.WriteWithColor("-Enter password-", ConsoleColor.Cyan);
            string password = Console.ReadLine();

			var admin = _adminRepository.GetByUsernameAndPassword(username, password);
			if(admin is null)
			{
                ConsoleHelper.WriteWithColor("username or password is incorrect", ConsoleColor.Red);
				goto LoginDesc;
            }
			return admin;

        }
	}
}

