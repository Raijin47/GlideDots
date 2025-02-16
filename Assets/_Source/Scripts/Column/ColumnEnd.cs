using UnityEngine;

public class ColumnEnd : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public Vector3 GetDirection(Vector3 direction)
    {
        return Vector3.left;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBase ball))
        {
            _particle.Play();
            ball.ReturnToPool();
            Game.Audio.PlayClip(0);
            Game.Wallet.Add(1);
        }
    }
}