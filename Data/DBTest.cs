using System;
using System.Data.SqlClient;

namespace ScavengeRUs.Data
{
    public static class DBTest
    {
        private static string connectionString = "Server=tcp:scavengerus.database.windows.net,1433;Initial Catalog=ScavengeRUsDB;Persist Security Info=False;User ID=ScavengeRUs;Password=CodingWizards2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void read() {

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
                        }
                    }
                }
            }
        }

        public static string[,] getMessageInfo()
        {
            int length;

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT COUNT(*) FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        length = reader.GetInt32(0);
                    }
                }
            }

            string[,] result = new string[length, 3];
            int col = 0;

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode, email, phoneNum FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result[col, 0] = reader.GetString(0);
                            result[col, 1] = reader.GetString(1);
                            result[col, 2] = reader.GetString(2);

                            col++;
                        }
                    }
                }
            }

            return result;
        }

        public static List<string> getAccessCodes()
        {
            List<string> result = new List<string>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return result;
        }

        public static List<string> fillList()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < 1000000; i++)
            {
                result.Add(i.ToString("000000"));
            }

            return result;
        }

        public static List<string> getAvailableAccessCodes()
        {
            List<string> full = fillList();
            List<string> used = getAccessCodes();

            foreach (string code in used)
            {
                full.Remove(code);
            }
            return full;
        }

        public static void newAccount(string email, string dob, string first, string last, string phone, string username, string password)
        {
            string accessCode;
            Random random = new Random();

            List<string> existingCodes = getAvailableAccessCodes();
            int tempCode = random.Next(0, existingCodes.Count);

            accessCode = existingCodes[tempCode];

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO account (accessCode, email, dob, firstName, lastName, phoneNum, username, pass)
                        VALUES
                        (@access, @email, @DOB, @first, @last, @phone, @username, @password);";

                    command.Parameters.AddWithValue("@access", accessCode);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@first", first);
                    command.Parameters.AddWithValue("@last", last);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                    

                }
            }
        }

    }
}
