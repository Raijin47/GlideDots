using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BallBase : PoolMember
{

    [SerializeField] private BallType _type;

    private const float _speed = 5;
    private int _limitIncrease;

    public float Power { get; set; }

    public BallType Type => _type;
    public Direction Direction { get; set; }

    private Tween _tween;
    private Coroutine _coroutine;
    public Vector3 Target { get; set; }

    public override void Init() => Activate();
    
    public override void Activate()
    {
        _limitIncrease = 10;
        _tween?.Kill();
        _tween = transform.DOScale(.4f, 2).From(0).OnComplete(StartMovement);
        Power = Service.Power;
    }

    public void IncreasePower(int grade)
    {
        if (_limitIncrease <= 0) return;
        Power = Service.IncreaseValue(Power, grade);
        _limitIncrease--;
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
        _tween?.Kill();
    }
}

public enum BallType
{
    Speed, Damage, Heal, Attack
}