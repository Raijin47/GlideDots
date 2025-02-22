using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialHelper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private GameObject _hand;

    private Tween _tween;
    public bool IsShow
    {
        set
        {
            _hand.SetActive(value);
        }
    }

    private void Start()
    {
        if (Game.Data.Saves.IsTutorialComplated) Destroy(this);
        else
        {
            _tween = _anchor.DOScale(0.7f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Game.Locator.Tutorial.Click();
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}