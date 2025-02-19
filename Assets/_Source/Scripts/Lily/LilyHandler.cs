using System;
using UnityEngine;

public class LilyHandler : MonoBehaviour
{
    public event Action OnShowPanel;

    private LilyState _state;
    private LilyBase _lily;

    [SerializeField] private ButtonBase _buttonAction;

    private void Start()
    {
        _buttonAction.OnClick.AddListener(Action);
    }

    public void ShowPanel(LilyBase lily)
    {
        OnShowPanel?.Invoke();

        _state = lily.State;
        _lily = lily;
    }

    public void Change(int state)
    {
        _state = (LilyState)state;
    }

    private void Action()
    {
        if(_lily.State != _state)
            _lily.State = _state;

        Game.Locator.PanelLily.Exit();
    }
}