using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabStore : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private StoreViewItem itemPrefab;

    private void Start()
    {
        GetStore("Brindes", "BrindesStore");
    }

    private void GetStore(string catalog, string store)
    {
        PlayFabClientAPI.GetStoreItems
        (
            new GetStoreItemsRequest
            {
                CatalogVersion = catalog,
                StoreId = store
            },
            OnSuccess,
            OnError
        );
    }

    private void OnSuccess(GetStoreItemsResult result)
    {
        foreach (StoreItem item in result.Store)
        {
            var itemInstance = Instantiate(itemPrefab, container);
            itemInstance.SetItem(item);
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error);
    }
}
