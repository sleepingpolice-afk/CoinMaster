using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void PlaySound(string sfxName)
    {
        if (string.IsNullOrEmpty(sfxName))
        {
            Debug.LogWarning("SFX Name is empty. Cannot play sound.", this);
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(sfxName);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance not found. Cannot play sound: " + sfxName, this);
        }
    }
}