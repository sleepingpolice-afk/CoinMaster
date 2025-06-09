using TMPro;
using UnityEngine;
using System.Collections;

public class ProductionUpgrade : MonoBehaviour
{
    public Upgrade upgrade;
    public TMP_Text buttonText;
    public TMP_Text productionText;
    public TMP_Text WarningText;

    private Coroutine warningCoroutine;

    void Start()
    {

        if (upgrade == null)
            Debug.LogError("Upgrade is NULL in ProductionUpgrade");

        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            DataManager.Instance.data.OnDataChanged += UpdateUI;
        }
        UpdateUI();
    }

    void Update()
    {
        if (DataManager.Instance.data == null)
        {
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
        if (DataManager.Instance != null && DataManager.Instance.data != null)
        {
            DataManager.Instance.data.OnDataChanged -= UpdateUI;
        }
    }

    public void BuyProduction()
    {
        if (DataManager.Instance.data.coins >= DataManager.Instance.data.productionUpgradeCost)
        {
            DataManager.Instance.data.coins -= DataManager.Instance.data.productionUpgradeCost;
            DataManager.Instance.data.passiveIncomeLevel++;
            DataManager.Instance.data.StartPassiveIncome(); // Restart coroutine
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
        if (buttonText != null && DataManager.Instance != null && DataManager.Instance.data != null) // Added null checks for DataManager
        {
            buttonText.text = "Production Increase 1\n" +
                                "Buy Production Upgrade\nCost: " + NumberFormatter.FormatNumber(cost()) +
                              "\nPassive Income Level: " + DataManager.Instance.data.passiveIncomeLevel;

            // Use the new PassiveIncomeRate property (assuming it will be named with a capital P in Data.cs)
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
