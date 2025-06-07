public class ConditionalNotification
{
    public string Title;
    public string Message;
    public System.Func<bool> Condition;
    public System.Action OnConfirm;

    public bool Triggered;

    public ConditionalNotification(string title, string message, System.Func<bool> condition, System.Action onConfirm = null)
    {
        Title = title;
        Message = message;
        Condition = condition;
        OnConfirm = onConfirm;
        Triggered = false;
    }
}