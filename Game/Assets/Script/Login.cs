using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public Button submitbutton;

    private string loginurl = "http://localhost:8080/api/players/login";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        submitbutton.onClick.AddListener(() =>
        {
            StartCoroutine(LoginUser());
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private IEnumerator LoginUser()
    {
        Player player = new Player
        {
            email = email.text,
            password = password.text
        };

        string jsonData = JsonUtility.ToJson(player);
        UnityWebRequest request = new UnityWebRequest(loginurl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ Login successful: " + request.downloadHandler.text);
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.LogError("❌ Login failed: " + request.responseCode + " " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
}

[System.Serializable]
public class Loginuser
{
    public string email;
    public string password;
}