using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    public event Action<float> OnAttack;

    [SerializeField] private GameObject[] _balls;
    [SerializeField] private GameObject _particle;
    [SerializeField] private TextMeshPro _textPower;
    [SerializeField] private Slider _healthSlider;

    private Tween _tween;
    private readonly float _maxHealth = 1000;

    private int _charge;

    public float Health
    {
        get => Game.Data.Saves.Health;
        set
        {
            Game.Data.Saves.Health = Mathf.Clamp(value, 0, _maxHealth);
            Game.Data.SaveProgress();

            if (Game.Data.Saves.Health <= 0) Game.Action.SendLose();

            _tween?.Kill();
            _tween = _healthSlider.DOValue(Game.Data.Saves.Health, 1f);
        }
    }

    public float Damage
    {
        get => Game.Data.Saves.Damage;
        set
        {
            Game.Data.Saves.Damage = Mathf.Floor(value);
            _textPower.text = $"{Game.Data.Saves.Damage}<sprite=1>";
        }
    }

    private void Start()
    {
        _healthSlider.value = Game.Data.Saves.Health;
        _textPower.text = $"{Game.Data.Saves.Damage}<sprite=1>";
    }

    public void AddBall(BallType type, float power)
    {
        switch (type)
        {
            case BallType.Speed: AddÑharge(); break;
            case BallType.Heal: Health += power; break;
            case BallType.Damage: Damage += power; break;
        }
    }

    private void AddÑharge()
    {
        if (_charge >= 8)
        {
            Attack();

            for (int i = 0; i < _balls.Length; i++)
                _balls[i].SetActive(false);

            _charge = 0;
        }
        else
        {
            _balls[_charge].SetActive(true);
            _charge++;
        }
    }

    private void Attack()
    {
        _particle.SetActive(false);
        _particle.SetActive(true);

        Game.Audio.PlayClip(1);

        OnAttack?.Invoke(Damage);
        Damage = 0;
    }
}