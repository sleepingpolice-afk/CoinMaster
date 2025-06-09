using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Login : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public Button submitbutton;
    public Button goToRegisterButton; // Added for navigating to Register scene

    private string loginurl = "http://localhost:8080/api/players/login";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        submitbutton.onClick.AddListener(() =>
        {
            StartCoroutine(LoginUserAndFetchData());
        });

        if (goToRegisterButton != null) // Added null check
        {
            goToRegisterButton.onClick.AddListener(GoToRegisterScene);
        }
        else
        {
            Debug.LogError("GoToRegisterButton is not assigned in Login script.");
        }

        if (DataManager.Instance.data != null)
        {
            DataManager.Instance.data.hasBoughtWeapon = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private IEnumerator LoginUserAndFetchData()
    {
        PlayerLoginRequest loginPayload = new PlayerLoginRequest
        {
            email = email.text,
            password = password.text
        };

        string jsonData = JsonUtility.ToJson(loginPayload);
        UnityWebRequest request = new UnityWebRequest(loginurl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Raw JSON response: " + request.downloadHandler.text);
            PlayerDataResponse resp = JsonUtility.FromJson<PlayerDataResponse>(request.downloadHandler.text);
            
            if (DataManager.Instance != null && DataManager.Instance.data != null)
            {
                DataManager.Instance.data.coins = resp.currency;
                DataManager.Instance.data.playerId = resp.playerId;
                DataManager.Instance.data.username = resp.username;
                DataManager.Instance.data.email = resp.email;

                if (resp.upgradeLevels != null)
                {
                    DataManager.Instance.data.clickUpgradeLevel = resp.upgradeLevels.Click;
                    DataManager.Instance.data.passiveIncomeLevel = resp.upgradeLevels.Production;
                }
                else
                {
                    Debug.LogWarning("Parsed upgradeLevels is null. Check JSON response and PlayerDataResponse structure.");
                }
                
                DataManager.Instance.data.StartPassiveIncome();

                Debug.Log($"Player ID: {DataManager.Instance.data.playerId}, Username: {DataManager.Instance.data.username}, Email: {DataManager.Instance.data.email}, Currency: {DataManager.Instance.data.coins}, Click Level: {DataManager.Instance.data.clickUpgradeLevel}, Production Level: {DataManager.Instance.data.passiveIncomeLevel}, Calculated Passive Rate: {DataManager.Instance.data.passiveIncomeRate}");
            }
            else
            {
                Debug.LogError("DataManager or DataManager.data is null. Cannot set player data.");
            }

            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.LogError("Login failed: " + request.responseCode + " " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }

    public void GoToRegisterScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    [System.Serializable]
    public class PlayerLoginRequest 
    {
        public string email;
        public string password;
    }

    [System.Serializable]
    public class UpgradeLevelsData
    {
        public int Click;
        public int Production;
    }

    [System.Serializable]
    public class PlayerDataResponse
    {
        public string playerId;
        public long currency;
        public string createdAt;
        public string username; 
        public string email;
        public UpgradeLevelsData upgradeLevels;
    }
}