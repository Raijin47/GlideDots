using UnityEngine;

public class ColumnError : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBase ball))
        {
            _particle.Play();
            ball.ReturnToPool();
            Game.Audio.PlayClip(0);
        }
    }
}