using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Map : MonoBehaviour
{
    public string apiKey = "AIzaSyDPkg2ZRNFhmgOMVQr6XjtzPAovdU8rj90";
    public float lat;
    public float lon;
    public int zoom;
    public bool realTimeMap;

    public enum Resolution
    {
        Low = 1,
        High = 2
    };

    public Resolution mapResolution;
    public enum Type
    {
        Roadmap = 1,
        Satellite,
        Hybrid,
        Terrain
    }

    public Type mapType;

    private string _url;
    private int _mapWidth = 1024;
    private int _mapHeight = 1024;
    private bool _mapIsLoading;
    private Rect _rect;
    private string _apiKeyLast;

    private float _lastLat;
    private float _lastLon;
    private int _lastZoom;
    private Resolution _lastResolution;
    private Type _lastType;
    private bool updateMap;

    private void Start()
    {
        //StartCoroutine(GetGoogleMap());
    }

    // private void Update()
    // {
    //     if(!updateMap) return;
    //
    //     if (_apiKeyLast != apiKey || !Mathf.Approximately(_lastLat, lat) || !Mathf.Approximately(_lastLon, lon) ||
    //         _lastZoom != zoom || _lastResolution != mapResolution || _lastType != mapType)
    //     {
    //         StartCoroutine(GetGoogleMap());
    //         updateMap = false;
    //     }
    // }

    [ContextMenu("Update map")]
    public void UpdateMap(float lat, float lon)
    {
        this.lat = lat;
        this.lon = lon;

        StartCoroutine(GetGoogleMap());
    }

    private IEnumerator GetGoogleMap()
    {
        _url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoom +
               "&size=" + _mapWidth + "x" + _mapHeight + "&scale=" + mapResolution + "&maptype=" + mapType + "&key=" +
               apiKey;
        _mapIsLoading = true;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("WWW ERROR" + www.error);
        }
        else
        {
            _mapIsLoading = false;
            GetComponent<MeshRenderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture; 
                //.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            _apiKeyLast = apiKey;
            _lastLat = lat;
            _lastLon = lon;
            _lastZoom = zoom;
            _lastResolution = mapResolution;
            _lastType = mapType;
            updateMap = true;
        }
    }
}
