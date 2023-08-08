using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Sikker_lagring__af__adgangskoder
{

internal class OpretBruger
    {
        public void Opretbruger(string username, string password)
        {

            Hasher hasher = new Hasher();
            SaltGenerator salter = new SaltGenerator();
            Console.WriteLine();

            byte[] salt = salter.GenerateRandomNumber(10);
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] PS = new byte[bytes.Length + salt.Length];
            Buffer.BlockCopy(bytes, 0, PS, 0, bytes.Length);
            Buffer.BlockCopy(salt, 0, PS, bytes.Length, salt.Length);

            byte[] HashedPS = hasher.Hash( PS);

            //Console.WriteLine(password);
            //Console.WriteLine(BitConverter.ToString(salt).Replace("-", ""));
            //Console.WriteLine(BitConverter.ToString(HashedPS).Replace("-", ""));
            //Console.ReadKey();


            try
            {
                string connectionString = "server=localhost;user=Remote;password=Kode1234!;database=logindkryptering;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string QHasedPS = BitConverter.ToString(HashedPS).Replace("-", "");
                    string QSalt = BitConverter.ToString(salt).Replace("-", "");

                    Console.WriteLine(QSalt);
                    Console.WriteLine(QHasedPS);
                    Console.ReadKey();

                    // Parameterized query
                    string sqlQuery = "INSERT INTO users (Username, PassHash, Salt) VALUES (@username, @QHashedPS, @QSalt)";

                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        // Provide parameter values
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@QHashedPS", QHasedPS);
                        command.Parameters.AddWithValue("@QSalt", QSalt);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
