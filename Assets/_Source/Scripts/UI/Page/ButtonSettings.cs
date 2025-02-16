using DG.Tweening;
using UnityEngine;

public class ButtonSettings : PanelBase
{
    [SerializeField] private ButtonBase _button;
    private bool _isShow;

    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).
            Join(_components[0].DOLocalMoveY(0, _delay).SetEase(Ease.InBack)).
            Join(_components[1].DOLocalMoveY(-130, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        _sequence.Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOLocalMoveY(-130, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[1].DOLocalMoveY(-230, _delay).SetEase(Ease.OutBack).From(-130)).

            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();
        _button.OnClick.AddListener(Active);
        Game.Action.OnEnter += Action_OnEnter;
    }

    private void Action_OnEnter()
    {
        if (!_isShow) return;
        Exit();
        _isShow = false;
    }

    private void Active()
    {
        _isShow = !_isShow;

        if (_isShow) Enter();
        else Exit();
    }
}