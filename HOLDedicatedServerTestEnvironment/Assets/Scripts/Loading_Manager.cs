using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Manager : MonoBehaviour
{
    public string SceneToLoad;

    public void SwitchScene(string i_SceneName)
    {
        SceneToLoad = i_SceneName;
        SceneManager.LoadScene(eSceneType.LoadingScene.ToString());
    }
}

[System.Serializable]
public enum eSceneType
{
    LoginTest,
    MainMenuTest,
    LoadingScene,
    GameplayScene
}
