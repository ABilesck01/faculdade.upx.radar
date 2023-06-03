using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public static string SceneName = "Popup";

    [SerializeField] private TextMeshProUGUI txtTittle;
    [SerializeField] private TextMeshProUGUI txtMessage;
    [SerializeField] private Button btnPositive;
    [SerializeField] private Button btnNegative;
    
    private static string _staticTittle;
    private static string _staticMessage;
    private static UnityAction _staticPositiveAction;
    private static UnityAction _staticNegativeAction;
    
    public static void ShowPopup(string tittle, string message, UnityAction onPositiveButton = null,
        UnityAction onNegativeButton = null)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        
        _staticTittle = tittle;
        _staticMessage = message;
        _staticPositiveAction = onPositiveButton;
        _staticNegativeAction = onNegativeButton;
    }

    private void Start()
    {
        btnPositive.onClick.RemoveAllListeners();
        btnNegative.onClick.RemoveAllListeners();
        
        txtTittle.text = _staticTittle;
        txtMessage.text = _staticMessage;
        if(_staticPositiveAction != null)
            btnPositive.onClick.AddListener(_staticPositiveAction);

        if (_staticNegativeAction != null)
        {
            btnNegative.onClick.AddListener(_staticNegativeAction);
            btnNegative.onClick.AddListener(Close);
            btnNegative.gameObject.SetActive(true);
        }
        else
        {
            btnNegative.gameObject.SetActive(false);
        }
        
        btnPositive.onClick.AddListener(Close);
    }

    private void Close()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
}
