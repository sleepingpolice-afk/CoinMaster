using System.Collections;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public TMP_Text upgradeText;
    public double clickUpgradeBaseCost;
    public double clickUpgradeCostMultiplier;

    public CounterManager counterManager;

    public ProductionUpgrade productionUpgrade;

    public Data data;

    public TMP_Text buttonText;
    public TMP_Text WarningText;

    private Coroutine warningCoroutine;

    void Start()
    {
        if (counterManager == null)
            Debug.LogError("counterManager is NULL in Upgrade");

        if (counterManager.data == null)
            Debug.LogError("counterManager.data is NULL in Upgrade");

        clickUpgradeBaseCost = 10;
        clickUpgradeCostMultiplier = 1.5f;

        counterManager.data.OnDataChanged += UpdateUI;

        WarningText.text = "";
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
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
        else
        {
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);

            warningCoroutine = StartCoroutine(ShowWarning("Not enough currency", 2f));
            Debug.LogWarning("Not enough currency to buy click upgrade!");
        }
    }


    public double cost()
    {
        return (double)counterManager.data.clickUpgradeCost;
    }

    public void UpdateUI()
    {
        buttonText.text = "Upgrade Click\n" +
                            "Cost: " + NumberFormatter.FormatNumber(cost()) + "\n" +
                          "Click Level: " + counterManager.data.clickUpgradeLevel + "\n";

        upgradeText.text = "Currency per Click: " + NumberFormatter.FormatNumber(counterManager.clickValue) + "\n";
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
