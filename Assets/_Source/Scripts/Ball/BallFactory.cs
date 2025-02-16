using System.Collections;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BallBase _ballBase;

    private Pool _poolBall;
    private Timer _timer;

    private void Start()
    {
        _poolBall = new(_ballBase);
        _timer = new(5f);

        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (true)
        {
            _timer.Update();

            if(_timer.IsCompleted)
            {
                _poolBall.Spawn(_spawnPoint.position);
                _timer.RestartTimer();
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(_spawnPoint.position, .3f);
        Gizmos.DrawLine(_spawnPoint.position, _spawnPoint.position + Vector3.back);
    }
}