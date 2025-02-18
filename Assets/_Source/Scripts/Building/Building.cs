using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _grade;
    [SerializeField] private Collider _collider;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private  Renderer _mainRenderer;

    private readonly Color AvailableColor = new(1, 1, 0, .5f);
    private readonly Color UnavailableColor = new(1, 0, 0, .5f);
    private readonly Color BuildColor = new(0, 0, 0, 0f);

    public void SetTransparent(bool available)
    {
        _collider.enabled = false;
        _mainRenderer.material.color = available ? AvailableColor : UnavailableColor;
    }

    public void SetNormal()
    {
        _mainRenderer.material.color = BuildColor;
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBase ball))
        {
            ball.Direction = _direction;
            Game.Audio.PlayClip(0);
            Game.Wallet.Add(1);
        }
    }
}