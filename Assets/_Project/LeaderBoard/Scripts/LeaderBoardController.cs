using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public const string SceneName = "Ranking";
    public const string LeaderBoardName = "leaderboard";

    [SerializeField] private LeaderboardUI leaderboardUI;

    private void Start()
    {
        GetLeaderboard();
    }

    private void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = LeaderBoardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };
        
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
    }

    public static void SendLeaderboard(int points)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = LeaderBoardName,
                    Value = points
                }
            }
        };
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendLeaderboard, OnError);
    }

    private static void OnSendLeaderboard(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Leaderboard updated!");
    }

    private static void OnError(PlayFabError obj)
    {
        Debug.LogError(obj.ErrorMessage);
        PopupController.ShowPopup("Erro", obj.ErrorMessage);
    }

    private void OnGetLeaderboard(GetLeaderboardResult obj)
    {
        leaderboardUI.UpdateUI(obj.Leaderboard);
    }
}
