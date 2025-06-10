using UnityEngine;
using DG.Tweening;

public class UpgradePanelController : MonoBehaviour
{
    public Navigation upgradeNavigation;
    private bool panelOpen = false;

    private string currentTab = "";
    public GameObject dismissArea;

    private RectTransform panelRectTransform;
    private RectTransform canvasRectTransform;
    public float animationDuration = 0.5f;

    void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();
        if (panelRectTransform == null)
        {
            Debug.LogError("UpgradePanelController: Panel RectTransform not found on this GameObject.", this);
            enabled = false;
            return;
        }

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            canvasRectTransform = parentCanvas.GetComponent<RectTransform>();
        }

        if (canvasRectTransform == null)
        {
            Debug.LogWarning("UpgradePanelController: Parent Canvas RectTransform not found. Ensure the panel is a child of a Canvas. This might not affect the new positioning but is unusual.", this);
        }

        if (!panelOpen)
        {
            Vector2 initialPosition = panelRectTransform.anchoredPosition;
            initialPosition.x = panelRectTransform.rect.width / 2.0f;
            panelRectTransform.anchoredPosition = initialPosition;
        }
    }

    public void ShowClickUpgrades()
    {
        currentTab = "Click";
        OpenPanelWithTab("Click");
    }

    public void ShowProductionUpgrades()
    {
        currentTab = "Production";
        OpenPanelWithTab("Production");
    }

    public void ShowWeapon()
    {
        currentTab = "Weapon";
        OpenPanelWithTab("Weapon");
    }

    private void OpenPanelWithTab(string tab)
    {
        AudioManager.Instance.PlaySFX("PanelSlide");
        if (panelRectTransform == null) return;

        if (!panelOpen)
        {
            float targetX = -panelRectTransform.rect.width / 2.0f;
            panelRectTransform.DOAnchorPosX(targetX, animationDuration).SetEase(Ease.OutQuad);
            panelOpen = true;

            if (dismissArea != null) dismissArea.SetActive(true);
        }

        if (upgradeNavigation != null)
        {
            upgradeNavigation.SwitchUpgrades(tab);
        }
        else
        {
            Debug.LogError("UpgradePanelController: upgradeNavigation is not assigned!", this);
        }
    }

    public void ClosePanel()
    {
        if (panelRectTransform == null) return;

        if (panelOpen)
        {
            // Target closed position: fully off-screen to the right (left edge of panel at right edge of canvas)
            // Assumes panel's pivot X is 0.5 (center)
            float targetX = panelRectTransform.rect.width / 2.0f;
            panelRectTransform.DOAnchorPosX(targetX, animationDuration).SetEase(Ease.InQuad);
            panelOpen = false;

            if (dismissArea != null)
            {
                Debug.Log("Closing Upgrade Panel");
                dismissArea.SetActive(false);
            }
        }
    }
}