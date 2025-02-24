using DG.Tweening;
using UnityEngine;

public class TutorialHandAnimation : MonoBehaviour
{
    private Tween _tween;

    private void OnEnable()
    {
        _tween?.Kill();

        _tween = transform.DOScale(0.8f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }
}