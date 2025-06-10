using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement; // Added for scene management

public class RegisterLogin : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField email;
    public Button registerButton;
    public Button goToLoginButton; // Added for navigating to Login scene

    private string registerUrl = "https://coinmaster.osc-fr1.scalingo.io/api/players";

    void Start()
    {
        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(Register());
        });

        if (goToLoginButton != null) // Added null check
        {
            goToLoginButton.onClick.AddListener(GoToLoginScene);
        }
        else
        {
            Debug.LogError("GoToLoginButton is not assigned in RegisterLogin script.");
        }
    }

    IEnumerator Register()
    {
        Player player = new Player
        {
            username = username.text,
            password = password.text,
            email = email.text
        };

        string jsonData = JsonUtility.ToJson(player);
        UnityWebRequest request = new UnityWebRequest(registerUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Registration successful: " + request.downloadHandler.text);
            SceneManager.LoadScene("LOGIN"); 
        }
        else
        {
            Debug.LogError("Registration failed: " + request.responseCode + " " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
    public void GoToLoginScene()
    {
        SceneManager.LoadScene("LOGIN");
    }
}

[System.Serializable]
public class Player
{
    public string username;
    public string password;
    public string email;
}
