using TMPro;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesSelected;
    public GameObject ProductionUpgradesSelected;

    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;

    public void SwitchUpgrades(string location)
    {
        bool isClick = location == "Click";

        SetActiveWithChildren(ClickUpgradesSelected, isClick, "ClickText");
        SetActiveWithChildren(ProductionUpgradesSelected, !isClick, "ProductionText");
    }


    private void SetActiveWithChildren(GameObject obj, bool active, string excludeName = "")
    {
        if (obj == null) return;

        foreach (Transform child in obj.transform)
        {
            if (child.name == excludeName) continue;
            child.gameObject.SetActive(active);
        }
    }

}
