using TMPro;
using UnityEngine;
using System.Collections;

public class ProductionUpgrade : MonoBehaviour
{
    public CounterManager counterManager;

    public Upgrade upgrade;

    public double productionBaseCost;
    public double productionCostMultiplier;
    public TMP_Text buttonText;
    public TMP_Text productionText;
    public TMP_Text WarningText;

    private Coroutine warningCoroutine;

    void Start()
    {
        if (counterManager == null)
            Debug.LogError("CounterManager is NULL in ProductionUpgrade");
        if (upgrade == null)
            Debug.LogError("Upgrade is NULL in ProductionUpgrade");

        productionBaseCost = 100;
        productionCostMultiplier = 1.2f;

        counterManager.data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    void Update()
    {
        counterManager.data.UpdatePassiveIncome();

        if (Input.GetKeyDown(KeyCode.P))
        {
            BuyProduction();
        }
    }

    void OnDestroy()
    {
        counterManager.data.OnDataChanged -= UpdateUI;
    }

    public void BuyProduction()
    {
        if (counterManager.data.coins >= cost())
        {
            counterManager.data.coins -= cost();
            counterManager.data.passiveIncomeLevel++;
            counterManager.data.passiveIncomeRate += 1 + counterManager.data.passiveIncomeLevel * 0.1;
            counterManager.data.StartPassiveIncome();
            UpdateUI();
        }
        else
        {
            if(warningCoroutine != null)
            {
                StopCoroutine(warningCoroutine);
            }
            warningCoroutine = StartCoroutine(ShowWarning("Not enough currency", 2f));
            Debug.LogWarning("Not enough currency to buy production upgrade!");
        }
    }

    public double cost()
    {
        return (double)(productionBaseCost * Mathf.Pow((float)productionCostMultiplier, counterManager.data.passiveIncomeLevel));
    }


    public void UpdateUI()
    {
        if (buttonText != null)
        {
            buttonText.text = "Production Increase 1\n" +
                                "Buy Production Upgrade\nCost: " + NumberFormatter.FormatNumber(cost()) +
                              "\nPassive Income Level: " + counterManager.data.passiveIncomeLevel;

            productionText.text = "\nPassive Income Rate: " + NumberFormatter.FormatNumber(counterManager.data.passiveIncomeRate);

        }
        else
        {
            Debug.LogError("buttonText is NULL in ProductionUpgrade");
        }
    }
    
    private IEnumerator ShowWarning(string message, float duration)
    {
        WarningText.text = message;
        WarningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        WarningText.text = "";
        WarningText.gameObject.SetActive(false);
    }
}
