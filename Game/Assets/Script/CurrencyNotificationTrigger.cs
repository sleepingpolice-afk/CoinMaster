using UnityEngine;

public class CurrencyNotificationTrigger : MonoBehaviour
{
    private void Start()
    {
        NotificationManager manager = FindFirstObjectByType<NotificationManager>();

        manager.RegisterNotification(new ConditionalNotification(
            "Congratulations!",
            "You have collected 500 coins! Keep it up!",
            () => CounterManagerInstanceExists() && CounterManager.Instance.data.coins > 500,
            () => Debug.Log("Player acknowledged 500 coins milestone.")
        ));

        manager.RegisterNotification(new ConditionalNotification(
            "Congratulations!",
            "You have collected 100 coins! Keep it up!",
            () => CounterManagerInstanceExists() && CounterManager.Instance.data.coins > 100,
            () => Debug.Log("Player acknowledged 100 coins milestone.")
        ));
    }

    private bool CounterManagerInstanceExists()
    {
        return CounterManager.Instance != null && CounterManager.Instance.data != null;
    }
}
