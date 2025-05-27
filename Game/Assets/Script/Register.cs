using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class RegisterLogin : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField email;
    public Button registerButton;

    private string registerUrl = "http://localhost:8080/api/players";

    void Start()
    {
        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(Register());
        });
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
            Debug.Log("✅ Registration successful: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("❌ Registration failed: " + request.responseCode + " " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
}

[System.Serializable]
public class Player
{
    public string username;
    public string password;
    public string email;
}
