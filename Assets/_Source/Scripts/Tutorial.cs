using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private Transform _hand3D;
    private CanvasGroup _canvas;

    [SerializeField] private LilyBase _lily1;
    [SerializeField] private LilyBase _lily2;
    [SerializeField] private LilyBase _lily3;
    [SerializeField] private LilyBase _lily4;

    private bool IsComplated
    {
        get => Game.Data.Saves.IsTutorialComplated;
        set
        {
            Game.Data.Saves.IsTutorialComplated = value;
            _canvas.alpha = value ? 1 : 0;
            _canvas.interactable = value;
            _canvas.blocksRaycasts = value;
            Game.Data.SaveProgress();
        }
    }

    private bool IsShow
    {
        set
        {
            _canvas.alpha = value ? 1 : 0;
            _canvas.interactable = value;
            _canvas.blocksRaycasts = value;
        }
    }

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        IsShow = false;

        if (IsComplated) Destroy(this);
        else StartCoroutine(TutorialProcess());
    }

    public void Click()
    {
        if (_lily1.State != LilyState.Attack)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily1);
            Game.Locator.Hand3D.IsShow = false;

            IsShow = true;

            _objects[0].SetActive(true);

            return;
        }

        if(_lily2.State != LilyState.Speed)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily2);
            Game.Locator.Hand3D.IsShow = false;

            IsShow = true;

            _objects[1].SetActive(true);

            return;
        }
    }

    private IEnumerator TutorialProcess()
    {
        Game.Locator.Hand3D.IsShow = true;
        _hand3D.position = _lily1.transform.position;

        yield return new WaitWhile(() => _lily1.State != LilyState.Attack);

        _objects[0].SetActive(false);
        _hand3D.position = _lily2.transform.position;
        Game.Locator.Hand3D.IsShow = true;

        IsShow = false;

        yield return new WaitWhile(() => _lily2.State != LilyState.Speed);

        _objects[1].SetActive(false);

        IsShow = false;

        yield return new WaitWhile(() => _lily3.State != LilyState.Heal);
        Debug.Log("TutorialStep1 IsComplated");
        //IsComplated = true;
    }
}
