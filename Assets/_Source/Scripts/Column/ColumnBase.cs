using UnityEngine;

public class ColumnBase : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    public Vector3 GetDirection(Vector3 direction)
    {
        return Vector3.left;
    }

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

public enum Direction
{
    left, right, up, down
}