using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button btnRanking;
    [SerializeField] private Button btnStore;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnStartGame;
    [Space]
    [SerializeField] private TextMeshProUGUI txtCoins;
    [SerializeField] private TextMeshProUGUI txtDiamonds;

    private void Start()
    {
        btnRanking.onClick.AddListener(BtnRankingOnClick);
        btnStore.onClick.AddListener(BtnStoreOnClick);
        btnSettings.onClick.AddListener(btnSettingsgOnClick);
        btnStartGame.onClick.AddListener(BtnStartGameOnClick);

        GetVirtualCurrencies();
    }

    private void BtnRankingOnClick()
    {
        SceneManager.LoadScene(LeaderBoardController.SceneName, LoadSceneMode.Additive);
    }

    private void BtnStoreOnClick()
    {
        throw new NotImplementedException();
    }

    private void btnSettingsgOnClick()
    {
        throw new NotImplementedException();
    }

    private void BtnStartGameOnClick()
    {
        throw new NotImplementedException();
    }

    private void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(
            new GetUserInventoryRequest(),
            OnGetUserInventoryRequestSuccess,
            OnError
            );
    }

    private void OnGetUserInventoryRequestSuccess(GetUserInventoryResult result)
    {
        txtCoins.text = result.VirtualCurrency["GC"].ToString("###,###,##0");
        txtDiamonds.text = result.VirtualCurrency["DM"].ToString("###,###,##0");
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
}
