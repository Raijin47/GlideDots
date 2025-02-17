using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    private readonly Color AvailableColor = new(1, 1, 0, .5f);
    private readonly Color UnavailableColor = new(1, 0, 0, .5f);
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