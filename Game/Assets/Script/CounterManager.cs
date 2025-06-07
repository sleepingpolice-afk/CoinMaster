using UnityEngine;
using TMPro;

public class CounterManager : MonoBehaviour
{
    
    public double clickValue = 1;
    [SerializeField] private TMP_Text counterText;

    public Upgrade upgrade;

    public Data data;

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
        counterText.text = "Currency: " + NumberFormatter.FormatNumber(data.coins);
    }
}