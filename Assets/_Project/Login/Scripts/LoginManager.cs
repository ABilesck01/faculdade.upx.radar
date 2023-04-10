using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class LoginManager : MonoBehaviour
{
    [Header("Register")] 
    [SerializeField] private TMP_InputField txtRegisterEmail;
    [SerializeField] private TMP_InputField txtRegisterPassword;
    [SerializeField] private TMP_InputField txtRegisterRepeatPassword;
    [Header("Login")]
    [SerializeField] private TMP_InputField txtLoginEmail;
    [SerializeField] private TMP_InputField txtLoginPassword;

    public void BtnRegister_click()
    {
        if(!txtRegisterPassword.text.Equals(txtRegisterRepeatPassword.text))
            return;
        
        var request = new RegisterPlayFabUserRequest
        {
            Email = txtRegisterEmail.text,
            Password = txtRegisterPassword.text,
            Username = "Motorista" + SystemInfo.deviceUniqueIdentifier.Substring(0, 4)
        };
        
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void BtnLogin_Click()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = txtLoginEmail.text,
            Password = txtLoginPassword.text
        };
        
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void BtnResetPassword_Click()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = txtLoginEmail.text
        };
        
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnResetPasswordSuccess, OnError);
    }

    private void OnResetPasswordSuccess(SendAccountRecoveryEmailResult obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnError(PlayFabError obj)
    {
        Debug.LogError(obj.GenerateErrorReport());
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        Debug.Log("register");
    }
}
