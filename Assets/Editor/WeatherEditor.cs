
using AlexWeather;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WeatherApi))]
public class WeatherEditor : Editor
{
    private WeatherApi weatherApi;
    private bool[] foldouts; 

    private void OnEnable()
    {
        weatherApi = (WeatherApi)target;
        foldouts = new bool[weatherApi.countries.Length]; 
    }

    public override void OnInspectorGUI()
    {
      
        WeatherApi weatherApi = (WeatherApi)target;

        SerializedProperty countries = serializedObject.FindProperty("countries"); 

        for (int i = 0; i < countries.arraySize; i++) 
        {
            SerializedProperty country = countries.GetArrayElementAtIndex(i); 

            EditorGUILayout.PropertyField(country.FindPropertyRelative("name"), new GUIContent("Name")); 
            EditorGUILayout.PropertyField(country.FindPropertyRelative("latitude"), new GUIContent("Latitude")); 
            EditorGUILayout.PropertyField(country.FindPropertyRelative("longitude"), new GUIContent("Longitude")); 


            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], "Weather Data"); 

            if (foldouts[i])
            {
                
                if (i == weatherApi.currentCountry) 
                {
                    EditorGUILayout.LabelField("Time Zone", weatherApi.data.name); 
                    EditorGUILayout.LabelField("Actual Temperature", weatherApi.data.actualTemp.ToString()); 
                    EditorGUILayout.LabelField("Wind Speed", weatherApi.data.windSpeed.ToString()); 
                    EditorGUILayout.LabelField("Humidity", weatherApi.data.humidity.ToString()); 
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
