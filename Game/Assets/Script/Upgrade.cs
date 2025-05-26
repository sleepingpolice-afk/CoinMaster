using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public TMP_Text upgradeText;
    public double clickUpgradeBaseCost;
    public double clickUpgradeCostMultiplier;

    public CounterManager counterManager;

    public Data data;

    void Start()
    {
        if (counterManager == null)
            Debug.LogError("counterManager is NULL in Upgrade");

        if (counterManager.data == null)
            Debug.LogError("counterManager.data is NULL in Upgrade");

        clickUpgradeBaseCost = 10;
        clickUpgradeCostMultiplier = 1.5f;

        counterManager.data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            BuyUpgrade();
        }
    }

    void OnDestroy()
    {
        counterManager.data.OnDataChanged -= UpdateUI;
    }

    public void BuyUpgrade()
    {
        if (counterManager.data.coins >= cost())
        {
            counterManager.data.coins -= cost();
            counterManager.data.clickUpgradeLevel++;
            counterManager.clickValue += counterManager.clickValue * 0.15f + 3;
            UpdateUI();
        }
    }


    public double cost()
    {
        return (double)(clickUpgradeBaseCost * Mathf.Pow((float)clickUpgradeCostMultiplier, counterManager.data.clickUpgradeLevel));
    }

    private void UpdateUI()
    {
        upgradeText.text = "Upgrade Cost: " + cost().ToString("F2") + "\n" +
                          "Current Level: " + counterManager.data.clickUpgradeLevel + "\n" +
                          "Click Value: " + counterManager.clickValue.ToString("F2");
    }
}
