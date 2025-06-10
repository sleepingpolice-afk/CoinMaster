using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    private string saveUrl = "https://coinmaster.osc-fr1.scalingo.io/api/player-progress/save";
    public TextMeshProUGUI feedbackText;

    public void OnSaveButtonPressed()
    {
        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            if (string.IsNullOrEmpty(DataManager.Instance.data.playerId))
            {
                Debug.LogError("SaveGameManager: PlayerID is missing. Cannot save progress. Ensure player is logged in.");
                if (feedbackText != null) feedbackText.text = "Error: Player not logged in!";
                return;
            }
            StartCoroutine(SavePlayerProgress());
        }
        else
        {
            Debug.LogError("SaveGameManager: DataManager or DataManager.data is null. Cannot save progress.");
            if (feedbackText != null) feedbackText.text = "Error: Player data not found!";
        }
    }

    public void LogOut()
    {
        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            DataManager.Instance.data.playerId = null;
            DataManager.Instance.data.username = null;
        }
        else
        {
            Debug.LogWarning("DataManager instance or data not found. Could not clear session data.");
        }
        
        SceneManager.LoadScene("LOGIN");
    }

    private IEnumerator SavePlayerProgress()
    {
        Debug.Log($"SaveGameManager: Preparing to save - PlayerID: {DataManager.Instance.data.playerId}, Currency: {DataManager.Instance.data.coins}, Click Level: {DataManager.Instance.data.clickUpgradeLevel}, Production Level: {DataManager.Instance.data.passiveIncomeLevel}");

        // Struktur payload yg baru, yg lama gak work anjing i give up so i ask gpt for this stuff
        UpgradeLevelsPayload upgradeLevelsData = new UpgradeLevelsPayload
        {
            Click = DataManager.Instance.data.clickUpgradeLevel,
            Production = DataManager.Instance.data.passiveIncomeLevel
            // Add other fixed upgrade names here if you expand in the future
        };

        SaveDataPayload payload = new SaveDataPayload
        {
            playerId = DataManager.Instance.data.playerId,
            currency = (long)DataManager.Instance.data.coins,
            upgradeLevels = upgradeLevelsData // Assign the structured object here
        };

        string jsonData = JsonUtility.ToJson(payload);
        Debug.Log("SaveGameManager: Sending save data: " + jsonData);

        UnityWebRequest request = new UnityWebRequest(saveUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        if (feedbackText != null) feedbackText.text = "Saving...";

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("SaveGameManager: Game saved successfully! Response: " + request.downloadHandler.text);
            if (feedbackText != null) feedbackText.text = "Game Saved!";
            StartCoroutine(ClearFeedbackAfterDelay(3f));
        }
        else
        {
            Debug.LogError("SaveGameManager: Failed to save game. Error: " + request.responseCode + " " + request.error);
            Debug.LogError("SaveGameManager: Response: " + request.downloadHandler.text);
            if (feedbackText != null) feedbackText.text = "Save Failed: " + request.error;
            StartCoroutine(ClearFeedbackAfterDelay(5f));
        }
    }

    // New helper class for upgrade levels, similar to what's used in Login.cs for response
    [System.Serializable]
    public class UpgradeLevelsPayload
    {
        public int Click;
        public int Production;
    }

    [System.Serializable]
    public class SaveDataPayload
    {
        public string playerId;
        public long currency;
        public UpgradeLevelsPayload upgradeLevels;
    }

    private IEnumerator ClearFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (feedbackText != null) feedbackText.text = "";
    }
}
