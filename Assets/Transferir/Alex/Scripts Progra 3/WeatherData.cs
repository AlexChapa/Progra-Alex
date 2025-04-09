using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    [SerializeField] public string name;
    [SerializeField] public float actualTemp;
    [SerializeField] public float windSpeed;
    [SerializeField] public float humidity;
}

