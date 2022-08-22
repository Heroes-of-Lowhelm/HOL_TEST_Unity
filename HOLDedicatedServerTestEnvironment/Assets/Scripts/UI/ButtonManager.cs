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
}
