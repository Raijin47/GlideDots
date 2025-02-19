using UnityEngine;

public class ColumnEnd : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBase ball))
        {
            _particle.Play();
            ball.ReturnToPool();
            Game.Locator.Player.AddBall(ball.Type);
            Game.Audio.PlayClip(0);
            Game.Wallet.Add(1);
        }
    }
}