using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/new store item")]
public class StoreItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemCost;
    public currencyType currencyType;

}

public enum currencyType
{
    DM,
    GC
}
