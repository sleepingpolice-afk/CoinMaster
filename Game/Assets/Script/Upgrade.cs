using System.Collections;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public TMP_Text upgradeText;
    public double clickUpgradeBaseCost;
    public double clickUpgradeCostMultiplier;

    public ProductionUpgrade productionUpgrade;

    public TMP_Text buttonText;
    public TMP_Text WarningText;

    private Coroutine warningCoroutine;

    void Start()
    {
        if (DataManager.Instance == null)
            Debug.LogError("DataManager is NULL in Upgrade");
        if (DataManager.Instance.data == null)
            Debug.LogError("DataManager.Instance.data is NULL in Upgrade");

        DataManager.Instance.data.OnDataChanged += UpdateUI;

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
        DataManager.Instance.data.OnDataChanged -= UpdateUI;
    }

    public void BuyUpgrade()
    {
        if (DataManager.Instance.data.coins >= cost())
        {
            DataManager.Instance.data.coins -= cost();
            DataManager.Instance.data.clickUpgradeLevel++;
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
        return (double)DataManager.Instance.data.clickUpgradeCost;
    }

    public void UpdateUI()
    {
        buttonText.text = "Upgrade Click\n" +
                            "Cost: " + NumberFormatter.FormatNumber(cost()) + "\n" +
                          "Click Level: " + DataManager.Instance.data.clickUpgradeLevel + "\n";

        upgradeText.text = "Currency per Click: " + NumberFormatter.FormatNumber(DataManager.Instance.data.clickValue) + "\n";
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
