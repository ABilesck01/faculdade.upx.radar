using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static string SceneName = "Settings";
    public static float MaxSpeed;

    [Header("Speedometer")] 
    [SerializeField] private Slider sliderSpeedometer;
    [SerializeField] private TextMeshProUGUI txtMaxSpeed;
    [Space]
    [SerializeField] private Button btnBack;
    [Space] 
    [SerializeField] private TMP_InputField inputDisplayName;
    [SerializeField] private Button btnSendDisplayName;
    [Space] 
    [SerializeField] private TextMeshProUGUI txtVersion;
    private void Start()
    {
        btnBack.onClick.AddListener(OnBtnBackClick);
        sliderSpeedometer.onValueChanged.AddListener(OnSliderSpeedometerChange);
        btnSendDisplayName.onClick.AddListener(BtnSendDisplayNameClick);
        txtVersion.text = $"v {Application.version}";
    }

    private void BtnSendDisplayNameClick()
    {
        if(string.IsNullOrEmpty(inputDisplayName.text)) return;
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = inputDisplayName.text
            },
            ResultCallback, ErrorCallback);
    }

    private void ErrorCallback(PlayFabError obj)
    {
        Debug.LogError(obj.ErrorMessage);
    }

    private void ResultCallback(UpdateUserTitleDisplayNameResult obj)
    {
        
    }

    private void OnSliderSpeedometerChange(float value)
    {
        txtMaxSpeed.text = value.ToString("#00");
        MaxSpeed = value;

    }

    private void OnBtnBackClick()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
}
