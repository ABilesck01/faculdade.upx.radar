using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gps : MonoBehaviour
{
    [SerializeField] private Map map;

    [SerializeField] private TextMeshProUGUI txtDebug;
    
    private void Start()
    {
        //StartCoroutine(Location());
    }

    private IEnumerator Location()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }
        
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            txtDebug.text = "Android and Location not enabled";
            yield break;
        }
        
        Input.location.Start(100, 100f);
                
        // Wait until service initializes
        int maxWait = 15;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }
        
        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif
        
        if (maxWait < 1) {
            // TODO Failure
            Debug.LogFormat("Timed out");
            txtDebug.text = "Time out";
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            txtDebug.text = "Unable to determine device location. Failed with status " + Input.location.status;
            yield break;
        }

        Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
        // Access granted and location value could be retrieved
        Debug.LogFormat("Location: "
                        + UnityEngine.Input.location.lastData.latitude + " "
                        + UnityEngine.Input.location.lastData.longitude + " "
                        + UnityEngine.Input.location.lastData.altitude + " "
                        + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                        + UnityEngine.Input.location.lastData.timestamp);

        txtDebug.text = "Location: "
                        + UnityEngine.Input.location.lastData.latitude + " "
                        + UnityEngine.Input.location.lastData.longitude + " "
                        + UnityEngine.Input.location.lastData.altitude + " "
                        + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                        + UnityEngine.Input.location.lastData.timestamp;
            
        map.lat = UnityEngine.Input.location.lastData.latitude;
        map.lon = UnityEngine.Input.location.lastData.longitude;
        map.UpdateMap();
    }
}
