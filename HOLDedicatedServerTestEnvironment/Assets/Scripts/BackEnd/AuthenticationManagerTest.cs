using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Text;
using System.Security.Cryptography;


public class AuthenticationManagerTest : MonoBehaviour 
{
    public static AuthenticationManagerTest authManager;
    private string dbName;
    private int OTP;

    private string loggingInUserID;

    private string signingUpUserName;
    private string signingUpEmail;

    public const string authSentence = "Your verification code is ";
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (authManager != null)
        {
            Destroy(transform.root.gameObject);
        }
        else
        {
            authManager = this;
        }
        dbName = "URI=file:" + Application.persistentDataPath + "/Users.db";
        CreateDB();
    }
    //--------------------------------------------
    public void DeleteAccount()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM users WHERE id " + DataStuff.data.ID;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    //--------------------------------------------
    public void CreateNewAccount(string s_password)
    {
        Debug.Log("Creating account...");
            UserEntityTest newuser = new UserEntityTest(RandomIdGenerator.GenerateID(), signingUpUserName, signingUpEmail, EncryptPassword(s_password));
            DataStuff.data.ID = newuser.ID; // Create the data first to be saved as json 
            DataStuff.data.username = newuser.username;
            DataStuff.data.email = newuser.email;
            DataStuff.data.level = newuser.level;
            DataStuff.data.experience = newuser.experience;
            DataStuff.data.maxExperience = newuser.maxexperience;
            DataStuff.data.password = newuser.password;
            DataStuff.data.stamina = newuser.stamina;
            // Add the new User to the SQL users Table
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Add to Database
                    command.CommandText = "INSERT INTO users (id, username, email, password, savejsonfile) VALUES ('" + newuser.ID + "', '" + newuser.username + "', '" + newuser.email + "', '" + newuser.password + "', '" + JsonifyData(DataStuff.data) + "'); ";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            LogOut(); // Clear the data for security (player must log in again after signing up).
    } // Final Step
    //--------------------------------------------
    public int ApplyForRegistration(string userName, string email)
    {
        bool emailExists = CheckforExistingCredentails(email, "email");
        bool usernameExists = CheckforExistingCredentails(userName, "username");
        if (emailExists == true)
        {
            Debug.LogError("Email is already taken");
            return 1;
        }
        else if (usernameExists == true)
        {
            Debug.LogError("Username is already taken");
            return 2;
        }
        else
        {
            Debug.Log("Username and Email is available, sending email...");
            signingUpUserName = userName;
            signingUpEmail = email;
            OTP = AlternateSendEmail.alterMail.CreateCode();
            SendOTP(email, OTP);
            return 0;
        }
    } // Step1
    //--------------------------------------------
    public void CancelAccountCreation()
    {
        signingUpEmail = string.Empty;
        signingUpUserName = string.Empty;
        OTP = 0;
    }
    //--------------------------------------------
    public void LogOut() // Public method na reset nalang
    {
        SaveData();
        DataStuff.data = null;
    }
    //--------------------------------------------
    public void SaveData()
    {
        //DATA NOT SAVING IN JSON!!! Check this out soon!!!
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE users SET savejsonfile = '" + JsonifyData(DataStuff.data) + "' WHERE id = '" + DataStuff.data.ID + "';";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        print("Data should save");
    }
    //--------------------------------------------
    public string JsonifyData(DataStuff data)
    {
        UserEntityTest user = new UserEntityTest(data.ID, data.username, data.email, data.password, data.level, data.experience, data.maxExperience, data.stamina);
        string json = JsonUtility.ToJson(user);
        return json;
    }
    //--------------------------------------------
    public bool CheckforExistingCredentails(string credentials, string tableName)
    {
        bool exists = false;
        try
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM users;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[tableName].ToString() == credentials)
                            {
                                exists =  true;
                            }
                            else
                            {
                                exists = false;
                            }
                        }
                    }
                }
                connection.Close();
            }
            return exists;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to check table because: " + e);
            return true;
        }
    }
    //--------------------------------------------
    public void DeleteAllAccounts()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM users;"; // Research .NET for SQL command for SDK
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    //--------------------------------------------
    public string EncryptPassword(string password) 
    {
        byte[] convertedpassword = Encoding.UTF8.GetBytes(password);

        SHA256Managed sha = new SHA256Managed();

        byte[] hashValue = sha.ComputeHash(convertedpassword);
        return GetHexStringFromHash(hashValue);
    }
    //--------------------------------------------
    private static string GetHexStringFromHash(byte[] hash)
    {
        string hexString = string.Empty;
        foreach (byte b in hash)
        {
            hexString += b.ToString("x2");
        }
        return hexString;
    }
    //--------------------------------------------
    public void CheckTable() // checking muna
    {
        try
        {
            Debug.Log("Checking table...");
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM users;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log("ID: " + reader["id"] + "\tUsername: " + reader["username"] + "\tEmail: " + reader["email"] + "\tPassword: " + reader["password"] + "\tData: " + reader["savejsonfile"]);
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to check table because: " + e);
        }
    }
    //--------------------------------------------
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS users (id VARCHAR(40), username VARCHAR(20), email VARCHAR(40), password VARCHAR(4000), savejsonfile VARCHAR(4000));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    //--------------------------------------------
    public bool Login(string username, string password)
    {
        string expectedPassword = string.Empty;
        string loginUserEmail = string.Empty;
        string convertedPassword = EncryptPassword(password);
       // try
       // {
            bool success = false;
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM users WHERE username = '" + username + "' OR email = '" + username + "';";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedPassword = reader["password"].ToString();

                            if (convertedPassword == expectedPassword)
                            {
                                OTP = AlternateSendEmail.alterMail.CreateCode();
                                loginUserEmail = reader["email"].ToString();
                                loggingInUserID = reader["id"].ToString();
                                SendOTP(loginUserEmail,OTP);
                                success = true;
                            }
                            else
                            {
                                Debug.LogError("Wrong Password");
                                success = false;
                            }
                        }
                    }
                }
                connection.Close();
            }
            if (success)
            {
                return true;
            }
            else
            {
                Debug.LogError("Invalid Credentials");
                return false;
            }
       // }
       // catch (Exception e)
       // {
       //     Debug.LogError("Invalid Credentials Error: " + e);
       //     return false;
       // }
    }
    //--------------------------------------------
    public async void SendOTP(string emailAddress, int OTPs)
    {
        bool result = await AlternateSendEmail.alterMail.SendEmail(emailAddress, authSentence + OTPs);
        if (result == true)
        {
            Debug.Log("Email sent");
        }
        else
        {
            Debug.LogError("Email not sent");
        }
    }
    //--------------------------------------------
    public void ResendOTPSignUp()
    {
        OTP = AlternateSendEmail.alterMail.CreateCode();
        SendOTP(signingUpEmail, OTP);
    }
    public void ResendOTPLogIn()
    {
        OTP = AlternateSendEmail.alterMail.CreateCode();
        string loginUserEmail = string.Empty;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users WHERE id = '" + loggingInUserID + "';";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loginUserEmail = reader["email"].ToString();
                        SendOTP(loginUserEmail, OTP);
                    }
                }
            }
            connection.Close();
        }
    }
    //--------------------------------------------
    public bool CheckOTP(string OTPSent, bool signUp)
    {
        if (OTPSent == OTP.ToString())
        {
            OTP = 0;
            if (signUp == false)
            {
                LoadData(loggingInUserID);
            }
            return true;
        }
        else
        {
            return false;
        }
    } // Step2
    //--------------------------------------------
    public bool CheckPasswordConfirmation(string passwordSent, string confirmPassword)
    {
        if (passwordSent == confirmPassword)
        {
            return true;
        }
        else
        {
            return false;
        }
    } // Step3 
    //--------------------------------------------
    public void LoadData(string userID)
    {
        string datajsonToLoad = string.Empty;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users WHERE id = '" + loggingInUserID +"';";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        datajsonToLoad = reader["savejsonfile"].ToString();
                    }
                }
            }
            connection.Close();
        }
      
        UserEntityTest user = JsonUtility.FromJson<UserEntityTest>(datajsonToLoad);
        DataStuff data = new DataStuff() {
            username = user.username,
            ID = user.ID,
            level = user.level,
            experience = user.experience,
            maxExperience = user.maxexperience,
            stamina = user.stamina
            };
        DataStuff.data = data;
        print("Data Should load");
    }
    //--------------------------------------------



}
