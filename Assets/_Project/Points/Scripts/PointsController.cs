using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private int points = 100;
    
    [ContextMenu("Send points")]
    private void SendPoints()
    {
        LeaderBoardController.SendLeaderboard(points);
    }
}
