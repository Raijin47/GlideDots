using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private BallBase _damageBall;
    [SerializeField] private BallBase _healBall;
    [SerializeField] private BallBase _speedBall;

    private Pool _poolDamageBall;
    private Pool _poolHealBall;
    private Pool _poolSpeedBall;

    private void Start()
    {
        _poolDamageBall = new(_damageBall);
        _poolHealBall = new(_healBall);
        _poolSpeedBall = new(_speedBall);
    }

    public void SpawnAttackBall(Vector3 position, Vector3 direction)
    {
        BallBase ball = _poolDamageBall.Spawn(position) as BallBase;
        ball.Direction = direction;
    }

    public void SpawnHealBall(Vector3 position, Vector3 direction)
    {
        BallBase ball = _poolHealBall.Spawn(position) as BallBase;
        ball.Direction = direction;
    }

    public void SpawnSpeedBall(Vector3 position, Vector3 direction)
    {
        BallBase ball = _poolSpeedBall.Spawn(position) as BallBase;
        ball.Direction = direction;
    }
}