using System.Collections;
using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    public TMP_Text WarningText;

    public TMP_Text buttonText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager is NULL in BuyWeapon");
            return;
        }
        else if (DataManager.Instance.data == null)
        {
            Debug.LogError("DataManager.data is NULL in BuyWeapon");
            return;
        }

        DataManager.Instance.data.OnDataChanged += UpdateUI;
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
        if (DataManager.Instance.data != null)
        {
            DataManager.Instance.data.OnDataChanged -= UpdateUI;
        }
    }
    public void BuyWeapons()
    {
        if (DataManager.Instance.data.coins >= cost())
        {
            DataManager.Instance.data.coins -= cost();
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowWarning("Not enough coins to buy this weapon upgrade!", 2f));
        }
    }

    private double cost()
    {
        return DataManager.Instance.data.weaponCost;
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
