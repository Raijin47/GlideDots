using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBase : PoolMember
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;

    public Vector3 Direction { private get; set; }

    public readonly List<Transform> Points = new();

    private Coroutine _coroutine;
    private Rigidbody _rigidbody;
    private Transform _target;

    public override void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Activate();
    }

    
    public override void Activate()
    {
        Direction = Vector3.back;

        ReleaseCoroutine();
        _coroutine = StartCoroutine(MovementProcess());
    }

    private IEnumerator MovementProcess()
    {
        while(true)
        {
            yield return GetPath();

            while (transform.position != _target.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private IEnumerator GetPath()
    {
        if (Physics.Raycast(transform.position + Direction, Direction, out RaycastHit hit, 100f, _layerMask))
            _target = hit.transform;

        yield return null;
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