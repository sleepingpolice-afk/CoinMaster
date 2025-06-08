using System.Collections;
using UnityEngine;

public class UpgradePanelController : MonoBehaviour
{
    public Animator panelAnimator;
    public Navigation upgradeNavigation;
    private bool panelOpen = false;

    private string currentTab = "";
    public GameObject dismissArea;

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
        if (!panelOpen)
        {
            panelAnimator.SetTrigger("SlideIn");
            panelOpen = true;

            if (dismissArea != null) dismissArea.SetActive(true);
        }

        upgradeNavigation.SwitchUpgrades(tab);
    }

    public void ClosePanel()
    {
        if (panelOpen)
        {
            panelAnimator.ResetTrigger("SlideIn");
            panelAnimator.SetTrigger("SlideOut");
            panelOpen = false;

            if (dismissArea != null)
                dismissArea.SetActive(false);

        }
    }
}
