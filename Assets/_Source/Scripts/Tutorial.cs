using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button _button;


    private CanvasGroup _canvas;

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

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    //private void Start()
    //{
    //    if (IsComplated) IsComplated = true;
    //    else StartCoroutine(TutorialProcess());
    //}

    //private IEnumerator TutorialProcess()
    //{
    //    _button.transform.position =

    //    yield return new WaitWhile(() => Game.Data.Saves.Lilies[20] != 1);
    //    yield return null;

    //    IsComplated = true;
    //}
}
