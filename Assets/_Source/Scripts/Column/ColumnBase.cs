using UnityEngine;

public class ColumnBase : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BallBase ball))
        {
            ball.Direction = _direction;
            Game.Audio.PlayClip(0);
            Game.Wallet.Add(1);
        }
    }
}