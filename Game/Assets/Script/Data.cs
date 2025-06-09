using System;
using UnityEngine;

public class Data
{
    public bool hasBoughtWeapon;
    public event Action OnDataChanged;

    public string playerId; 
    public string username; 
    public string email;    

    public double _coins;
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


    public double clickValueBase = 1;
    public double clickValueMultiplier = 1.15;
    public double clickValueBonusMultiplier = 1.0;
    public double clickValue
    {
        get
        {
            return (clickValueBase * Math.Pow(clickValueMultiplier, clickUpgradeLevel) + clickUpgradeLevel * 3) * clickValueBonusMultiplier;
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

    private const double BasePassiveIncomeAtLevelZero = 1.0;
    private const double PassiveIncomeIncreaseFactorPerLevel = 1.10;

    private double _passiveIncomeSkillMultiplier = 1.0;
    public double passiveIncomeSkillMultiplier 
    {
        get => _passiveIncomeSkillMultiplier;
        set
        {
            _passiveIncomeSkillMultiplier = value;
            OnDataChanged?.Invoke();
        }
    }

    public double passiveIncomeRate
    {
        get
        {
            return (BasePassiveIncomeAtLevelZero * Math.Pow(PassiveIncomeIncreaseFactorPerLevel, _passiveIncomeLevel)) * _passiveIncomeSkillMultiplier;
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
        // Use the calculated passiveIncomeRate property here
        if (_isPassiveIncomeActive && passiveIncomeRate > 0)
        {
            float currentTime = Time.time;
            // Grant income for every full second passed
            float elapsedSeconds = currentTime - _lastPassiveIncomeTime;
            if (elapsedSeconds >= 1.0f)
            {
                int secondsPassed = (int)elapsedSeconds;
                coins += passiveIncomeRate * secondsPassed;
                _lastPassiveIncomeTime += secondsPassed;
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
        _passiveIncomeSkillMultiplier = 1.0;
        username = "";
        email = "";
    }
}
