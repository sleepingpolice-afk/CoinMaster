using UnityEngine;
using TMPro;

public class CounterManager : MonoBehaviour
{
    public double clickValue = 1;
    [SerializeField] private TMP_Text counterText;

    public Upgrade upgrade;

    public Data data;

    void Start()
    {
        data = new Data();
        data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    void OnDestroy()
    {
        data.OnDataChanged -= UpdateUI;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseCounter();
        }
    }

    public void IncreaseCounter()
    {
        data.coins += clickValue;
        UpdateUI();
    }

    private void UpdateUI()
    {
        counterText.text = "Coins: " + NumberFormatter.FormatNumber(data.coins);
    }
}