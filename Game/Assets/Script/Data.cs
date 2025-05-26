using System;

public class Data
{
    public event Action OnDataChanged;

    private double _coins;
    public double coins
    {
        get => _coins;
        set
        {
            _coins = value;
            OnDataChanged?.Invoke();
        }
    }

    private int _clickUpgradeLevel;
    public int clickUpgradeLevel
    {
        get => _clickUpgradeLevel;
        set
        {
            _clickUpgradeLevel = value;
            OnDataChanged?.Invoke();
        }
    }

    public Data()
    {
        _coins = 0;
        _clickUpgradeLevel = 0;
    }
}
