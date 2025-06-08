using TMPro;
using UnityEngine;
using System.Collections;

public class ProductionUpgrade : MonoBehaviour
{
    public Upgrade upgrade;

    // public double productionBaseCost;
    // public double productionCostMultiplier;
    public TMP_Text buttonText;
    public TMP_Text productionText;
    public TMP_Text WarningText;

    private Coroutine warningCoroutine;

    void Start()
    {

        if (upgrade == null)
            Debug.LogError("Upgrade is NULL in ProductionUpgrade");

        // Use global DataManager for data reference
        DataManager.Instance.data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    void Update()
    {
        if (DataManager.Instance.data == null)
        {
            Debug.LogError("counterManager or DataManager.Instance.data is null in ProductionUpgrade.Update()");
            return;
        }
        DataManager.Instance.data.UpdatePassiveIncome();

        if (Input.GetKeyDown(KeyCode.P))
        {
            BuyProduction();
        }
    }

    void OnDestroy()
    {
        DataManager.Instance.data.OnDataChanged -= UpdateUI;
    }

    public void BuyProduction()
    {
        if (DataManager.Instance.data.coins >= DataManager.Instance.data.productionUpgradeCost)
        {
            DataManager.Instance.data.coins -= DataManager.Instance.data.productionUpgradeCost;
            DataManager.Instance.data.passiveIncomeLevel++;
            DataManager.Instance.data.passiveIncomeRate += 1 + DataManager.Instance.data.passiveIncomeLevel * 0.1;
            DataManager.Instance.data.StartPassiveIncome();
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
        return (double)DataManager.Instance.data.productionUpgradeCost;
    }


    public void UpdateUI()
    {
        if (buttonText != null)
        {
            buttonText.text = "Production Increase 1\n" +
                                "Buy Production Upgrade\nCost: " + NumberFormatter.FormatNumber(cost()) +
                              "\nPassive Income Level: " + DataManager.Instance.data.passiveIncomeLevel;

            productionText.text = "\nPassive Income Rate: " + NumberFormatter.FormatNumber(DataManager.Instance.data.passiveIncomeRate);

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
