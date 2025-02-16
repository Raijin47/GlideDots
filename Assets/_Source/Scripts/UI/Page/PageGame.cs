using DG.Tweening;

public class PageGame : PanelBase
{
    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).
            Join(_components[0].DOLocalMoveY(200, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOLocalMoveY(0, _delay).SetEase(Ease.OutBack).From(200)).

        OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        Game.Action.OnEnter += Enter;
        Game.Action.OnLose += Exit;
        Game.Action.OnRestart += Enter;
    }
}