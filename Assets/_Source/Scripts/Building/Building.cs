using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private int _grade;
    [SerializeField] private Building _nextGrade;

    [SerializeField] private Collider _collider;

    [SerializeField] private Direction[] _directions;
    [SerializeField] private Renderer _renderer;

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
            ball.Power = IncreacePower(ball.Power);
            Game.Wallet.Add(1);
        }
    }

    private float IncreacePower(float value)
    {

        return value;
    }
}