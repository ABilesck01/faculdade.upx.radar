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
    [SerializeField] private Button btnBuy;

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
        txtPrice.text = dm.ToString();
    }
}
