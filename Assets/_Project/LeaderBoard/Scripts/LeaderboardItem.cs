using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtPosition;
    [SerializeField] private TextMeshProUGUI txtDisplayName;

    public void FillInfo(string pos, string displayName)
    {
        txtPosition.text = pos;
        txtDisplayName.text = displayName;
    }
}
