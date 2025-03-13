using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    [Header("PLAYFAB SETTINGS")]
    [SerializeField] private string titleID = "8D1C4";
    [SerializeField]
    private string secretKey = "EOKBSMXK4PYDD5ENI6DRR1KB9K48OTTZJ1WHORQ367ZY9EYZDG";

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


    private string userDisplayName;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        {
            PlayFabSettings.TitleId = titleID;
            PlayFabSettings.DeveloperSecretKey = secretKey;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RegisterUser()
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

    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        Debug.Log("USUARIO REGISTRADO CORRECTAMENTE");
    }

    public void LogInUser()
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

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucces, PlayfabErrorMessage);
    }

    private void OnLoginSucces(LoginResult result)
    {
        Debug.Log("SESION INICIADA CORRECTAMENTE");
        onLogin?.Invoke();
        GetPlayerProfile(); // Obtiene el perfil del jugador después de iniciar sesión
    }

    public void GetPlayerProfile()
    {
        var request = new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowAvatarUrl = true,
                ShowDisplayName = true
            }
        };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetDisplayNameSucces, PlayfabErrorMessage);
    }

    private IEnumerator ShowAvatar(string avatarUrl)
    {
        // Verifica si la URL es válida
        if (string.IsNullOrEmpty(avatarUrl) || !Uri.IsWellFormedUriString(avatarUrl, UriKind.Absolute))
        {
            Debug.LogWarning("URL de avatar no válida: " + avatarUrl);
            yield break;
        }

        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(avatarUrl);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D avatarTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            Sprite avatarImage = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f));
            userProfilePicture.sprite = avatarImage;
            userProfilePicture.preserveAspect = true;
            userProfilePicture.rectTransform.sizeDelta = new Vector2(100, 100); // Establece el tamaño de la imagen a 100x100 píxeles
            Debug.Log("Avatar obtenido correctamente de la API.");
        }
        else
        {
            Debug.Log("Error al obtener el avatar: " + webRequest.error);
        }
    }

    private void OnGetDisplayNameSucces(GetPlayerProfileResult result)
    {
        userDisplayName = result.PlayerProfile.DisplayName;
        userDisplayNameText.text = userDisplayName;

        string avatarUrl = result.PlayerProfile.AvatarUrl;
        Debug.Log("Avatar URL: " + avatarUrl); // Imprime la URL en la consola para depuración
        StartCoroutine(ShowAvatar(avatarUrl));
    }

    private void PlayfabErrorMessage(PlayFabError error)
    {
        Debug.LogWarning(error.ErrorMessage);
    }
}

