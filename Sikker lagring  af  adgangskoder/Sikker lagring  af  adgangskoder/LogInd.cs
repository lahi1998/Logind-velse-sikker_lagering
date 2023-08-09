using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sikker_lagring__af__adgangskoder
{
    internal class LogInd
    {
        // det en public bool så jeg returner en boolean værdi for true/false for logind.
        public bool Logind(string username, string password)
        {
            string? fetchedSalt, fetchedPassHash;
            bool LogindStatus = false;

            try
            {
                //Forbindelse der henter brugernavn, hashed kode og salt hvor username(brugernavn) matcher.
                string connectionString = "server=localhost;user=Remote;password=Kode1234!;database=logindkryptering;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string usernameToFetch = username;
                    string sqlQuery = "SELECT PassHash, Salt FROM users WHERE Username = @username";

                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", usernameToFetch);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // hvis der bliver fundet en bruger der matcher validres indtsatede kodeord
                            // efter den er salted og hashed med den hashed kodeord fra databasen. + henter salt til hashing af det indtastede kodeord. 
                            if (reader.Read())
                            {
                                fetchedPassHash = reader["PassHash"].ToString();
                                fetchedSalt = reader["Salt"].ToString();

                                // salter det inputed kodeord fra login og matcher dem.
                                Hasher hasher = new Hasher();
                                byte[] salt = Convert.FromBase64String(fetchedSalt);
                                byte[] bytes = Encoding.ASCII.GetBytes(password);
                                byte[] PS = new byte[bytes.Length + salt.Length];
                                Buffer.BlockCopy(bytes, 0, PS, 0, bytes.Length);
                                Buffer.BlockCopy(salt, 0, PS, bytes.Length, salt.Length);
                                byte[] HashedPS = hasher.Hash(PS);

                                // converter det hashed og saltede kodeord til string så jeg kan matche dem.
                                string saltedhash = Convert.ToBase64String(HashedPS).Replace("-", "");

                                // hvis de matcher ændre den logindstatus til true eller returnere vi senere bare false
                                if (saltedhash == fetchedPassHash)
                                {
                                    LogindStatus = true;
                                }

                            }
                            else
                            {
                                Console.WriteLine("Brugeren findes ikke test");
                                Console.ReadKey();
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }


            return LogindStatus;

        }
    }
}
