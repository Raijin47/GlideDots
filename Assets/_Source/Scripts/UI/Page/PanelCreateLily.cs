using DG.Tweening;

public class PanelCreateLily : PanelBase
{
    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).

        Join(_components[0].DOScale(0, _delay).SetEase(Ease.InBack)).
        Join(_components[0].DOLocalMoveY(-300, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOScale(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[0].DOLocalMoveY(0, _delay).SetEase(Ease.OutBack).From(-300)).

            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        Game.Locator.LilyHandler.OnShowPanel += Enter;
    }
}