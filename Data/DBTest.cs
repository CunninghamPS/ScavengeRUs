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
                            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
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

                    command.CommandText = @"SELECT id, email, phoneNum FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result[col, 0] = reader.GetInt32(0).ToString();
                            result[col, 1] = reader.GetString(1);
                            result[col, 2] = reader.GetString(2);

                            col++;
                        }
                    }
                }
            }

            return result;
        }

        public static void write()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO account (id, email, dob, firstName, lastName, phoneNum, username, pass)
                        VALUES
                        (@ID, @email, @DOB, @first, @last, @phone, @username, @password);";

                    command.Parameters.AddWithValue("@ID", 2);
                    command.Parameters.AddWithValue("@email", "test@test2.com");
                    command.Parameters.AddWithValue("@DOB", "02-21-1998");
                    command.Parameters.AddWithValue("@first", "Bob");
                    command.Parameters.AddWithValue("@last", "Test");
                    command.Parameters.AddWithValue("@phone", "0555555555");
                    command.Parameters.AddWithValue("@username", "test");
                    command.Parameters.AddWithValue("@password", "test");

                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
        }

        public static void newAccount(string email, string dob, string first, string last, string phone, string username, string password)
        {
            int count;

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT TOP 1 * FROM account ORDER BY id DESC;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        if (reader.HasRows == true)
                            count = reader.GetInt32(0) + 1;
                        else
                            count = 1;
                    }
                }
            }

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO account (id, email, dob, firstName, lastName, phoneNum, username, pass)
                        VALUES
                        (@ID, @email, @DOB, @first, @last, @phone, @username, @password);";

                    command.Parameters.AddWithValue("@ID", count);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@first", first);
                    command.Parameters.AddWithValue("@last", last);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
        }

    }
}
