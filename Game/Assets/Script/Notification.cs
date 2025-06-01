using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public static Notification Instance { get; private set; }
    public GameObject panel;

    public GameObject panelInstance;
    public TMP_Text titleText;
    public TMP_Text messageText;
    public Button confirmButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (panel == null)
        {
            Debug.LogError("Notification panel prefab is not assigned in the inspector.");
            return;
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene!");
            return;
        }

        //panel jadiin prefab aja blok -wesley from the future

        panelInstance = Instantiate(panel, canvas.transform);


        titleText = panelInstance.transform.Find("NotificationMessage").GetComponent<TMP_Text>();
        messageText = panelInstance.transform.Find("Message").GetComponent<TMP_Text>();
        confirmButton = panelInstance.transform.Find("ConfirmationButton").GetComponent<Button>();

        confirmButton.onClick.AddListener(HideNotification);
        panelInstance.SetActive(false);
    }

    public void ShowNotification(string title, string message, System.Action onConfirm = null)
    {
        Debug.Log($"ShowNotification called with title: {title}, message: {message}");
        if (panelInstance == null)
        {
            Debug.LogWarning("Notification panel instance not found.");
            return;
        }

        panelInstance.SetActive(true);
        panelInstance.transform.SetAsLastSibling();

        titleText.text = title;
        messageText.text = message;

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(HideNotification);

        if (onConfirm != null)
        {
            confirmButton.onClick.AddListener(() => onConfirm.Invoke());
        }
    }

    private void HideNotification()
    {
        if (panelInstance != null)
        {
            panelInstance.SetActive(false);
        }
    }
}
