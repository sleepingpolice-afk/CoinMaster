using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    private List<ConditionalNotification> notifications = new List<ConditionalNotification>();

    public void RegisterNotification(ConditionalNotification notification)
    {
        notifications.Add(notification);
    }

    private void Update()
    {
        foreach (var notif in notifications)
        {
            if (!notif.Triggered && notif.Condition != null && notif.Condition())
            {
                Notification.Instance.ShowNotification(notif.Title, notif.Message, notif.OnConfirm);
                notif.Triggered = true;
            }
        }
    }
}
