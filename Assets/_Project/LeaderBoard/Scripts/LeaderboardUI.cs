using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private LeaderboardItem itemTemplate;

    public void UpdateUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        ClearUI();

        foreach (PlayerLeaderboardEntry entry in leaderboard)
        {
            LeaderboardItem newItem = Instantiate(itemTemplate, container);
            newItem.FillInfo(entry.Position.ToString(), entry.DisplayName);
        }
    }

    private void ClearUI()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }
    }
}
