using UnityEngine;

public class BuildingsHandler : MonoBehaviour
{
    private Vector2Int _gridSize = new(7, 9);
    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _camera;

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];

        _camera = Camera.main;
    }

    public void CreateBuilding(Building buildingPrefab)
    {
        if (Game.Wallet.Money < 10) return;

        bool isCreate = false;
        
        for(int x = 0; x < _gridSize.x && !isCreate; x++)
        {
            for(int y = 0; y < _gridSize.y && !isCreate; y++)
            {
                if(_grid[x, y] == null)
                {
                    var build = Instantiate(buildingPrefab);
                    build.transform.position = new Vector3(x, 0, y);
                    _grid[x, y] = build;
                    isCreate = true;
                    break;
                }
            }
        }

        if(isCreate) Game.Wallet.Spend(10);
    }

    private void Update()
    { 
        return;
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > _gridSize.x - 1) available = false;
                if (y < 0 || y > _gridSize.y - 1) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                _flyingBuilding.transform.position = new Vector3(x, 0, y);
                _flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        return _grid[placeX, placeY] != null;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        _grid[placeX, placeY] = _flyingBuilding;

        _flyingBuilding.SetNormal();
        _flyingBuilding = null;
    }
}