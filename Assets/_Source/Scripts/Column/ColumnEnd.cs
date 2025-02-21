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
            Game.Locator.Player.AddBall(ball.Type, ball.Power);
            Game.Wallet.Add((int)(ball.Power * Service.Income));
            Game.Audio.PlayClip(0);
        }
    }
}