using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingsHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Building[] _prefabs;
    [SerializeField] private ButtonBase _buttonCreate;
    [SerializeField] private GameObject _mergeParticle;
    [SerializeField] private ParticleSystem _particle;

    private Vector2Int _gridSize = new(7, 9);
    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _camera;

    private readonly Color CreateColor = new(0, 0, 0, 0);
    private readonly Color MergeColor = new(1, 0, 1, 0.3f);
    private readonly Color AvailableColor = new(1, 1, 0, 0.3f);
    private readonly Color UnavailableColor = new(1, 0, 0, 0.3f);

    private int _onBeginX;
    private int _onBeginY;
    private bool _isMerge;

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _camera = Camera.main;
    }

    private void Start() => LoadingBuilds();

    private void LoadingBuilds()
    {
        int index = 0;

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                if (Game.Data.Saves.Cell[index] != 0)
                {
                    var build = Instantiate(_prefabs[Game.Data.Saves.Cell[index]]);
                    build.transform.position = new Vector3(x, 0, y);
                    _grid[x, y] = build;
                }

                index++;
            }
        }
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
                    SaveBuild(x, y, build.Grade);
                    //Game.Data.Saves.Grid[x, y] = build.Grade;
                    Game.Data.SaveProgress();
                    isCreate = true;
                    break;
                }
            }
        }

        if(isCreate) Game.Wallet.Spend(10);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            _onBeginX = Mathf.RoundToInt(worldPosition.x);
            _onBeginY = Mathf.RoundToInt(worldPosition.z);

            if(_grid[_onBeginX, _onBeginY] != null)
            {
                _flyingBuilding = _grid[_onBeginX, _onBeginY];
                _flyingBuilding.Color = AvailableColor;
                _flyingBuilding.Collider = false;
                _grid[_onBeginX, _onBeginY] = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_flyingBuilding == null) return;

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

            if (available && _grid[x, y] != null) available = false;

            _flyingBuilding.transform.position = new Vector3(x, 0, y);


            _isMerge = _grid[x, y] != null && _flyingBuilding.Grade == _grid[x, y].Grade;

            _flyingBuilding.Color = available ?
                AvailableColor : _isMerge ?
                MergeColor : UnavailableColor;

            _mergeParticle.transform.position = new Vector3(x, -0.5f, y);
            _mergeParticle.SetActive(_isMerge);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_flyingBuilding == null) return;

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

            if (available && _grid[x, y] != null) available = false;

            if(available)
            {
                _grid[x, y] = _flyingBuilding;
                //Game.Data.Saves.Grid[x, y] = _flyingBuilding.Grade;
                SaveBuild(_onBeginX, _onBeginY, 0);
                SaveBuild(x, y, _flyingBuilding.Grade);
            }
            else
            {
                if (_isMerge)
                {
                    Destroy(_grid[x, y].gameObject);

                    var newGrade = Instantiate(_flyingBuilding.NextGrade);
                    newGrade.transform.position = new Vector3(x, 0, y);
                    _grid[x, y] = newGrade;

                    //Game.Data.Saves.Grid[_onBeginX, _onBeginY] = 0;
                    //Game.Data.Saves.Grid[x, y] = newGrade.Grade;
                    SaveBuild(_onBeginX, _onBeginY, 0);
                    SaveBuild(x, y, newGrade.Grade);

                    Destroy(_flyingBuilding.gameObject);

                    _mergeParticle.SetActive(false);
                    _particle.transform.position = newGrade.transform.position;
                    _particle.Play();
                }
                else
                {
                    _grid[_onBeginX, _onBeginY] = _flyingBuilding;
                    _flyingBuilding.transform.position = new Vector3(_onBeginX, 0, _onBeginY);
                }

            }
        }

        Game.Data.SaveProgress();
        if (_flyingBuilding == null) return;
        _flyingBuilding.Collider = true;
        _flyingBuilding.Color = CreateColor;
        _flyingBuilding = null;
    }

    private void SaveBuild(int cellX, int cellY, int value)
    {
        int temp = 0;
        int index = 0;

        for(int x = 0; x < _gridSize.x; x++)
        {
            for(int y = 0; y < _gridSize.y; y++)
            {
                if (x == cellX && cellY == y) index = temp;
                temp++;
            }
        }

        Game.Data.Saves.Cell[index] = value;
    }
}