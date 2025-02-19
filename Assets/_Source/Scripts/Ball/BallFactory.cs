using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [Space(10)]
    [SerializeField] private BallBase _damageBall;
    [SerializeField] private BallBase _healBall;
    [SerializeField] private BallBase _speedBall;
    [SerializeField] private BallBase _attackBall;

    private Pool _poolDamageBall;
    private Pool _poolHealBall;
    private Pool _poolSpeedBall;
    private Pool _poolAttackBall;

    private void Start()
    {
        _poolDamageBall = new(_damageBall);
        _poolHealBall = new(_healBall);
        _poolSpeedBall = new(_speedBall);
        _poolAttackBall = new(_attackBall);
    }

    public void SpawnAttackBall(Vector3 position, Direction direction)
    {
        BallBase ball = _poolDamageBall.Spawn(position) as BallBase;
        ball.Direction = direction;
        ball.Target = GetPath(position, direction);
    }

    public void SpawnHealBall(Vector3 position, Direction direction)
    {
        BallBase ball = _poolHealBall.Spawn(position) as BallBase;
        ball.Direction = direction;
        ball.Target = GetPath(position, direction);
    }

    public void SpawnSpeedBall(Vector3 position, Direction direction)
    {
        BallBase ball = _poolSpeedBall.Spawn(position) as BallBase;
        ball.Direction = direction;
        ball.Target = GetPath(position, direction);
    }

    public Vector3 GetPath(Vector3 position, Direction dir)
    {
        Vector3 direction = dir switch
        {
            Direction.forward => Vector3.forward,
            Direction.right => Vector3.right,
            Direction.back => Vector3.back,
            Direction.left => Vector3.left,

            _ => throw new System.NotImplementedException(),
        };

        if (Physics.Raycast(position, direction, out RaycastHit hit, 100f, _layerMask))
            return hit.point;

        return Vector3.zero;
    }
}

public enum Direction
{
    forward, right, back, left
}