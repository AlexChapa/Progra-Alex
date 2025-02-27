using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


namespace AlexWeather
{

    public class WeatherApi : MonoBehaviour
    {
        [SerializeField] public WeatherData data; //Estructura de la data del clima
        [SerializeField] public Country[] countries = new Country[10]; //Paises
        [SerializeField] public int currentCountry = -1; // Índice del país actual

        [SerializeField] private VolumeProfile volumenProfile; //Perfil de volumen
        [SerializeField] private float bloomColorTransitionSpeed; //Velocidad de transicion de color
        [SerializeField] private float colorAdjustmentSpeed; //Velocidad de transicion de balance de blancos

        [SerializeField] private Transform ligth;
        [SerializeField] private TextMeshProUGUI countryUI;

        private Color actualColor; //Color actual
        private Color actualAdjustmentColor; //Color de ajuste actual

        private static readonly string apiKey = "7fe45acb4f5a69f83c45312aad97613a"; //API Key
        private string json; //JSON

        private void Start()
        {
            StartCoroutine(RetrieveWhwatherData()); //Obtiene la data del clima
        }

        private void Update()
        {
            countryUI.text = " Current Weather : " + data.name; //Muestra la zona horaria en la UI
        }

        IEnumerator RetrieveWhwatherData()
        {
            while (true)
            {
                UnityWebRequest request = new UnityWebRequest(CountryURL()); //Crea un objeto de tipo UnityWebRequest con la URL del país
                request.downloadHandler = new DownloadHandlerBuffer(); //Descarga la data

                yield return request.SendWebRequest(); //Espera a que se descargue la data

                if (request.result != UnityWebRequest.Result.Success) //Si hay un error
                {
                    Debug.Log(request.error); //Error
                }
                else
                {
                    Debug.Log(request.downloadHandler.text); //JSON
                    json = request.downloadHandler.text; // JSON
                    DecodeJson(); //Decodifica el JSON
                    yield return new WaitForSeconds(2); //Espera 2 segundos para que se cargue la data
                    actualColor = GetColorByTemp(); //Obtiene el color segun la temperatura
                    actualAdjustmentColor = GetAdjustmentsColorByTemp(); //Obtiene el color segun la temperatura
                    StartCoroutine(ColorAdjustmentTransition()); //Transicion de balance de blancos
                    StartCoroutine(BloomColorTransition()); //Transicion de color
                }
                yield return new WaitForSecondsRealtime(90); //Espera 5 segundos
            }
        }

        private IEnumerator BloomColorTransition() //Transicion de color
        {
            yield return new WaitUntil(() => TransitionColorBloom() == actualColor); //Espera a que la transicion de color termine
            Debug.Log("Color Cambiado Bloom");
        }
        private IEnumerator ColorAdjustmentTransition() // Transicion de ColorAdjustments
        {
            yield return new WaitUntil(() => ColorAdjustments() == actualAdjustmentColor); //Espera a que la transicion de balance de blancos termine
            Debug.Log("Color Adjustments Cambiado");
        }

        private Color TransitionColorBloom() //Transicion de color
        {
            volumenProfile.TryGet(out Bloom bloom); // Obtiene el bloom del perfil de volumen
            bloom.tint.value = Color.Lerp(bloom.tint.value, actualColor, bloomColorTransitionSpeed); //Transicion de color
            return bloom.tint.value; //Retorna el color
        }
        private Color ColorAdjustments() //Transicion de balance de blancos
        {
            volumenProfile.TryGet(out ColorAdjustments colorAdjustments); // Obtiene el balance de blancos del perfil de volumen
            colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, actualAdjustmentColor, colorAdjustmentSpeed); //Transicion de balance de blancos
            return colorAdjustments.colorFilter.value; //Retorna el color
        }

        private Color GetColorByTemp() //Obtiene el color segun la temperatura
        {
            switch (data.actualTemp)
            {
                case var color when data.actualTemp <= 8: //Si la temperatura es menor o igual a 8
                    {
                        actualColor = Color.cyan;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualColor;
                    }

                case var color when data.actualTemp > 8 && data.actualTemp < 24:
                    {
                        actualColor = Color.blue;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualColor;
                    }

                case var color when data.actualTemp > 24 && data.actualTemp < 45:
                    {
                        actualColor = Color.yellow;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualColor;
                    }

                case var color when data.actualTemp >= 45:
                    {
                        actualColor = Color.red;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualColor;
                    }

                default:
                    {
                        return actualColor;
                    }
            }
        }
        private Color GetAdjustmentsColorByTemp() //Obtiene el color segun la temperatura
        {
            switch (data.actualTemp)
            {
                case var color when data.actualTemp <= 8: //Si la temperatura es menor o igual a 8
                    {
                        actualAdjustmentColor = Color.blue;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp > 8 && data.actualTemp < 24:
                    {
                        actualAdjustmentColor = Color.cyan;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp > 24 && data.actualTemp < 45:
                    {
                        actualAdjustmentColor = Color.yellow;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp >= 45:
                    {
                        actualAdjustmentColor = Color.red;
                        StartCoroutine(LightMove()); //Mueve la luz
                        return actualAdjustmentColor;
                    }

                default:
                    {
                        return actualAdjustmentColor;
                    }
            }
        }

        public void DecodeJson()
        {
            var weatherJson = JSON.Parse(json);

            data.actualTemp = float.Parse(weatherJson["current"]["temp"].Value); //Temperatura actual
            data.name = weatherJson["timezone"].Value; //Zona horaria
            data.windSpeed = float.Parse(weatherJson["current"]["wind_speed"].Value); //Velocidad del viento
            data.humidity = float.Parse(weatherJson["current"]["humidity"].Value); //Humedad

            // Actualiza el nombre del país actual
            if (currentCountry >= 0 && currentCountry < countries.Length) //
            {
                countries[currentCountry].name = weatherJson["timezone"].Value;
            }
        }
        string CountryURL() //URL del pais
        {
            Country country = RandomCountry(); // Obtiene un país aleatorio

            string url = $"https://api.openweathermap.org/data/3.0/onecall?lat={country.latitude}&lon={country.longitude}&appid={apiKey}&lang=sp&units=metric";  // Construye la URL de la API usando la latitud y longitud del país aleatorio
            return url; // Retorna la URL
        }
        Country RandomCountry() // Obtiene un país aleatorio
        {
            currentCountry = Random.Range(0, countries.Length); // Genera un índice aleatorio
            return countries[currentCountry]; // Retorna el país en el índice aleatorio
        }
        IEnumerator LightMove()
        //Mueve la luz
        {
            while (true)
            {
                ligth.Rotate(new Vector3(50f, 0, 0), 5f); //Rota el sol
                yield return new WaitForSeconds(45f); //Espera 5 segundos
            }
        }

    }
}

