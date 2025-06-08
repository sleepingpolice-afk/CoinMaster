using UnityEngine;
using TMPro;

public class CounterManager : MonoBehaviour
{
    
    public double clickValue = 1;
    [SerializeField] private TMP_Text counterText;

    public Upgrade upgrade;

    public static CounterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (DataManager.Instance == null || DataManager.Instance.data == null)
        {
            Debug.LogError("DataManager or DataManager.data is null in CounterManager.Start()");
            return;
        }
        DataManager.Instance.data.OnDataChanged += UpdateUI;
        UpdateUI();
    }

    void OnDestroy()
    {
        if (DataManager.Instance.data != null)
            DataManager.Instance.data.OnDataChanged -= UpdateUI;
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
        DataManager.Instance.data.coins += clickValue;
        UpdateUI();
    }

    private void UpdateUI()
    {
        counterText.text = "Currency: " + NumberFormatter.FormatNumber(DataManager.Instance.data.coins);
    }
}