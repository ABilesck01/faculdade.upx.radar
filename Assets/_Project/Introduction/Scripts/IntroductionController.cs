using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroductionController : MonoBehaviour
{
    public static readonly string SceneName = "Intro";

    [SerializeField] private Button btnContinue;

    private void Start()
    {
        btnContinue.onClick.AddListener(BtnContinueClick);
    }

    private void BtnContinueClick()
    {
        SceneManager.LoadScene(MainGameManager.SceneName);
    }
}
