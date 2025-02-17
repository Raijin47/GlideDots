using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Vector3 _direction;

    private LilyState _state;
    private Coroutine _coroutine;
    private Timer _timer;

    public LilyState State
    {
        get => _state;
        set
        {
            Release();

            switch (value)
            {
                case LilyState.None:
                    
                    break;

                case LilyState.Heal: 

                    break;

                case LilyState.Attack:
                    _coroutine = StartCoroutine(SpawnProcess());

                    break;
                case LilyState.Host:

                    break;
            }

            _lily.SetActive(value != LilyState.None);

            _hairs[(int)_state].SetActive(false);
            _hairs[(int)value].SetActive(true);

            _state = value;
        }
    }

    private void Start()
    {
        _timer = new(5f);
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            _timer.Update();

            if (_timer.IsCompleted)
            {
                Game.Locator.Factory.SpawnAttackBall(_spawnPoint.position, _direction);
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

    public void OnPointerClick(PointerEventData eventData) => Game.Locator.LilyHandler.ShowPanel(this);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(_spawnPoint.position, .3f);
        Gizmos.DrawLine(_spawnPoint.position, _spawnPoint.position + _direction);
    }
}

public enum LilyState
{
    None, Attack, Host, Heal
}