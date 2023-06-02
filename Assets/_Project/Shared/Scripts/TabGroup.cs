using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> tabButtons = new List<TabButton>();
    [SerializeField] private List<GameObject> tabs = new List<GameObject>();
    [Space]
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabSelected;

    private TabButton selectedTabButton;

    private void Start()
    {
        OnTabSelect(tabButtons[0]);
    }

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTabButton == null || button != selectedTabButton)
            button.background.sprite = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelect(TabButton button)
    {
        selectedTabButton = button;
        ResetTabs();
        button.background.sprite = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].SetActive(i == index);
        }
    }

    private void ResetTabs()
    {
        foreach(TabButton button in tabButtons) 
        {
            if (selectedTabButton != null && button == selectedTabButton)
                continue;

            button.background.sprite = tabIdle;
        }
    }
}
