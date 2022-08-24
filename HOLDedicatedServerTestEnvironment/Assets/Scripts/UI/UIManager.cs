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

    [Header("Panels")]
    [SerializeField] private GameObject m_LoginPanel;
    [SerializeField] private GameObject m_SignupPanel;
    [SerializeField] private GameObject m_VerificationPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI m_EmailText;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField m_EmailField;

    public string EmailFieldText => m_EmailField.text;

    /// <summary>
    /// This function gets called in ButtonManager
    /// </summary>
    /// <param name="i_Message"></param>
    public void ShowInfoMessage(string i_Message)
    {
        m_InfoPanel.SetActive(true);
        m_InfoText.text = i_Message;
    }

    public void OpenMenu(eMenuType i_Type)
    {
        DisableAllUI();

        switch (i_Type)
        {
            case eMenuType.Login:
                m_LoginPanel.SetActive(true);
                break;
            case eMenuType.Signup:
                m_SignupPanel.SetActive(true);
                break;
            case eMenuType.Verification:
                m_VerificationPanel.SetActive(true);
                break;
        }
    }

    public void DisableAllUI()
    {
        m_LoginPanel.SetActive(false);
        m_SignupPanel.SetActive(false);
        m_VerificationPanel.SetActive(false);
    }

    public void SetEmailText()
    {
        m_EmailText.text = m_EmailField.text;
    }

    public void ShowLogin()
    {
        m_LoginPanel.SetActive(true);
    }

    public bool IsEmailEmpty()
    {
        if (m_EmailField.text == "")
            return true;
        return false;
    }
}

[System.Serializable]
public enum eMenuType
{
    Login,
    Signup,
    Verification
}