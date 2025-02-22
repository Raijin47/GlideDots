using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LilyBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _id;

    [Space(10)]
    [SerializeField] private GameObject _lily;
    [SerializeField] private GameObject[] _hairs;
    
    [Space(10)]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Direction _direction;

    private LilyState _state;
    private Coroutine _coroutine;
    private Timer _timer;

    public LilyState State
    {
        get => _state;
        set
        {
            _hairs[(int)_state].SetActive(false);
            _state = value;

            Game.Data.Saves.Lilies[_id] = (int)_state;
            Game.Data.SaveProgress();

            Activate();
        }
    }

    private void Start()
    {
        _timer = new(5f);

        _state = (LilyState)Game.Data.Saves.Lilies[_id];
        Activate();
    }

    private void Activate()
    {
        Release();

        switch (_state)
        {
            case LilyState.None:

                break;

            case LilyState.Heal:
                _coroutine = StartCoroutine(SpawnProcess());
                break;

            case LilyState.Attack:
                _coroutine = StartCoroutine(SpawnProcess());
                break;

            case LilyState.Speed:
                _coroutine = StartCoroutine(SpawnProcess());
                break;
        }

        _lily.SetActive(_state != LilyState.None);
        _hairs[(int)_state].SetActive(true);
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            _timer.Update();

            if (_timer.IsCompleted)
            {
                switch (_state)
                {
                    case LilyState.Attack: Game.Locator.Factory.SpawnAttackBall(_spawnPoint.position, _direction); break;
                    case LilyState.Heal: Game.Locator.Factory.SpawnHealBall(_spawnPoint.position, _direction); break;
                    case LilyState.Speed: Game.Locator.Factory.SpawnSpeedBall(_spawnPoint.position, _direction); break;
                }

                _timer.RestartTimer();
            }

            yield return null;
        }
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Game.Data.Saves.IsTutorialComplated) return;
        Game.Locator.LilyHandler.ShowPanel(this);
    }

}

public enum LilyState
{
    None, Attack, Speed, Heal
}