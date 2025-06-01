using System;
using UnityEngine;

[Serializable]
public class Event
{
    public string unlockKey;
    public float unlockTime;
    public bool isUnlocked = false;
    public string notificationTitle = "tes aja";
    public string notificationMessage = "";
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [Tooltip("Configure unlock events with key and unlock time in seconds")]
    public Event[] unlocks;

    private float elapsedTime = 0f;

    public event Action<string> OnUnlocked;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        foreach (var unlock in unlocks)
        {
            if (!unlock.isUnlocked && elapsedTime >= unlock.unlockTime)
            {
                unlock.isUnlocked = true;
                OnUnlocked?.Invoke(unlock.unlockKey);
                Debug.Log($"{unlock.unlockKey} at {elapsedTime:F2}s");

                if (!string.IsNullOrEmpty(unlock.notificationTitle) || !string.IsNullOrEmpty(unlock.notificationMessage))
                {
                    Notification.Instance.ShowNotification(
                        unlock.notificationTitle,
                        unlock.notificationMessage,
                        null
                    );
                }
            }
        }
    }

    public bool IsUnlocked(string key)
    {
        foreach (var unlock in unlocks)
        {
            if (unlock.unlockKey == key)
                return unlock.isUnlocked;
        }
        return false;
    }
}
