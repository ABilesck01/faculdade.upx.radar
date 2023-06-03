using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public float updateInterval = 1f; // Intervalo de atualização em segundos
    [SerializeField] private TextMeshProUGUI txtSpeedometer;
    [SerializeField] private Map map;

    private float latitude;
    private float longitude;
    private float previousLatitude;
    private float previousLongitude;
    private float speed;

    private void Start()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // Inicia o cálculo da velocidade com a primeira leitura do GPS
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            InvokeRepeating("CalculateSpeed", updateInterval, updateInterval);
        }
        else
        {
            Debug.LogError("GPS não está habilitado no dispositivo.");
            txtSpeedometer.text = "0";
        }
    }

    private void Update()
    {
        // Atualiza as coordenadas do GPS
        if (Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
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

        map.UpdateMap(latitude, longitude);
    }

    private float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        // Calcula a distância em metros entre duas coordenadas usando a fórmula do haversine
        const float EarthRadius = 6371f; // Raio médio da Terra em quilômetros

        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float a = Mathf.Sin(dLat / 2f) * Mathf.Sin(dLat / 2f) +
                  Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                  Mathf.Sin(dLon / 2f) * Mathf.Sin(dLon / 2f);

        float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));

        float distance = EarthRadius * c * 1000f; // Multiplica por 1000 para converter para metros

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
}
