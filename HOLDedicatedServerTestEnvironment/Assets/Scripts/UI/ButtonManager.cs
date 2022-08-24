using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * If you want to call ButtonManager methods
 * Call it like:
 * Managers.ButtonManager.<method>;
 * Example: Managers.ButtonManager.ShowMessage("hi");
 */

public class ButtonManager : MonoBehaviour
{

    /// <summary>
    /// Gets called in UI buttons in the hierarchy
    /// </summary>
    /// <param name="i_Message"></param>
    public void ShowMessage(string i_Message)
    {
        Managers.UIManager.ShowInfoMessage(i_Message);
    }


    #region Buttons
    /*
     * These buttons are being called in buttons in unity inspector menu
     */

    public void LoadSceneButton()
    {

    }

    public void OpenLogin()
    {
        Managers.UIManager.OpenMenu(eMenuType.Login);
    }

    public void OpenRegister()
    {
        Managers.UIManager.OpenMenu(eMenuType.Signup);
    }

    public void OpenVerification()
    {
        Managers.UIManager.OpenMenu(eMenuType.Verification);
    }

    public void SignupUser()
    {
        if (Managers.UIManager.EmailFieldText == "")
        {
            Managers.UIManager.ShowInfoMessage("Please enter your email.");
            return;
        }
        Managers.UIManager.SetEmailText();
        OpenVerification();
    }
    #endregion
}
