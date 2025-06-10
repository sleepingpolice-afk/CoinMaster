using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BuyWeapon : MonoBehaviour
{
    public TMP_Text WarningText;
    public TMP_Text buttonText;

    public GameObject menuPanel;
    public GameObject playerSelectionPanel;
    public GameObject playerButtonPrefab;
    public GameObject cancelButtonPrefab;
    public Transform[] playerButtonAnchors = new Transform[6];

    private string randomPlayersUrl = "https://coinmaster.osc-fr1.scalingo.io/api/players/random";
    private string playerActionsBaseUrl = "https://coinmaster.osc-fr1.scalingo.io/api/players";
    private double pendingWeaponCost;

    // Helper class to parse player data from JSON
    [System.Serializable]
    public class PlayerInfo
    {
        public string playerId;
        public string username;
        public double currency;
    }

    // Helper class to parse the list of players from JSON
    [System.Serializable]
    public class PlayerList
    {
        public List<PlayerInfo> players;
    }


    void Start()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager is NULL in BuyWeapon");
            return;
        }
        else if (DataManager.Instance.data == null)
        {
            Debug.LogError("DataManager.data is NULL in BuyWeapon");
            return;
        }

        DataManager.Instance.data.OnDataChanged += UpdateUI;
        UpdateUI();

        if (playerSelectionPanel != null)
        {
            playerSelectionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("PlayerSelectionPanel is not assigned in BuyWeapon.");
        }
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("MenuPanel is not assigned in BuyWeapon.");
        }
         if (playerButtonPrefab == null)
        {
            Debug.LogError("PlayerButtonPrefab is not assigned in BuyWeapon.");
        }
        if (playerButtonAnchors == null || playerButtonAnchors.Length != 6)
        {
            Debug.LogError("PlayerButtonAnchors array is not assigned or does not contain 6 elements in BuyWeapon.");
            return;
        }
        for(int i = 0; i < playerButtonAnchors.Length; i++)
        {
            if (playerButtonAnchors[i] == null)
            {
                Debug.LogError($"PlayerButtonAnchors element {i} is not assigned in BuyWeapon.");
                return;
            }
        }
        if (cancelButtonPrefab == null)
        {
            Debug.LogError("CancelButtonPrefab is not assigned in BuyWeapon.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightWindows))
        {
            BuyWeapons();
        }
    }
    void OnDestroy()
    {
        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            DataManager.Instance.data.OnDataChanged -= UpdateUI;
        }
    }

    public void BuyWeapons()
    {
        if (DataManager.Instance == null || DataManager.Instance.data == null)
        {
            StartCoroutine(ShowWarning("Data not loaded. Cannot buy weapon.", 2f));
            return;
        }
        
        if (string.IsNullOrEmpty(DataManager.Instance.data.playerId))
        {
            StartCoroutine(ShowWarning("Player ID not found. Cannot fetch players.", 2f));
            return;
        }

        DataManager.Instance.data.hasBoughtWeapon = true;

        if (DataManager.Instance.data.coins >= cost())
        {
            pendingWeaponCost = cost();

            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
            }
            StartCoroutine(FetchAndDisplayPlayers());
        }
        else
        {
            StartCoroutine(ShowWarning("Not enough coins to buy this weapon upgrade!", 2f));
        }
    }

    private double cost()
    {
        if (DataManager.Instance == null || DataManager.Instance.data == null) return double.MaxValue;
        return DataManager.Instance.data.weaponCost;
    }

    public void UpdateUI()
    {
        if (buttonText != null)
        {
            buttonText.text = "Buy Weapon\n" +
                                "Cost: " + NumberFormatter.FormatNumber(cost()) + "\n";
        }
    }

    private IEnumerator FetchAndDisplayPlayers()
    {
        if (playerSelectionPanel == null || playerButtonPrefab == null || cancelButtonPrefab == null || playerButtonAnchors == null || playerButtonAnchors.Length != 6)
        {
            Debug.LogError("UI elements (PlayerSelectionPanel, PlayerButtonPrefab, CancelButtonPrefab or PlayerButtonAnchors) for player selection are not assigned/configured correctly in BuyWeapon.");
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
            }
            if (playerSelectionPanel != null)
            {
                playerSelectionPanel.SetActive(false);
            }
            yield break;
        }

        string excludePlayerId = DataManager.Instance.data.playerId;
        string urlWithQuery = $"{randomPlayersUrl}/{excludePlayerId}";

        UnityWebRequest request = UnityWebRequest.Get(urlWithQuery);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Random players fetched: " + request.downloadHandler.text);
            string jsonResponse = "{\"players\":" + request.downloadHandler.text + "}";
            PlayerList playerListData = JsonUtility.FromJson<PlayerList>(jsonResponse);

            ClearPlayerList();

            if (playerListData != null && playerListData.players != null)
            {
                for (int i = 0; i < playerListData.players.Count && i < playerButtonAnchors.Length - 1; i++)
                {
                    if (playerButtonAnchors[i] == null)
                    {
                        Debug.LogWarning($"Player anchor point at index {i} is null. Skipping button placement.");
                        continue;
                    }

                    PlayerInfo player = playerListData.players[i];
                    GameObject buttonGO = Instantiate(playerButtonPrefab, playerButtonAnchors[i]);
                    buttonGO.transform.localPosition = Vector3.zero;

                    TMP_Text[] textComponents = buttonGO.GetComponentsInChildren<TMP_Text>();
                    TMP_Text usernameTextComponent = null;
                    TMP_Text currencyTextComponent = null;

                    foreach (TMP_Text textComp in textComponents)
                    {
                        if (textComp.gameObject.name == "PlayerButtonPrefabText")
                        {
                            usernameTextComponent = textComp;
                        }
                        else if (textComp.gameObject.name == "Currency")
                        {
                            currencyTextComponent = textComp;
                        }
                    }
                    
                    if (usernameTextComponent == null && textComponents.Length > 0) {
                        usernameTextComponent = textComponents[0];
                    }
                    if (currencyTextComponent == null && textComponents.Length > 1) {
                         currencyTextComponent = textComponents[1];
                    }


                    Button buttonComponent = buttonGO.GetComponent<Button>();

                    if (usernameTextComponent != null)
                    {
                        usernameTextComponent.text = player.username;
                    }
                    else
                    {
                        Debug.LogError("PlayerButtonPrefab is missing a TMP_Text component for username or it could not be found.");
                    }

                    if (currencyTextComponent != null)
                    {
                        currencyTextComponent.text = "Currency: " + NumberFormatter.FormatNumber(player.currency);
                    }
                    else
                    {
                        Debug.LogWarning("PlayerButtonPrefab is missing a TMP_Text component for currency or it could not be found. Currency will not be displayed.");
                    }

                    if (buttonComponent != null)
                    {
                        string capturedId = player.playerId;
                        string capturedUsername = player.username;
                        buttonComponent.onClick.AddListener(() => OnPlayerSelected(capturedId, capturedUsername));
                    }
                    else
                    {
                        Debug.LogError("PlayerButtonPrefab is missing a Button component.");
                    }
                }

                Transform cancelAnchor = playerButtonAnchors[5];
                if (cancelAnchor != null)
                {
                    GameObject cancelGO = Instantiate(cancelButtonPrefab, cancelAnchor);
                    cancelGO.transform.localPosition = Vector3.zero;
                    Button cancelComponent = cancelGO.GetComponent<Button>();
                    if (cancelComponent != null)
                    {
                        cancelComponent.onClick.AddListener(CancelPlayerSelection);
                    }
                    else
                    {
                        Debug.LogError("CancelButtonPrefab is missing a Button component.");
                    }
                }
                else
                {
                    Debug.LogError("The 6th anchor point (for Cancel button) is not assigned.");
                }

                playerSelectionPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("Failed to parse player list or no players returned.");
                StartCoroutine(ShowWarning("Could not fetch player list.", 2f));
            }
        }
        else
        {
            Debug.LogError($"Error fetching random players: {request.responseCode} - {request.error}");
            Debug.LogError($"Response: {request.downloadHandler.text}");
            StartCoroutine(ShowWarning("Error fetching players. See console.", 2f));
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
            }
            if (playerSelectionPanel != null)
            {
                playerSelectionPanel.SetActive(false);
            }
        }
    }

    void ClearPlayerList()
    {
        if (playerButtonAnchors == null) return;

        foreach (Transform anchor in playerButtonAnchors)
        {
            if (anchor != null)
            {
                foreach (Transform child in anchor)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    void OnPlayerSelected(string playerId, string username)
    {
        Debug.Log($"Player selected: {username} (ID: {playerId})");

        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            DataManager.Instance.data.coins -= pendingWeaponCost;
            Debug.Log($"Deducted {pendingWeaponCost} coins. Remaining coins: {DataManager.Instance.data.coins}");
        }
        else
        {
            Debug.LogError("DataManager or its data is null. Cannot deduct coins.");
            StartCoroutine(ShowWarning("Error processing purchase. Please try again.", 3f));
            if (playerSelectionPanel != null) playerSelectionPanel.SetActive(false);
            if (menuPanel != null) menuPanel.SetActive(true);
            return;
        }

        StartCoroutine(DecreaseAndFinalizeSelection(playerId, username));
    }

    private IEnumerator DecreaseAndFinalizeSelection(string playerId, string username)
    {
        StartCoroutine(ShowWarning($"Attacking {username}...", 2f));

        string attackerPlayerId = DataManager.Instance.data.playerId; //ini id user
        string url = $"{playerActionsBaseUrl}/{attackerPlayerId}/attack/{playerId}"; // Use renamed variable
        UnityWebRequest request = UnityWebRequest.Post(url, new WWWForm()); 
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Successfully decreased upgrades for player {username} (ID: {playerId}). Response: {request.downloadHandler.text}");
            yield return StartCoroutine(ShowWarning($"Successfully attacked {username}! They will remember it....", 2f));
        }
        else
        {
            Debug.LogError($"Error decreasing upgrades for {username} (ID: {playerId}): {request.responseCode} - {request.error}");
            Debug.LogError($"Response: {request.downloadHandler.text}");
            yield return StartCoroutine(ShowWarning($"Failed to attack {username}. Error: {request.error}", 3f));
        }

        if (playerSelectionPanel != null)
        {
            playerSelectionPanel.SetActive(false);
        }
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
    }

    public void CancelPlayerSelection()
    {
        Debug.Log("Player selection cancelled.");
        if (playerSelectionPanel != null)
        {
            playerSelectionPanel.SetActive(false);
        }
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
    }

    private IEnumerator ShowWarning(string message, float duration)
    {
        if (WarningText != null)
        {
            WarningText.text = message;
            WarningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            WarningText.text = "";
            WarningText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"ShowWarning: WarningText is null. Message: {message}");
        }
    }
}
