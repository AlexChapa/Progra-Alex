using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    [Header("PLAYFAB SETTINGS")]
    [SerializeField] private string titleID = "8D1C4";
    //[SerializeField]
    private string secretKey = "JJHTJQR6W15WGOIBNYCSIJBPDTBQO9N1SZQBC4RD5O9D49JWPI";

    [Header("Create Account Inputs")]
    [SerializeField] private TMP_InputField newUsernameInput;
    [SerializeField] private TMP_InputField setPasswordInput;

    [Header("Log In Inputs")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private UnityEvent onLogin;

    [Header("User Info")]
    [SerializeField] private TMP_Text userDisplayNameText;
    [SerializeField] private Image userProfilePicture;

    [Header("Leaderboard")]
    [SerializeField] private GameObject leadboardPrefab; 
    [SerializeField] private Transform leadboardParent; 


    private Texture2D avatarTexture;
    private Sprite avatarSprite;
    private float avatarWidth = 100;
    private float avatarHeight = 100;

    private string userDisplayName;



    private void Start()
    {
        //if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        //{
        //    PlayFabSettings.TitleId = titleID;
        //    PlayFabSettings.DeveloperSecretKey = secretKey;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        Time.timeScale = 0; // Pausa el tiempo del juego al inicio

        PlayFabSettings.TitleId = titleID;
        PlayFabSettings.DeveloperSecretKey = secretKey;
        Cursor.lockState = CursorLockMode.None;

    }


    public void RegisterUser() // Registra un nuevo usuario en PlayFab con el nombre de usuario y la contraseña
    {
        if (string.IsNullOrEmpty(newUsernameInput.text) || string.IsNullOrEmpty(setPasswordInput.text))
        {
            Debug.LogWarning("Alguno de los campos esta vacion");
            return;
        }

        var request = new RegisterPlayFabUserRequest()
        {
            DisplayName = newUsernameInput.text,
            Username = newUsernameInput.text,
            Password = setPasswordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, PlayfabErrorMessage);
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result) // Se llama cuando el registro es exitoso
    {
        Debug.Log("USUARIO REGISTRADO CORRECTAMENTE");
    }
   
   
    public void LogInUser() // Inicia sesión en PlayFab con el nombre de usuario y la contraseña
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.LogWarning("ALGUNO DE LOS CAMPOS ESTA VACIO");
            return;
        }

        var request = new LoginWithPlayFabRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
        };

        Cursor.lockState = CursorLockMode.None;
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucces, PlayfabErrorMessage);
    }

    private void OnLoginSucces(LoginResult result) // Se llama cuando el inicio de sesión es exitoso
    {
        Debug.Log("SESION INICIADA CORRECTAMENTE");
        onLogin?.Invoke();
        GetPlayerProfile(); // Obtiene el perfil del jugador después de iniciar sesión
        Time.timeScale = 1; 
        Cursor.lockState = CursorLockMode.Locked; 
    }
   

    public void GetPlayerProfile() // Obtiene el perfil del jugador después de iniciar sesión
    {
        var request = new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowAvatarUrl = true,
                ShowDisplayName = true
            }
        };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetProfileInfoSucces, PlayfabErrorMessage);
    }

    private IEnumerator ShowAvatar(string avatarUrl, Image avatarImage) // Descarga el avatar del jugador y lo muestra en la pantalla de inicio de sesión
    {
        Debug.Log("Iniciando corrutina ShowAvatar para URL: " + avatarUrl); 

        // Verifica si la URL es válida
        if (string.IsNullOrEmpty(avatarUrl))
        {
            Debug.LogWarning("URL de avatar no válida: " + avatarUrl);
            yield break;
        }

        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(avatarUrl);
        webRequest.timeout = 4; // Aumenta el tiempo de espera

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            avatarTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f));

            if (avatarImage != null)
            {
                avatarImage.sprite = avatarSprite;
                avatarImage.rectTransform.sizeDelta = new Vector2(avatarWidth, avatarHeight); 
                Debug.Log("Avatar obtenido correctamente de la API.");
            }
            else
            {
                Debug.LogWarning("Image no asignado.");
            }
        }
    }

    private void OnGetProfileInfoSucces(GetPlayerProfileResult result) 
    {
        userDisplayName = result.PlayerProfile.DisplayName;
        userDisplayNameText.text = userDisplayName;

        string avatarUrl = result.PlayerProfile.AvatarUrl;
        StartCoroutine(ShowAvatar(avatarUrl, userProfilePicture));
    }
   

    public void UpdateLeaderBoard(int value) 
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = "Highscore",
                    Value = value
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderUpdateSuccess, PlayfabErrorMessage); 
    }
    private void OnLeaderUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Se actualizo el leaderboard correctamente");
    }
    public void RequestLeaderboard() 
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "Highscore",
            StartPosition = 0,
            MaxResultsCount = 10,
            ProfileConstraints = new PlayerProfileViewConstraints() 
            {
                ShowDisplayName = true,
                ShowAvatarUrl = true

            }
        };
        PlayFabClientAPI.GetLeaderboard(request, DisplayLeaderboard, PlayfabErrorMessage); 
    }
    private void DisplayLeaderboard(GetLeaderboardResult result) 
    {
        foreach (Transform childOfParent in leadboardParent) 
        {
            Destroy(childOfParent.gameObject);
        }

        for (int i = 0; i < result.Leaderboard.Count; i++) 
        {
            var score = result.Leaderboard[i]; 
            GameObject leadboardInstance = Instantiate(leadboardPrefab, leadboardParent); 
            leadboardInstance.transform.SetParent(leadboardParent, false);


            RectTransform rectTransform = leadboardInstance.GetComponent<RectTransform>(); //posición de cada instancia para separación vertical
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -i * 80f); 

            TextMeshProUGUI[] texts = leadboardInstance.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = score.DisplayName; 
            texts[1].text = score.StatValue.ToString(); 

            Transform imageChild = leadboardInstance.transform.GetChild(2); 
            Image avatarImage = imageChild.GetComponent<Image>();
            if (avatarImage != null)
            {
                string avatarUrl = score.Profile.AvatarUrl;
                StartCoroutine(ShowAvatar(avatarUrl, avatarImage));
            }

            Debug.Log(string.Format("Nombre: {0} | Highscore: {1}", score.DisplayName, score.StatValue));
        }
    }

    private void PlayfabErrorMessage(PlayFabError error)
    {
        Debug.LogWarning(error.ErrorMessage);
    }
}
