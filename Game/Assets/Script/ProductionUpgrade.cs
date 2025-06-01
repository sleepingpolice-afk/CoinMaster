using UnityEngine;

public class ProductionUpgrade : MonoBehaviour
{
    public CounterManager counterManager;

    public Upgrade upgrade;

    public double productionBaseCost;
    public double productionCostMultiplier;

    void Start()
    {
        if (counterManager == null)
            Debug.LogError("CounterManager is NULL in ProductionUpgrade");
        if (upgrade == null)
            Debug.LogError("Upgrade is NULL in ProductionUpgrade");

        productionBaseCost = 100;
        productionCostMultiplier = 1.2f;

        counterManager.data.OnDataChanged += upgrade.UpdateUI;
        upgrade.UpdateUI();
    }

    void Update()
    {
        counterManager.data.UpdatePassiveIncome(); // Add this line
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            BuyProduction();
        }
    }

    void OnDestroy()
    {
        counterManager.data.OnDataChanged -= upgrade.UpdateUI;
    }

    public void BuyProduction()
    {
        if (counterManager.data.coins >= cost())
        {
            counterManager.data.coins -= cost();
            counterManager.data.passiveIncomeLevel++;
            counterManager.data.passiveIncomeRate += 1 + counterManager.data.passiveIncomeLevel * 0.1;
            counterManager.data.StartPassiveIncome();
            upgrade.UpdateUI();
        }
    }
    
    public double cost()
    {
        return (double)(productionBaseCost * Mathf.Pow((float)productionCostMultiplier, counterManager.data.passiveIncomeLevel));
    }
}
