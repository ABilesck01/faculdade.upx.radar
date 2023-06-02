using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreViewItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnBuy;
    [SerializeField] private Image displayImage;
    [Space]
    [SerializeField] private Image icon;
    [SerializeField] private Sprite dmIcon;
    [SerializeField] private Sprite gcIcon;

    private CatalogItem myItem;
    private string virtualCurrency;
    private int price;

    public void SetItem(CatalogItem item)
    {
        myItem = item;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        uint dm = 0;
        uint gc = 0;

        if(myItem.VirtualCurrencyPrices.ContainsKey("DM"))
            dm = myItem.VirtualCurrencyPrices["DM"];
        else if(myItem.VirtualCurrencyPrices.ContainsKey("GC"))
            gc = myItem.VirtualCurrencyPrices["GC"];
      
        if(gc == 0)
        {
            txtPrice.text = dm.ToString();
            virtualCurrency = "DM";
            price = (int)dm;
            icon.sprite = dmIcon;
        }
        else if(dm == 0)
        {
            txtPrice.text = gc.ToString();
            virtualCurrency = "GC";
            price = (int)gc;
            icon.sprite = gcIcon;
        }
        txtName.text = myItem.DisplayName;
        btnBuy.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        PlayFabClientAPI.PurchaseItem(
            new PurchaseItemRequest
            {
                VirtualCurrency = virtualCurrency,
                Price = price,
                ItemId = myItem.ItemId
    }, 
            OnPurchaseSuccess, 
            OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }

    private void OnPurchaseSuccess(PurchaseItemResult result)
    {
        Debug.Log("Bought item");
    }
}
