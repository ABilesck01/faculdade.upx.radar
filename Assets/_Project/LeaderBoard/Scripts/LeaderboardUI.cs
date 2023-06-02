using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{   
    [SerializeField] private Transform container;
    [SerializeField] private LeaderboardItem itemTemplate;
    [Space]
    [SerializeField] private Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(BtnCloseOnClick);
    }

    private void BtnCloseOnClick()
    {
        SceneManager.UnloadSceneAsync(LeaderBoardController.SceneName);
    }

    public void UpdateUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        ClearUI();

        foreach (PlayerLeaderboardEntry entry in leaderboard)
        {
            LeaderboardItem newItem = Instantiate(itemTemplate, container);
            newItem.FillInfo((entry.Position + 1).ToString(), entry.DisplayName);
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
