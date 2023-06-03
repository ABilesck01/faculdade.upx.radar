using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate;
    [Space]
    public float updateInterval = 1f; // Intervalo de atualização em segundos
    [SerializeField] private TextMeshProUGUI txtSpeedometer;
    [SerializeField] private Map map;
    [Space]
    [SerializeField] private SpeedometerPoints points;
    
    private float latitude;
    private float longitude;
    private float previousLatitude;
    private float previousLongitude;
    private float speed;

    private void Update()
    {
        // Atualiza as coordenadas do GPS
        if (Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            
            RotateObject(CalculateDirection());
        }
    }

    private float CalculateDirection()
    {
        float deltaLongitude = Mathf.Deg2Rad * (previousLongitude - longitude);
        float y = Mathf.Sin(deltaLongitude) * Mathf.Cos(Mathf.Deg2Rad * previousLatitude);
        float x = Mathf.Cos(Mathf.Deg2Rad * latitude) * Mathf.Sin(Mathf.Deg2Rad * previousLatitude) -
                  Mathf.Sin(Mathf.Deg2Rad * latitude) * Mathf.Cos(Mathf.Deg2Rad * previousLatitude) *
                  Mathf.Cos(deltaLongitude);

        float direction = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return direction;
    }

    private void RotateObject(float direction)
    {
        objectToRotate.rotation = Quaternion.Euler(0f, direction, 0f);
    }
    
    private void CalculateSpeed()
    {
        // Calcula a velocidade com base nas coordenadas atuais e anteriores
        float distance = CalculateDistance(latitude, longitude, previousLatitude, previousLongitude);
        speed = distance / updateInterval;

        // Armazena as coordenadas atuais para a próxima atualização
        previousLatitude = latitude;
        previousLongitude = longitude;

        Debug.Log("Velocidade: " + speed + " m/s");
        txtSpeedometer.text = (speed * 3.6f).ToString("#00");
        
        if(speed > 0)
            map.UpdateMap(latitude, longitude);
        
        points.MeasureSpeed(speed * 3.6f);
    }

    private float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        // Calcula a distância em metros entre duas coordenadas usando a fórmula do haversine
        const float earthRadius = 6371f; // Raio médio da Terra em quilômetros

        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float a = Mathf.Sin(dLat / 2f) * Mathf.Sin(dLat / 2f) +
                  Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                  Mathf.Sin(dLon / 2f) * Mathf.Sin(dLon / 2f);

        float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));

        float distance = earthRadius * c * 1000f; // Multiplica por 1000 para converter para metros

        return distance;
    }
    
    private void OnDisable()
    {
        // Para a leitura do GPS quando o script é desativado
        if (Input.location.isEnabledByUser)
        {
            Input.location.Stop();
        }
    }

    public void StartMeasure()
    {
        Debug.Log("start measure");
        
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // Inicia o cálculo da velocidade com a primeira leitura do GPS
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start(5, 1);
            InvokeRepeating(nameof(CalculateSpeed), updateInterval, updateInterval);
        }
        else
        {
            PopupController.ShowPopup("Aviso", "GPS não está habilitado no dispositivo.");
            Debug.LogError("GPS não está habilitado no dispositivo.");
            txtSpeedometer.text = "0";
        }
    }
    
    public void StopMeasure()
    {
        Debug.Log("stop measure");
        
        if (Input.location.isEnabledByUser)
        {
            Input.location.Stop();
        }
        CancelInvoke(nameof(CalculateSpeed));
    }
}
