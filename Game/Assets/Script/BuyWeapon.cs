using System.Collections;
using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    public CounterManager counterManager;
    public TMP_Text WarningText;

    public TMP_Text buttonText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (counterManager == null)
        {
            Debug.LogError("CounterManager is NULL in BuyWeapon");
            return;
        }
        else if (counterManager.data == null)
        {
            Debug.LogError("CounterManager.data is NULL in BuyWeapon");
            return;
        }
        
        counterManager.data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightWindows))
        {
            BuyWeapons();
        }
    }
    void OnDestroy()
    {
        if (counterManager != null && counterManager.data != null)
        {
            counterManager.data.OnDataChanged -= UpdateUI;
        }
    }
    public void BuyWeapons()
    {
        if (counterManager.data.coins >= cost())
        {
            counterManager.data.coins -= cost();
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowWarning("Not enough coins to buy this weapon upgrade!", 2f));
        }
    }

    private double cost()
    {
        return counterManager.data.weaponCost;
    }

    public void UpdateUI()
    {
        buttonText.text = "Buy Weapon\n" +
                            "Cost: " + NumberFormatter.FormatNumber(cost()) + "\n";
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
