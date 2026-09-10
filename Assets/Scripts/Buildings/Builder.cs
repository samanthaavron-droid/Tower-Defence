using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Builder : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private Camera _mainCamera;
    public GameObject currentPrefab = null;
    private Vector3Int _currentCellPosition;
    public List<Vector3Int> occupiedCells = new();
    private bool _buildingOnMouse = false;
    public float currentResource;
    void Start()
    {
        
    }

    void Update()
    {
        MouseControls();

        if (_buildingOnMouse)
            BuildingOnMouse();
    }
    private void MouseControls()
    {
        MouseToGrid(); //grid coordinates

        if (Input.GetMouseButtonDown(0))
        {
            if (currentPrefab == null)
            {
                Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(_currentCellPosition));

                if (hit != null)
                {
                    if (hit.GetComponent<Grabber>() != null)
                    {
                        Grabber grabber = hit.GetComponent<Grabber>();
                        if (currentResource - grabber.buildingCost >= 0)
                        {
                            currentPrefab = Instantiate(grabber.prefabIU, hit.gameObject.transform.position, Quaternion.identity);
                            _buildingOnMouse = true;
                            currentResource -= grabber.buildingCost;

                            Debug.Log("Building Placed! Remaining Resources: " + currentResource);
                        }
                        else
                        {
                            Debug.Log("Not enough resources to build this!"); 
                            //run effect to show not enough resources
                        }
                    }
                }
            }
            else if (currentPrefab != null) //if you have something selected, tries to use it
            {
                PlaceNewBuilding();
            }
        }
    }
    private void MouseToGrid()  
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        _currentCellPosition = tilemap.WorldToCell(mouseWorldPosition);
    }
    private void PlaceNewBuilding()
    {
        if (IsCellOccupied(_currentCellPosition) == true)
        {
            //if the cell is occupied and you have remover, it runs remover
            if (currentPrefab.GetComponent<Debree>() == null) return;

            RemoveDebree();
        } else if (currentPrefab.GetComponent<Debree>() != null) //if the cell is not occupied and you have remover, it does nothing
        {
            BuilderReset();
        }
        else
        {
            GameObject building = Instantiate(currentPrefab);
            building.transform.position = tilemap.GetCellCenterWorld(_currentCellPosition);
            occupiedCells.Add(_currentCellPosition);

            BuilderReset();
        }
    }
    public bool IsCellOccupied(Vector3Int cellPosition) //returns true if occupied
    {
        return occupiedCells.Contains(cellPosition);
    }
    private void BuildingOnMouse() //keeps the prefab of the current object on mouse
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        currentPrefab.transform.position = mouseWorldPosition;
    }
    private void BuilderReset() //removes the object from the mouse and resets the builder
    {
        _buildingOnMouse = false;
        Destroy(currentPrefab);
        currentPrefab = null;
    }
    private void RemoveDebree() //starts the debree removal process and resets the builder
    {
        BuilderReset();
        Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(_currentCellPosition));

        if (hit.gameObject.GetComponent<Debree>() != null)
        {
            hit.gameObject.GetComponent<Debree>().StartRemoval(this);
        }
    }
}
