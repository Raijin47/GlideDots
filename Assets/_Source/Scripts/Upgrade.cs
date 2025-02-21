using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private ButtonBase _buttonUpgradeIncome;
    [SerializeField] private ButtonBase _buttonUpgradePower;
    [SerializeField] private TextMeshProUGUI _textUpgradeIncome;
    [SerializeField] private TextMeshProUGUI _textUpgradePower;

    private readonly float Degree = 1.10f;
    private readonly float BaseIncomePrice = 500f;
    private readonly float BasePowerPrice = 100f;

    private int _incomePrice;
    private int _powerPrice;

    private void Start()
    {
        _buttonUpgradeIncome.OnClick.AddListener(UpgradeIncome);
        _buttonUpgradePower.OnClick.AddListener(UpgradePower);
        UpdateIncomePrice();
        UpdatePowerPrice();
    }

    private void UpgradeIncome()
    {
        if (Game.Wallet.Spend(_incomePrice))
        {
            Game.Data.Saves.IncomeLevel++;
            Game.Data.SaveProgress();
            UpdateIncomePrice();
        }
    }

    private void UpgradePower()
    {
        if (Game.Wallet.Spend(_powerPrice))
        {
            Game.Data.Saves.PowerLevel++;
            Game.Data.SaveProgress();
            UpdatePowerPrice();
        }
    }

    private void UpdateIncomePrice()
    {
        _incomePrice = (int)Service.Calculate(Game.Data.Saves.IncomeLevel, BaseIncomePrice, Degree);
        _textUpgradeIncome.text = $"Income {Service.Income}x\n" +
            $"{_incomePrice}<sprite=0>";
    }

    private void UpdatePowerPrice()
    {
        _powerPrice = (int)Service.Calculate(Game.Data.Saves.PowerLevel, BasePowerPrice, Degree);
        _textUpgradePower.text = $"Power {Service.Power}\n" +
            $"{_powerPrice}<sprite=0>";
    }
}