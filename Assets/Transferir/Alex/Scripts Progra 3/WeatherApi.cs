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
        [SerializeField] public WeatherData data;
        [SerializeField] public Country[] countries = new Country[10];
        [SerializeField] public int currentCountry = -1;

        [SerializeField] private VolumeProfile volumenProfile;
        [SerializeField] private float bloomColorTransitionSpeed;
        [SerializeField] private float colorAdjustmentSpeed;

        [SerializeField] private Transform ligth;
        [SerializeField] private TextMeshProUGUI countryUI;

        private Color actualColor;
        private Color actualAdjustmentColor;

        private static readonly string apiKey = "7fe45acb4f5a69f83c45312aad97613a";
        private string json;

        private void Start()
        {
            StartCoroutine(RetrieveWhwatherData());
        }

        private void Update()
        {
            countryUI.text = " Current Weather : " + data.name;
        }

        IEnumerator RetrieveWhwatherData()
        {
            while (true)
            {
                UnityWebRequest request = new UnityWebRequest(CountryURL());
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                    json = request.downloadHandler.text;
                    DecodeJson();
                    yield return new WaitForSeconds(2);
                    actualColor = GetColorByTemp();
                    actualAdjustmentColor = GetAdjustmentsColorByTemp();
                    StartCoroutine(ColorAdjustmentTransition());
                    StartCoroutine(BloomColorTransition());
                }
                yield return new WaitForSecondsRealtime(90);
            }
        }

        private IEnumerator BloomColorTransition()
        {
            yield return new WaitUntil(() => TransitionColorBloom() == actualColor);
            Debug.Log("Color Cambiado Bloom");
        }
        private IEnumerator ColorAdjustmentTransition()
        {
            yield return new WaitUntil(() => ColorAdjustments() == actualAdjustmentColor);
            Debug.Log("Color Adjustments Cambiado");
        }

        private Color TransitionColorBloom()
        {
            volumenProfile.TryGet(out Bloom bloom); 
            bloom.tint.value = Color.Lerp(bloom.tint.value, actualColor, bloomColorTransitionSpeed);
            return bloom.tint.value;
        }
        private Color ColorAdjustments() 
        {
            volumenProfile.TryGet(out ColorAdjustments colorAdjustments);
            colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, actualAdjustmentColor, colorAdjustmentSpeed);
            return colorAdjustments.colorFilter.value;
        }

        private Color GetColorByTemp()
        {
            switch (data.actualTemp)
            {
                case var color when data.actualTemp <= 8:
                    {
                        actualColor = Color.cyan;
                        StartCoroutine(LightMove());
                        return actualColor;
                    }

                case var color when data.actualTemp > 8 && data.actualTemp < 24:
                    {
                        actualColor = Color.blue;
                        StartCoroutine(LightMove());
                        return actualColor;
                    }

                case var color when data.actualTemp > 24 && data.actualTemp < 45:
                    {
                        actualColor = Color.yellow;
                        StartCoroutine(LightMove());
                        return actualColor;
                    }

                case var color when data.actualTemp >= 45:
                    {
                        actualColor = Color.red;
                        StartCoroutine(LightMove());
                        return actualColor;
                    }

                default:
                    {
                        return actualColor;
                    }
            }
        }
        private Color GetAdjustmentsColorByTemp() 
        {
            switch (data.actualTemp)
            {
                case var color when data.actualTemp <= 8: 
                    {
                        actualAdjustmentColor = Color.blue;
                        StartCoroutine(LightMove());
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp > 8 && data.actualTemp < 24:
                    {
                        actualAdjustmentColor = Color.cyan;
                        StartCoroutine(LightMove());
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp > 24 && data.actualTemp < 45:
                    {
                        actualAdjustmentColor = Color.yellow;
                        StartCoroutine(LightMove());
                        return actualAdjustmentColor;
                    }

                case var color when data.actualTemp >= 45:
                    {
                        actualAdjustmentColor = Color.red;
                        StartCoroutine(LightMove());
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

            data.actualTemp = float.Parse(weatherJson["current"]["temp"].Value);
            data.name = weatherJson["timezone"].Value;
            data.windSpeed = float.Parse(weatherJson["current"]["wind_speed"].Value); 
            data.humidity = float.Parse(weatherJson["current"]["humidity"].Value); 

          
            if (currentCountry >= 0 && currentCountry < countries.Length)
            {
                countries[currentCountry].name = weatherJson["timezone"].Value;
            }
        }
        string CountryURL()
        {
            Country country = RandomCountry();

            string url = $"https://api.openweathermap.org/data/3.0/onecall?lat={country.latitude}&lon={country.longitude}&appid={apiKey}&lang=sp&units=metric";
            return url; 
        }
        Country RandomCountry() 
        {
            currentCountry = Random.Range(0, countries.Length); 
            return countries[currentCountry]; 
        }
        IEnumerator LightMove()
        
        {
            while (true)
            {
                ligth.Rotate(new Vector3(50f, 0, 0), 5f); 
                yield return new WaitForSeconds(45f); 
            }
        }

    }
}

