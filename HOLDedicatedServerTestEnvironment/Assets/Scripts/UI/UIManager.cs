using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Info Panel")]
    [SerializeField] private GameObject m_InfoPanel;
    [SerializeField] private TextMeshProUGUI m_InfoText;

    /// <summary>
    /// This function gets called in ButtonManager
    /// </summary>
    /// <param name="i_Message"></param>
    public void ShowInfoMessage(string i_Message)
    {
        m_InfoPanel.SetActive(true);
        m_InfoText.text = i_Message;
    }
}
