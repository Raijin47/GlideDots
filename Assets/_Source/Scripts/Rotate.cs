using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _delay, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}