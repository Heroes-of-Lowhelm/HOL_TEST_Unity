using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button b_Login, b_DeleteAllAccounts, b_showAccounts, b_createTable, b_verifyOTP, b_googleSignIn, b_applySignIn, b_verifySignupOTP, b_addPassword, b_resendOTP;
    [SerializeField]TMP_InputField i_loginUserName, i_loginPassword, i_signUpUserName, i_signUpEmail, i_signUpPassword, i_signUpConfirmPassword, i_OTPField, i_OTPSignUpField;
    [SerializeField] GameObject g_loginPanel, g_signupPanel, g_twoFAPanel, g_signupPanelStepTwo, g_signupPanelStepThree, g_successfulSignUp;
    [SerializeField] TextMeshProUGUI t_SignupErrorstep1, t_SignupErrorstep2, t_SignupErrorstep3, t_ErrorLogin, t_errorOTPLogin;

    [SerializeField] string levelName;

    private const string s_emailTaken = "Email is already taken";
    private const string s_usernameTaken = "Username is already taken";
    private const string s_wrongOTP = "Invalid OTP";
    private const string s_passwordDoesnotMatch = "Passwords do not match";
    private const string s_invalidcredentials = "Invalid credentials";

    const string success = "Account Successfully Created";
    public void Start()
    {
        b_showAccounts.onClick.AddListener(ShowAccounts);
        b_createTable.onClick.AddListener(CreateTable);
        b_Login.onClick.AddListener(Login);
        b_DeleteAllAccounts.onClick.AddListener(DeleteAllAccounts);
        b_verifyOTP.onClick.AddListener(OnOTPValidate);
#if UNITY_ANDRIOD
        b_googleSignIn.onClick.AddListener(OnGoogleSignIn);
#endif
        b_applySignIn.onClick.AddListener(SubmitInfo);
        b_verifySignupOTP.onClick.AddListener(OnEmailVerification);
        b_addPassword.onClick.AddListener(AddPassord);
        b_resendOTP.onClick.AddListener(ResendOTPSignUp);
    }
    public void CreateTable()
    {
        AuthenticationManagerTest.authManager.CreateDB();
    }
    public void CreateAccount() // Final Step
    {
        AuthenticationManagerTest.authManager.CreateNewAccount(i_signUpPassword.text);
        g_loginPanel.SetActive(true);
        g_signupPanel.SetActive(false);
        t_ErrorLogin.text = string.Empty;
        
    }
    public void SubmitInfo() // Step 1
    {
       int result = AuthenticationManagerTest.authManager.ApplyForRegistration(i_signUpUserName.text, i_signUpEmail.text);
        if (result == 0)
        {
            g_signupPanelStepTwo.SetActive(true);
            g_signupPanel.SetActive(false);
            t_SignupErrorstep2.text = string.Empty;
        }
        else if (result == 1)
        {
            t_SignupErrorstep1.color = Color.red;
            t_SignupErrorstep1.text = s_emailTaken;
        }
        else if (result == 2)
        {
            t_SignupErrorstep1.color = Color.red;
            t_SignupErrorstep1.text = s_usernameTaken;
        }
    }

    public void OnEmailVerification() // Step 2
    {
        bool result = AuthenticationManagerTest.authManager.CheckOTP(i_OTPSignUpField.text, true);
        if (result)
        {
            g_signupPanelStepTwo.SetActive(false);
            g_signupPanelStepThree.SetActive(true);
            t_SignupErrorstep3.text = string.Empty;
        }
        else
        {
            t_SignupErrorstep2.color = Color.red;
            t_SignupErrorstep2.text = s_wrongOTP;
        }
    }
    public void AddPassord() // Step 3
    {
        bool result = AuthenticationManagerTest.authManager.CheckPasswordConfirmation(i_signUpPassword.text, i_signUpConfirmPassword.text);
        if (result)
        {
            CreateAccount();
            g_signupPanelStepThree.SetActive(false);
            g_successfulSignUp.SetActive(true);

        }
        else
        {
            t_SignupErrorstep3.color = Color.red;
            t_SignupErrorstep3.text = s_passwordDoesnotMatch;
        }
    }
    public void CancelAccountCreation(GameObject objectToDisable)
    {
        AuthenticationManagerTest.authManager.CancelAccountCreation();
        g_loginPanel.SetActive(true);
        objectToDisable.SetActive(false);

    }
    public void DeleteAllAccounts()
    {
        AuthenticationManagerTest.authManager.DeleteAllAccounts();
        Debug.LogWarning("All accounts deleted");
    }
    public void ShowAccounts()
    {
        AuthenticationManagerTest.authManager.CheckTable();
    }
    public void Login()
    {
        bool login = AuthenticationManagerTest.authManager.Login(i_loginUserName.text,i_loginPassword.text);
        if (login == true)
        {
            ActivateTwoFactorAuthentication();
        }
        else
        {
            t_ErrorLogin.text = s_invalidcredentials;
            //Send UI Failure feedback
        }
    }
    public void ActivateTwoFactorAuthentication()
    {
        g_twoFAPanel.SetActive(true);
    }

    public void OnOTPValidate() // Login
    {
        bool result = AuthenticationManagerTest.authManager.CheckOTP(i_OTPField.text, false);
        if (result)
        {
            LoadingManager.ChangeLevel(levelName);
        }
        else
        {
            t_errorOTPLogin.text = s_wrongOTP;
        }
    }
#if GOOGLE_ANDROID
    public void OnGoogleSignIn()
    {
        GoogleSignIn.googleSI.OnGoogleSignInPress();
    }
#endif
    public void ResendOTPSignUp()
    {
        AuthenticationManagerTest.authManager.ResendOTPSignUp();
        t_SignupErrorstep2.color = Color.black;
        t_SignupErrorstep2.text = "OTP Resent";
    }
    public void ResendOTPLogIn()
    {
        AuthenticationManagerTest.authManager.ResendOTPLogIn();
        t_errorOTPLogin.color = Color.black;
        t_errorOTPLogin.text = "OTP Resent";
    }


}
