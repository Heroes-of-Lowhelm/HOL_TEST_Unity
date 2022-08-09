using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using TMPro;

public class GoogleSignIn : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfiguration;
    public TextMeshProUGUI t_status;
    public TextMeshProUGUI t_description;

    public static GoogleSignIn googleSI;

    private void Start()
    {
        ConfigureGPGS();
        if (googleSI == null)
        {
            googleSI = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }
    internal void SignIntoGSPS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
         {
             t_status.text = "Authenticating...";
             if (code == SignInStatus.Success)
             {
                 t_status.text = "Success!";
                 t_description.text = "Hi " + Social.localUser.userName + " your ID is " + Social.localUser.id;
             }
             else
             {
                 t_status.text = "Failed to Authenticate";
                 t_description.text = "It's a failure because " + code;
             }
         });

    }
    public void OnGoogleSignInPress()
    {
        SignIntoGSPS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }
    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        t_status.text = "Signed Out";
        t_description.text = string.Empty;
    }

}
