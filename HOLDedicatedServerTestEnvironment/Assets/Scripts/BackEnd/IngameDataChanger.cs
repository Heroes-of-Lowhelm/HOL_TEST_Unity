using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IngameDataChanger : MonoBehaviour
{
    public UIDisplayManager displayManager;
    public string loginScreen;
    public void ChangeStamina(int amount)
    {
        DataStuff.data.stamina += amount;
        if (DataStuff.data.stamina > 100)
        {
            DataStuff.data.stamina = 100;
        }
        if (DataStuff.data.stamina < 0)
        {
            DataStuff.data.stamina = 0;
        }
        displayManager.StaminaDisplayUpdate();
        AuthenticationManagerTest.authManager.SaveData();
    }
   public void GainXP(int amount)
    {
        DataStuff.data.experience += amount * DataStuff.data.level;
        if (DataStuff.data.experience > DataStuff.data.maxExperience)
        {
            DataStuff.data.level += 1;
            DataStuff.data.experience = 0;
            DataStuff.data.maxExperience *= 2;
            displayManager.LevelDisplayUpdate();
        }
        displayManager.ExperienceDisplayUpdate();
        AuthenticationManagerTest.authManager.SaveData();
    }
    public void LogOut()
    {
        AuthenticationManagerTest.authManager.LogOut();
        SceneManager.LoadScene(loginScreen);
    }
}
