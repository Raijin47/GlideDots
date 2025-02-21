using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _jumpDuration;

    private readonly WaitForSeconds Delay = new(0.18f);
    private readonly WaitForSeconds RessurectionTime = new(5f);
    private Animator[] _animators;
    private Sequence _sequence;
    private Coroutine _coroutine;
    private Timer _timer;

    private bool _isDeath;

    private readonly float _baseDelay = 5f;
    private readonly float _baseHealth = 10f;
    private readonly float _baseDamage = 1f;
    private float _health;
    private float _damage;

    private void Start()
    {
        _timer = new(5f);
        _animators = GetComponentsInChildren<Animator>();
        Game.Locator.Player.OnAttack += Player_OnAttack;

        _isDeath = true;
        _coroutine = StartCoroutine(UpdateProcess());
    }

    private void Activate()
    {
        _isDeath = false;

        _animators[0].Play("Idle");
        _animators[1].Play("Idle");
        _transforms[0].localPosition = new Vector3(-10 ,0, 0);
        _transforms[1].localPosition = new Vector3(10, 0, 0);

        float level = Game.Data.Saves.EnemyLevel;

        _health = Service.Calculate(level, _baseHealth, 1.1f);
        _damage = Mathf.Clamp(Service.Calculate(level, _baseDamage, 1.05f), 0, 499);
        _timer.RequiredTime = Mathf.Clamp(Service.Calculate(level, _baseDelay, 0.98f), 0.1f, 5);

        CheckPos();
    }

    private void Jump()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();

        _sequence
            .Append(_transforms[0].DOLocalJump(_transforms[0].localPosition + Vector3.right, _jumpPower, 1, _jumpDuration).SetEase(Ease.OutBack))
            .Join(_transforms[1].DOLocalJump(_transforms[1].localPosition + Vector3.left, _jumpPower, 1, _jumpDuration).SetEase(Ease.OutBack))
            .OnComplete(CheckPos);
    }

    private void CheckPos()
    {
        if (_transforms[0].localPosition.x != -2)
            Jump();
        else
        {
            Release();
            _coroutine = StartCoroutine(UpdateProcess());
        }
    }

    private IEnumerator UpdateProcess()
    {
        while (!_isDeath)
        {
            _timer.Update();
            if (_timer.IsCompleted)
            {
                _animators[0].SetTrigger("Attack");
                Game.Audio.PlayClip(2);
                yield return Delay;
                Attack();

                _animators[1].SetTrigger("Attack");
                Game.Audio.PlayClip(2);
                yield return Delay;
                Attack();

                _timer.RestartTimer();
            }

            yield return null;
        }

        yield return RessurectionTime;
        Activate();
    }

    private void Attack()
    {
        Game.Locator.Player.Health -= _damage;
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void Player_OnAttack(float value)
    {
        if (_isDeath) return;

        _health -= value;
        if (_health < 0)
        {
            _isDeath = true;
            Game.Wallet.Add((int)(Service.Calculate(Game.Data.Saves.EnemyLevel, _baseHealth, 1.1f) * Service.Income));
            Game.Data.Saves.EnemyLevel++;
            Game.Data.SaveProgress();

            _animators[0].SetTrigger("Die");
            _animators[1].SetTrigger("Die");
        }
    }
}