using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI stamina, experience, level, username;

    // Start is called before the first frame update
    void Start()
    {
        username.text = "Username: " + DataStuff.data.username;
        level.text = "Level: " + DataStuff.data.level;
        stamina.text = "Stamina: " + DataStuff.data.stamina.ToString() + "/100";
        experience.text = "Experience: " + DataStuff.data.experience + "/" + DataStuff.data.maxExperience;
    }
    public void StaminaDisplayUpdate()
    {
        stamina.text = "Stamina: " + DataStuff.data.stamina.ToString() + "/100";
    }
    public void ExperienceDisplayUpdate()
    {
        experience.text = "Experience: " + DataStuff.data.experience + "/" + DataStuff.data.maxExperience;
    }    
    public void LevelDisplayUpdate()
    {
        level.text = "Level: " + DataStuff.data.level;
    }
}
