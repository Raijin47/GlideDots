using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingDrag : MonoBehaviour, IDragHandler
{
    private Vector2 mousePosition;

    private float offsetX, offsetY;
    public static bool mouseButtonReleased;

    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.z;   
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x - offsetX, 0, mousePosition.y - offsetY);

    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //var groundPlane = new Plane(Vector3.up, Vector3.zero);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (groundPlane.Raycast(ray, out float position))
        //{
        //    Vector3 worldPosition = ray.GetPoint(position);

        //    int x = Mathf.RoundToInt(worldPosition.x);
        //    int y = Mathf.RoundToInt(worldPosition.z);

        //    bool available = true;

        //    if (x < 0 || x > _gridSize.x - 1) available = false;
        //    if (y < 0 || y > _gridSize.y - 1) available = false;

        //    if (available && IsPlaceTaken(x, y)) available = false;

        //    _flyingBuilding.transform.position = new Vector3(x, 0, y);
        //    _flyingBuilding.SetTransparent(available);

        //    if (available && Input.GetMouseButtonDown(0))
        //    {
        //        PlaceFlyingBuilding(x, y);
        //    }
        //}
    }
}
