using MlkPwgen;
using System;
using System.Data.SqlClient;

namespace ScavengeRUs.Data
{
    public static class DBTest
    {
        private static string connectionString = "Server=tcp:scavengerus.database.windows.net,1433;Initial Catalog=ScavengeRUsDB;Persist Security Info=False;User ID=ScavengeRUs;Password=CodingWizards2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void setUserLogin(string email, string pass) {

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM account WHERE email=@email AND pass=@pass;";

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@pass", pass);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        
                        UserLoginInfo.accessCode = Int32.Parse(reader.GetString(0));
                        UserLoginInfo.email = reader.GetString(1);
                        UserLoginInfo.dob = reader.GetString(2);
                        UserLoginInfo.firstName = reader.GetString(3);
                        UserLoginInfo.lastName = reader.GetString(4);
                        UserLoginInfo.phoneNum = reader.GetString(5);
                        UserLoginInfo.username = reader.GetString(6);

                        Console.WriteLine(UserLoginInfo.accessCode);
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

        public static string getURL(string accessCode)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT urlID FROM access WHERE accessCode=@accessCode;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return reader.GetString(0);
                    }
                }
            }

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
           System.Random random = new System.Random();

            List<string> existingCodes = getAvailableAccessCodes();
            int tempCode = random.Next(0, existingCodes.Count);

            accessCode = existingCodes[tempCode];

            addUserToGame(accessCode);

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

        public static void createGameURLs()
        {
            List<string> accessCodes = new List<string>();
            System.Random rand = new System.Random();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode FROM account;";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            accessCodes.Add(reader.GetString(0));
                        }

                    }
                }

                conn.Close();

                long urlID;

                foreach(string accessCode in accessCodes)
                {
                    urlID = rand.Next(501) * rand.Next(501);
                    addToAccessDB(accessCode, urlID.ToString());
                }
            }
        }

        public static void addToAccessDB(string accessCode, string urlID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"INSERT INTO access(accessCode, urlID) VALUES (@accessCode, @urlID);";

                    command.Parameters.AddWithValue("@accessCode", accessCode);
                    command.Parameters.AddWithValue("@urlID", urlID);

                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                }
            }
        }

        public static void removeFromAccessDB(string accessCode)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM access WHERE accessCode=@accessCode;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);

                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                }

            }
        }

        public static bool validateUser(string email, string password)
        {

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM account WHERE email=@email AND pass=@pass;";

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@pass", password);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("User logged in successfully");
                            return true;
                        }
                        else
                            return false;
                    }
                }

            }

        }

        public static string getGuid(string accessCode)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT guid FROM account WHERE accessCode=@accessCode;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.GetString(0);
                    }
                }
            }
        }

        public static string getGuid(string email, string pass)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT guid FROM account WHERE email=@email AND pass=@pass;";

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@pass", pass);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.GetString(0);
                    }
                }
            }
        }

        public static bool hasAccess(string accessCode, string urlID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM access WHERE accessCode=@accessCode AND urlID=@urlID;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);
                    command.Parameters.AddWithValue("@urlID", urlID);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        if(reader.HasRows)
                        {
                            return true;
                        }
                        else
                            return false;

                    }
                }
            }
        }

        public static string getAccessCode(string email, string pass)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode FROM account WHERE email=@email AND pass=@pass;";

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@pass", pass);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                       return reader.GetString(0);

                    }
                }
            }
        }

        public static string login(string accessCode)
        {
            string guid = PasswordGenerator.Generate(length: 10, allowed: Sets.Alphanumerics);

            updateGuid(guid, accessCode);
            removeFromAccessDB(accessCode);
            return guid;
        }

        public static string login(string email, string pass)
        {
            string guid = PasswordGenerator.Generate(length: 10, allowed: Sets.Alphanumerics);
            string accessCode = getAccessCode(email, pass);

            updateGuid(guid, accessCode);

            return guid;
        }

        public static void logout(string guid)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"UPDATE account SET guid=@guid WHERE guid=@oldGuid;";

                    command.Parameters.AddWithValue("@guid", null);
                    command.Parameters.AddWithValue("@oldGuid", guid);

                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                }
            }
        }

        public static void updateGuid(string guid, string accessCode)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"UPDATE account SET guid=@guid WHERE accessCode=@accessCode;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);
                    command.Parameters.AddWithValue("@guid", guid);

                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                }
            }
        }
        public static List<string> getTasks()
        {
            List<string> tasks = new List<string>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('game');";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        while (reader.Read())
                        {
                            tasks.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return tasks;
        }

        public static List<bool> getUserTasks(string guid)
        {
            List<bool> completed = new List<bool>();
            string accessCode;

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode FROM account WHERE guid=@guid;";

                    command.Parameters.AddWithValue("@guid", guid);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        
                        accessCode = reader.GetString(0);
                    }
                }

                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM game WHERE accessCode=@accessCode;";

                    command.Parameters.AddWithValue("@accessCode", accessCode);

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        for(int i = 1; i < reader.FieldCount; i++)
                        {
                            completed.Add(reader.GetBoolean(i));
                        }
                    }
                }
            }

            return completed;
        }

        public static List<string> getUsers()
        {
            List<string> users = new List<string>();
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
                            users.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return users;
        }

        public static string getLocations()
        {
            int length;
            string result = "";
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT count(*) FROM information_schema.columns WHERE table_name = 'game';";

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        length = reader.GetInt32(0);
                    }
                }
            }

            for(int i = 0; i < length - 1; i++)
            {
                result += ", @false";
            }

            return result;
        }

        public static void addUserToGame(string accessCode)
        {
            string locations = getLocations();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"INSERT INTO game VALUES(@accessCode" + locations + ");";

                    command.Parameters.AddWithValue("@accessCode", accessCode);
                    command.Parameters.AddWithValue("@false", 0);


                    conn.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                    }
                }
            }
        }

        public static void addUsersToGame()
        {
            List<string> users = getUsers();
            string locations = getLocations();

            foreach (string user in users) 
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var command = conn.CreateCommand())
                    {

                        command.CommandText = @"INSERT INTO game VALUES(@accessCode" + locations + ");";

                        command.Parameters.AddWithValue("@accessCode", user);
                        command.Parameters.AddWithValue("@false", 0);
                        

                        conn.Open();

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                            conn.Close();
                        }
                    }
                }
            }
        }

        //for sam
        public static List<string> getUserInfo(string guid)
        {
            List<string> result = new List<string>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM account WHERE guid=@guid;";

                    command.Parameters.AddWithValue("@guid", guid);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0)); // access code
                            result.Add(reader.GetString(1)); // email
                            result.Add(reader.GetString(2)); // DOB
                            result.Add(reader.GetString(3)); // first name
                            result.Add(reader.GetString(4)); // last name
                            result.Add(reader.GetString(5)); // phone num
                            result.Add(reader.GetString(6)); // username
                        }
                    }
                }
            }

            return result;
        }

        public static string getAccessFromGuid(string guid)
        {
            string result;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT accessCode FROM account WHERE guid=@guid;";

                    command.Parameters.AddWithValue("@guid", guid);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        result = reader.GetString(0);
                    }
                }
                conn.Close();
            }

            return result;
            
        }

        public static bool validateQR(string QR, string guid)
        {
            bool exists;
            string task_name;
            string accessCode = getAccessFromGuid(guid);
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"SELECT * FROM tasks WHERE qrCode=@qrCode;";

                    command.Parameters.AddWithValue("@qrCode", QR);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
    

                        if (reader.HasRows)
                        {
                            exists = true;
                            task_name = reader.GetString(1);
                        }
                        else
                            exists = false;

                    }
                }
                if (exists)
                {
                    using (var command = conn.CreateCommand())
                    {

                        command.CommandText = @"UPDATE game SET @taskname=1 WHERE accessCode=@accessCode";

                        command.Parameters.AddWithValue("@accessCode", accessCode);

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.Message);
                            conn.Close();
                        }

                    }
                }
            }

            return exists;
        }
    }
}
