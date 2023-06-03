using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField impLat;
    [SerializeField] private TMP_InputField impLon;
    [SerializeField] private Button btnUpdate;
    [Space] 
    [SerializeField] private Map map;

    private void Start()
    {
        impLat.onValueChanged.AddListener(ImpLat_valueChange);
        impLon.onValueChanged.AddListener(ImpLon_valueChange);
        btnUpdate.onClick.AddListener(BtnUpdate_click);
    }

    private void BtnUpdate_click()
    {
        //map.UpdateMap();
    }

    private void ImpLat_valueChange(string value)
    {
        try
        {
            map.lat = (float)Convert.ToDouble(value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private void ImpLon_valueChange(string value)
    {
        try
        {
            map.lon = (float)Convert.ToDouble(value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
