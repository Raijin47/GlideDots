using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    private readonly Color AvailableColor = new(0, 1, 0, .8f);
    private readonly Color UnavailableColor = new(1, 0, 0, .8f);
    private readonly Color BuildColor = new(0, 0, 0, 0f);

    public void SetTransparent(bool available)
    {
        MainRenderer.material.color = available ? AvailableColor : UnavailableColor;
    }

    public void SetNormal()
    {
        MainRenderer.material.color = BuildColor;
    }
}