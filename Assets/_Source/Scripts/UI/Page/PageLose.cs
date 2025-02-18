using DG.Tweening;


public class PageLose : PanelBase
{
    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).

            Join(_components[0].DOScale(0, _delay).SetEase(Ease.InBack)).
            Join(_components[1].DOScaleX(0, _delay).SetEase(Ease.InBack)).
            Join(_components[2].DOLocalMoveX(-200, _delay).SetEase(Ease.InBack)).
            Join(_components[3].DOLocalMoveX(200, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOScale(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[1].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[2].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(-300)).
            Join(_components[3].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(300)).

            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        Game.Action.OnExit += Exit;
        Game.Action.OnLose += Enter;
        Game.Action.OnRestart += Exit;
    }
}