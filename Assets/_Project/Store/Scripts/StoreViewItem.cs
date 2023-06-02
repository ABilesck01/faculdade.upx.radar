using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreViewItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite gcIcon;
    [SerializeField] private Sprite dmIcon;

    private StoreItem myItem;

    public void SetItem(StoreItem item)
    {
        myItem = item;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        //uint gc = myItem.VirtualCurrencyPrices["GC"];
        uint dm = myItem.VirtualCurrencyPrices["DM"];

        txtName.text = myItem.ItemId;
        icon.sprite = dmIcon;
        txtPrice.text = dm.ToString();
        //if (gc == 0)
        //{
            
        //}
        //else if(dm == 0)
        //{
        //    icon.sprite= gcIcon;
        //    txtPrice.text = gc.ToString();
        //}
    }
}
