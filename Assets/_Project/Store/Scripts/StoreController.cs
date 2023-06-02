using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public static string SceneName = "Store";

    [SerializeField] private Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(BtnCloseOnClick);
    }

    private void BtnCloseOnClick()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
}
