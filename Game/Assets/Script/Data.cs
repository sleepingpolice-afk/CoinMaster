using System;
using UnityEngine;

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

    public double clickUpgradeBaseCost = 10;
    public double clickUpgradeCostMultiplier = 1.5;

    public double clickUpgradeCost
    {
        get
        {
            return clickUpgradeBaseCost * Math.Pow(clickUpgradeCostMultiplier, clickUpgradeLevel);
        }
    }


    private int _passiveIncomeLevel;
    public int passiveIncomeLevel
    {
        get => _passiveIncomeLevel;
        set
        {
            _passiveIncomeLevel = value;
            OnDataChanged?.Invoke();
        }
    }

    public double productionUpgradeBaseCost = 100;
    public double productionUpgradeCostMultiplier = 1.2;

    public double productionUpgradeCost
    {
        get
        {
            return productionUpgradeBaseCost * Math.Pow(productionUpgradeCostMultiplier, passiveIncomeLevel);
        }
    }

    private double _passiveIncomeRate = 0;

    public double passiveIncomeRate
    {
        get => _passiveIncomeRate;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Passive income rate cannot be negative.");
            _passiveIncomeRate = value;
            OnDataChanged?.Invoke();
        }
    }

    private bool _isPassiveIncomeActive = false;
    private float _lastPassiveIncomeTime;

    public void StartPassiveIncome()
    {
        if (!_isPassiveIncomeActive)
        {
            _isPassiveIncomeActive = true;
            _lastPassiveIncomeTime = Time.time;
        }
    }

    public void StopPassiveIncome()
    {
        _isPassiveIncomeActive = false;
    }

    public void UpdatePassiveIncome()
    {
        if (_isPassiveIncomeActive && _passiveIncomeRate > 0)
        {
            float currentTime = Time.time;
            if (currentTime - _lastPassiveIncomeTime >= 1.0f)
            {
                coins += _passiveIncomeRate;
                _lastPassiveIncomeTime = currentTime;
            }
        }
    }

    public double weaponBaseCost = 100;
    public double weaponCostMultiplier = 2.0;

    public double weaponCost
    {
        get
        {
            return weaponBaseCost * (clickUpgradeLevel + 1) * (passiveIncomeLevel + 1) * weaponCostMultiplier;
        }
    }


    public Data()
    {
        _coins = 0;
        _clickUpgradeLevel = 0;
        _passiveIncomeLevel = 0;
    }
}
