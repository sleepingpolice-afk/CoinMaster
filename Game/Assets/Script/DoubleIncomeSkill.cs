using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoubleIncomeSkill : MonoBehaviour
{
    public CounterManager counterManager;
    public float skillDuration = 5f;
    public float cooldownDuration = 20f;

    public TMP_Text cooldownText;
    public Button skillButton;
    public RawImage skillIcon;

    [Tooltip("Unlock key used by EventManager")]
    public string unlockKey;

    private bool isOnCooldown = false;
    private double originalRate;

    private Image buttonImage;

    private Color originalButtonColor;
    private Color originalIconColor;
    private Color originalTextColor;

    private float cooldownAlpha = 0.5f;
    private float darkenFactor = 0.6f;

    private void Start()
    {
        cooldownText.text = "";
        skillButton.onClick.AddListener(ActivateSkill);
        skillButton.gameObject.SetActive(false);
        buttonImage = skillButton.GetComponent<Image>();

        if (buttonImage != null)
            originalButtonColor = buttonImage.color;

        if (skillIcon != null)
            originalIconColor = skillIcon.color;

        if (cooldownText != null)
            originalTextColor = cooldownText.color;

        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnUnlocked += HandleUnlock;

            if (EventManager.Instance.IsUnlocked(unlockKey))
                UnlockSkill();
        }
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnUnlocked -= HandleUnlock;
    }

    private void HandleUnlock(string key)
    {
        if (key == unlockKey)
        {
            UnlockSkill();
        }
    }

    private void UnlockSkill()
    {
        skillButton.gameObject.SetActive(true);
    }

    public void ActivateSkill()
    {
        if (isOnCooldown) return;
        if (EventManager.Instance != null && !EventManager.Instance.IsUnlocked(unlockKey)) return;

        StartCoroutine(SkillRoutine());
    }

    private IEnumerator SkillRoutine()
    {
        isOnCooldown = true;
        skillButton.interactable = false;

        originalRate = DataManager.Instance.data.passiveIncomeRate;
        DataManager.Instance.data.passiveIncomeRate *= 2;

        float timer = skillDuration;
        while (timer > 0)
        {
            cooldownText.color = Color.green;
            cooldownText.text = $"{timer:F0}";
            yield return new WaitForSeconds(1f);
            timer--;
        }

        DataManager.Instance.data.passiveIncomeRate = originalRate;

        SetCooldownAppearance();

        timer = cooldownDuration;
        while (timer > 0)
        {
            cooldownText.color = Color.black;
            cooldownText.text = $"{timer:F0}";
            yield return new WaitForSeconds(1f);
            timer--;
        }

        ResetAppearance();

        skillButton.interactable = true;
        cooldownText.text = "";
        isOnCooldown = false;
    }

    private void SetCooldownAppearance()
    {
        if (buttonImage != null)
        {
            Color color = originalButtonColor;
            color.r *= darkenFactor;
            color.g *= darkenFactor;
            color.b *= darkenFactor;
            color.a = cooldownAlpha;
            buttonImage.color = color;
        }

        if (skillIcon != null)
        {
            Color iconColor = originalIconColor;
            iconColor.r *= darkenFactor;
            iconColor.g *= darkenFactor;
            iconColor.b *= darkenFactor;
            iconColor.a = cooldownAlpha;
            skillIcon.color = iconColor;
        }

        if (cooldownText != null)
        {
            Color textColor = originalTextColor;
            textColor.a = cooldownAlpha;
            cooldownText.color = textColor;
        }
    }

    private void ResetAppearance()
    {
        if (buttonImage != null)
            buttonImage.color = originalButtonColor;

        if (skillIcon != null)
            skillIcon.color = originalIconColor;

        if (cooldownText != null)
            cooldownText.color = originalTextColor;
    }
}
