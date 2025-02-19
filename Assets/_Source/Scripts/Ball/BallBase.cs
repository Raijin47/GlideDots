using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BallBase : PoolMember
{

    [SerializeField] private BallType _type;
    [SerializeField] private float _speed;
    [SerializeField] private float _power;

    public float Power { get; set; }

    public BallType Type => _type;
    public Direction Direction { get; set; }

    private Tween _tween;
    private Coroutine _coroutine;
    public Vector3 Target { get; set; }

    public override void Init() => Activate();
    
    public override void Activate()
    {
        _tween?.Kill();
        _tween = transform.DOScale(.4f, 2).From(0).OnComplete(StartMovement);
        Power = _power;
    }

    private void StartMovement()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(MovementProcess());
    }

    private IEnumerator MovementProcess()
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void ReleaseCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public override void Release()
    {

    }
}

public enum BallType
{
    Speed, Damage, Heal, Attack
}