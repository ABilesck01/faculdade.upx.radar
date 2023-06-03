using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedometerPoints : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Image gfxButton;
    [SerializeField] private TextMeshProUGUI txtButton;
    [SerializeField] private GameObject speedometerGameObject;
    [Space]
    [SerializeField] private Color playColor;
    [SerializeField] private Color stopColor;
    [Space] 
    [SerializeField] private Speedometer speedometer;
    [Header("Status")] 
    [SerializeField] private int maxLives = 10;
    [SerializeField] private float maxSpeed = 60;
    [SerializeField] private TextMeshProUGUI txtLives;
    
    private bool isPlaying;
    private int currentLives;
    
    private void Start()
    {
        btnPlay.onClick.AddListener(BtnPlayOnClick);
        StopPlaying();
    }

    private void BtnPlayOnClick()
    {
        if (isPlaying)
        {
            StopPlaying();
        }
        else
        {
            StartPlaying();
        }
    }


    private void StartPlaying()
    {
        gfxButton.color = stopColor;
        txtButton.text = "Parar";
        speedometerGameObject.SetActive(true);
        speedometer.StartMeasure();
        isPlaying = true;
        currentLives = 10;
        txtLives.text = currentLives.ToString("00");
    }

    public void MeasureSpeed(float speed)
    {
        float maxSpeedTolerance = maxSpeed * 1.2f; //20% of max speed
        if (speed >= maxSpeedTolerance)
        {
            currentLives--;
            txtLives.text = currentLives.ToString("00");
            if (currentLives <= 0)
            {
                StopPlaying();
                Debug.Log("lost the game!");
                PopupController.ShowPopup("Aviso", "Você perdeu todas suas vidas! Não irá ganhar nenhum ponto nesta rodada.");
            }
        }
    }
    
    private void StopPlaying()
    {
        if (currentLives > 0)
        {
            ShowFinishMessage();
        }
        
        gfxButton.color = playColor;
        txtButton.text = "Correr";
        speedometerGameObject.SetActive(false);
        speedometer.StopMeasure();
        isPlaying = false;
    }

    private void ShowFinishMessage()
    {
        LeaderBoardController.SendLeaderboard(currentLives * 10);
        
        AddCurrency("DM", 1, result =>
        {
            AddCurrency("GC", 50, currencyResult =>
            {
                FindObjectOfType<MainMenuController>().GetVirtualCurrencies();
            });
        });
        
        PopupController.ShowPopup("Aviso", "Parabéns pela corrida completa! Aqui esta sua recompensa! " +
                                           "\nContinue assim!");
    }

    private void AddCurrency(string currency, int amount, Action<ModifyUserVirtualCurrencyResult> onSuccess)
    {
        PlayFabClientAPI.AddUserVirtualCurrency(
            new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = currency,
                Amount = amount
            },
            onSuccess,
            OnError);
    }

    private void OnError(PlayFabError obj)
    {
        PopupController.ShowPopup("Erro", obj.ErrorMessage);
    }
}
