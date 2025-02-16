using DG.Tweening;
using UnityEngine;

public class PanelWallet : PanelBase
{
    [SerializeField] private CanvasGroup _canvaas;

    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).
            
        Join(_canvaas.DOFade(0, _delay)).
        Join(_components[0].DOLocalMoveY(300, _delay).SetEase(Ease.InBack)).
        Join(_components[1].DOLocalMoveY(-300, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_canvaas.DOFade(1, _delay)).
            Join(_components[0].DOLocalMoveY(0, _delay).SetEase(Ease.OutBack)).
            Join(_components[1].DOLocalMoveY(0, _delay).SetEase(Ease.OutBack)).

            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        Game.Action.OnEnter += Exit;
        Game.Action.OnExit += Enter;
    }
}