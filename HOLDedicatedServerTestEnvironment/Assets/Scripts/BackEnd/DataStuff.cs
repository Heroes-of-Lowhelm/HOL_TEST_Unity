using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataStuff : MonoBehaviour // This is the data that will be in Game
{
    public int level;
    public int experience;
    public int maxExperience;
    public int stamina;

    public string username;
    public string ID;

    public string password;
    public string email;



    public static DataStuff data;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (data != null)
        {
            Destroy(gameObject);
        }
        else
        {
            data = this;
        }
    }
}
