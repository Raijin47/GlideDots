using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _grade;
    [SerializeField] private Building _nextGrade;

    [SerializeField] private Collider _collider;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private Direction[] _directions;

    public int Grade => _grade;
    public Building NextGrade => _nextGrade;
    public Color Color
    {
        set
        {
            _renderer.material.color = value;
        }
    }
    public bool Collider
    {
        set
        {
            _collider.enabled = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBase ball))
        {
            ball.Direction = _directions[(int)ball.Direction];
            ball.Target = Game.Locator.Factory.GetPath(transform.position, ball.Direction);
            ball.IncreasePower(_grade);
            Game.Audio.PlayClip(3);
        }
    }
}