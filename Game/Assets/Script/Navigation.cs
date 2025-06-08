using TMPro;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesSelected;
    public GameObject ProductionUpgradesSelected;
    public GameObject BuyWeaponsSelected;

    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;

    private void Start()
    {
        if (ClickUpgradesSelected == null || ProductionUpgradesSelected == null || BuyWeaponsSelected == null)
        {
            Debug.LogError("Navigation: ClickUpgradesSelected or ProductionUpgradesSelected or BuyWeaponsSelected is NULL");
            return;
        }

        SwitchUpgrades("Click");
    }

    public void SwitchUpgrades(string location)
    {
        bool isClick = location == "Click";

        SetActiveWithChildren(ClickUpgradesSelected, isClick, "ClickText");
        SetActiveWithChildren(ProductionUpgradesSelected, !isClick, "ProductionText");
        SetActiveWithChildren(BuyWeaponsSelected, location == "Weapon");
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
