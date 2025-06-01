using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoubleClick : MonoBehaviour
{
    public CounterManager counterManager;
    public double skillDuration = 5f;
    public double cooldownDuration = 20f;

    [Tooltip("Unlock key used by EventManager")]
    public string unlockKey;


    private bool isOnCooldown = false;
    private double originalClickValue;

    public TMP_Text cooldownText;
    public Button skillButton;

    public RawImage skillIcon;
    private Image buttonImage;
    private float originalAlpha = 1f;
    private float cooldownAlpha = 0.5f;
    private float darkenFactor = 0.6f;

    private Color originalTextColor;
    private Color originalButtonColor;
    private Color originalIconColor;

    public Upgrade upgrade;

    private void Start()
    {
        cooldownText.text = "";
        skillButton.onClick.AddListener(ActivateSkill);

        // Initially hide the skill button
        skillButton.gameObject.SetActive(false);

        buttonImage = skillButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            originalButtonColor = buttonImage.color;
            originalAlpha = originalButtonColor.a;
        }

        if (skillIcon != null)
            originalIconColor = skillIcon.color;

        if (cooldownText != null)
            originalTextColor = cooldownText.color;

        // Subscribe to EventManager unlock events
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnUnlocked += HandleUnlock;

            // Check if already unlocked
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

        originalClickValue = counterManager.clickValue;
        counterManager.clickValue *= 2;

        double timer = skillDuration;
        upgrade.UpdateUI();
        while (timer > 0)
        {
            cooldownText.color = Color.green;
            cooldownText.text = $"{timer:F0}";
            yield return new WaitForSeconds(1f);
            timer--;
        }

        counterManager.clickValue = originalClickValue;
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
            Color textColor = cooldownText.color;
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