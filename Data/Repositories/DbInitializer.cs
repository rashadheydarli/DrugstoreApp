using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data.Repositories
{
    public static class DbInitializer
    {
        static int id;

        public static void SeadAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = ++id,
                    Username = "admin1",
                    Password = PasswordHasher.Encrypt("1234"),
                    CreatedBy = "System"
                },

                new Admin
                {
                    Id = ++id,
                    Username = "admin2",
                    Password = PasswordHasher.Encrypt("admin2"),
                    CreatedBy = "System"
                 },

                new Admin
                 {
                    Id = ++id,
                    Username = "admin3",
                    Password = PasswordHasher.Encrypt("admin3"),
                    CreatedBy = "System"
                 }
            };
            DbContext.Admins.AddRange(admins);

        }

    } 
}

