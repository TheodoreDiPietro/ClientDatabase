using System;
using System.Collections.Generic;

namespace ClientDatabase
{
    internal class Program
    {
        private static void Main()
        {
            var repo = new Repository();

            var user1 = new User
            {
                AddressLine11 = "Somewhere 1",
                AddressLine12 = "At some floor",
                City1 = "SomeCity",
                ZipCode1 = "11111AA",
                AddressLine21 = "Somewhere 1",
                AddressLine22 = "At some floor",
                City2 = "SomeCity",
                ZipCode2 = "11111AA",
                FirstName = "John",
                LastName = "Doe"
            };

            Console.WriteLine(repo.DeleteDb()
                ? "Successfully recreated database !"
                : "Unsuccessfully recreated database !");

            // Add address
            var addresses = new List<Address>
            {
                repo.CreateAddress(user1.AddressLine11, user1.AddressLine12, user1.City1, user1.ZipCode1),
                repo.CreateAddress(user1.AddressLine21, user1.AddressLine22, user1.City2, user1.ZipCode2)
            };

            // Add client
            var rowsModified = repo.AddCustomer(user1.FirstName, user1.LastName, addresses);
            rowsModified += repo.AddCustomer("Cocorico", user1.LastName, addresses);

            Console.WriteLine("{0} rows added", rowsModified);

            Console.ReadKey();
        }

        public struct User
        {
            public string AddressLine11;
            public string AddressLine12;
            public string City1;
            public string ZipCode1;
            public string AddressLine21;
            public string AddressLine22;
            public string City2;
            public string ZipCode2;
            public string FirstName;
            public string LastName;
        }
    }
}