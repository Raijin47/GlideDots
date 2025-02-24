using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private Transform _hand3D;
    private CanvasGroup _canvas;

    [SerializeField] private PanelMain _panelMain;
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

        if (IsComplated) Destroy(gameObject);
        else StartCoroutine(TutorialProcess());
    }

    public void Click()
    {
        if (_lily1.State != LilyState.Attack)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily1);

            IsShow = true;
            Game.Locator.Hand3D.IsShow = false;
            _objects[0].SetActive(true);

            return;
        }

        if(_lily2.State != LilyState.Speed)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily2);


            IsShow = true;
            Game.Locator.Hand3D.IsShow = false;
            _objects[1].SetActive(true);

            return;
        }

        if(_lily3.State != LilyState.Heal)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily3);

            IsShow = true;
            Game.Locator.Hand3D.IsShow = false;
            _objects[2].SetActive(true);

            return;
        }

        if (_lily4.State != LilyState.Heal)
        {
            Game.Locator.LilyHandler.ShowPanel(_lily4);

            IsShow = true;
            Game.Locator.Hand3D.IsShow = false;
            _objects[2].SetActive(true);

            return;
        }
    }

    private IEnumerator TutorialProcess()
    {
        yield return new WaitForSeconds(1f);

        if (_lily1.State != LilyState.Attack)
        {
            Game.Locator.Hand3D.IsShow = true;
            _hand3D.position = _lily1.transform.position;
        }

        yield return new WaitWhile(() => _lily1.State != LilyState.Attack);

        if(_lily2.State != LilyState.Speed)
        {
            _objects[0].SetActive(false);

            _hand3D.position = _lily2.transform.position;
            Game.Locator.Hand3D.Hand.localRotation = Quaternion.Euler(0, 90, 0);
            Game.Locator.Hand3D.IsShow = true;

            IsShow = false;
        }

        yield return new WaitWhile(() => _lily2.State != LilyState.Speed);

        if(_lily3.State != LilyState.Heal)
        {
            _objects[1].SetActive(false);

            _hand3D.position = _lily3.transform.position;
            Game.Locator.Hand3D.Hand.localRotation = Quaternion.Euler(0, 0, 0);
            Game.Locator.Hand3D.IsShow = true;

            IsShow = false;
        }

        yield return new WaitWhile(() => _lily3.State != LilyState.Heal);

        if(_lily4.State != LilyState.Heal)
        {
            _objects[2].SetActive(false);

            _hand3D.position = _lily4.transform.position;
            Game.Locator.Hand3D.IsShow = true;

            IsShow = false;
        }

        yield return new WaitWhile(() => _lily4.State != LilyState.Heal);

        if(Game.Data.Saves.BuildCount != 1)
        {
            IsShow = true;
            _objects[2].SetActive(false);

            _panelMain.Enter();

            _objects[3].SetActive(true);
        }

        yield return new WaitWhile(() => Game.Data.Saves.BuildCount != 1);

        Destroy(Game.Locator.Hand3D.gameObject);
        IsShow = false;
        Game.Data.Saves.IsTutorialComplated = true;
        Game.Data.SaveProgress();
    }
}
