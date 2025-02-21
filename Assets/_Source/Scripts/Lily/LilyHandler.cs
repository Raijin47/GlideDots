using System;
using TMPro;
using UnityEngine;

public class LilyHandler : MonoBehaviour
{
    public event Action OnShowPanel;

    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _names;
    [SerializeField] private TextMeshProUGUI _text;

    private LilyBase _lily;

    private int _currentPrice;
    private readonly int BasePrice = 100;

    private void Start()
    {
        _currentPrice = (int)Service.Calculate(Game.Data.Saves.LilyCount, BasePrice, 1.15f);
    }

    public void ShowPanel(LilyBase lily)
    {
        OnShowPanel?.Invoke();

        _lily = lily;
        
        for(int i = 0; i < _buttons.Length; i++)     
            _buttons[i].SetActive(i != (int)_lily.State);

        _text.text = lily.State == LilyState.None ? "Invoke" : "Change";

        _names[0].text = $"Clean <color=green>{50}</color><sprite=0>";
        _names[1].text = $"Attack <color=red>{_currentPrice}</color><sprite=0>";
        _names[2].text = $"Speed <color=red>{_currentPrice}</color><sprite=0>";
        _names[3].text = $"Heal <color=red>{_currentPrice}</color><sprite=0>";
    }

    public void Change(int state)
    {
        bool isClean = state == 0;

        if (!isClean && !Game.Wallet.Spend(_currentPrice)) return;
        if (isClean) Game.Wallet.Add(50);

        Game.Data.Saves.LilyCount += isClean ? -1 : 1;
        Game.Data.SaveProgress();

        _currentPrice = (int)Service.Calculate(Game.Data.Saves.LilyCount, BasePrice, 1.15f);

        _lily.State = (LilyState)state;
        Game.Locator.PanelLily.Exit();
    }
}