using System;

[Serializable]
public class UserEntityTest 
{
    public string ID;
    public string username;
    public string email;
    public string password; // for now (Will super secure this later)
    public int stamina;
    public int level;
    public int experience;
    public int maxexperience;

    public UserEntityTest(string n_ID, string n_username, string n_email, string n_password, int n_level = 1, int n_experience = 0, int n_maxexperience = 300, int n_stamina = 100)
    {
        ID = n_ID;
        username = n_username;
        email = n_email;
        password = n_password;
        level = n_level;
        experience = n_experience;
        maxexperience = n_maxexperience;
        stamina = n_stamina;
    }

}
