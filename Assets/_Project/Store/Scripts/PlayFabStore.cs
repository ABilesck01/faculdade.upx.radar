using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabStore : MonoBehaviour
{
    [SerializeField] private StoreViewItem itemPrefab;
    [SerializeField] private Transform containerBrindes;
    [SerializeField] private Transform containerPowerups;

    private List<CatalogItem> catalogBrindes;
    private List<CatalogItem> catalogPowerups;

    private void Start()
    {
        if(PlayFabClientAPI.IsClientLoggedIn())
        {
            GetCatalog("Brindes", "BrindesStore", catalogBrindes, containerBrindes);
            GetCatalog("powerUps", "PowerUpStore", catalogPowerups, containerPowerups);
        }
    }

    private void GetStore(string catalog, string store, List<CatalogItem> catalogList, Transform container)
    {
        PlayFabClientAPI.GetStoreItems
        (
            new GetStoreItemsRequest
            {
                CatalogVersion = catalog,
                StoreId = store
            },
            result =>
            {
                OnGetStoreSuccess(catalogList, container, result);
            },
            OnError
        );
    }

    private void OnGetStoreSuccess(List<CatalogItem> catalogList, Transform container, GetStoreItemsResult result)
    {
        foreach (StoreItem item in result.Store)
        {
            var catalogItem = catalogList.Find(x => x.ItemId == item.ItemId);

            if (catalogItem != null)
            {
                var itemInstance = Instantiate(itemPrefab, container);
                itemInstance.SetItem(catalogItem);
            }
        }
    }

    private void GetCatalog(string catalogVersion, string store, List<CatalogItem> catalogList, Transform container)
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion
        },
        result =>
        {
            catalogList = result.Catalog;
            GetStore(catalogVersion, store, catalogList, container);
        }
        , OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error);
    }
}
