using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleApp4
{
    internal class Repository
    {
        public bool DeleteDb()
        {
            using (var ctx = new ShopContext())
            {
                return ctx.Database.EnsureDeleted() && ctx.Database.EnsureCreated();
            }
        }

        public int RemoveAllDataFromAllTable()
        {
            using (var ctx = new ShopContext())
            {
                var dbSets = ctx.GetType().GetProperties().Where(p => p.PropertyType.Name.StartsWith("DbSet"));

                return dbSets.Select(x => x.Name).Select(RemoveAllDataFromTable).Sum();
            }
        }

        private static int RemoveAllDataFromTable(string tableName)
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString))
            {
                var commandText = string.Format($"delete from {tableName}", tableName);

                var command = new SqlCommand(commandText, connection);
                connection.Open();
                var accountNumber = command.ExecuteNonQuery();
                connection.Close();

                return accountNumber;
            }
        }

        internal int AddCustomer(string firstName, string lastName, Address addressId1, Address addressId2)
        {
            using (var ctx = new ShopContext())
            {
                var address1 = ctx.Addresses.SingleOrDefault(x => x.Equals(addressId1));
                if (address1 != null) ctx.Addresses.Attach(address1);
                var address2 = ctx.Addresses.SingleOrDefault(x => x.Equals(addressId2));
                if (address2 != null) ctx.Addresses.Attach(address2);

                var c = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BlAddress = addressId1,
                    SpAddress = addressId2
                };

                ctx.Customers.Add(c);

                var count = ctx.SaveChanges();

                return count;
            }
        }

        internal Address CreateAddress(string addressLine1, string addressLine2, string city, string zipCode)
        {
            var a = new Address
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city,
                ZipCode = zipCode
            };

            return a;
        }
    }
}