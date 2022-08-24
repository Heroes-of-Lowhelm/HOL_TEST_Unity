using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public static void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


}
