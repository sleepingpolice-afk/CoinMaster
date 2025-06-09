using UnityEngine;

public class CurrencyNotificationTrigger : MonoBehaviour
{
    private void Start()
    {
        NotificationManager manager = FindFirstObjectByType<NotificationManager>();

        manager.RegisterNotification(new ConditionalNotification(
            "Congratulations!",
            "You Have bought a weapon! Select a player to attack with this weapon. Click Cancel to cancel purchase.",
            () => CounterManagerInstanceExists() && DataManager.Instance.data.hasBoughtWeapon == true,
            () => Debug.Log("Player acknowledged weapon purchase.")
        ));
    }

    private bool CounterManagerInstanceExists()
    {
        return CounterManager.Instance != null && DataManager.Instance.data != null;
    }
}
