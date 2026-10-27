using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;

public class Builder : MonoBehaviour
{
    public static Builder instance;
    public Tilemap tilemap;
    [SerializeField] private Camera _mainCamera;
    public GameObject currentPrefab = null;
    private Vector3Int _currentCellPosition;
    public List<Vector3Int> occupiedCells = new();
    private bool _buildingOnMouse = false;
    public float currentResource;
    [SerializeField] private Unit city;
    [SerializeField] private Image resources;
    private Vector3 lastMousePos;
    private BuildingBase towerUp;
    private Debree debree;
    private Grabber graber;
    private LayerMask buildingLayer;
    void Awake()
    {
        instance = this;
        buildingLayer = LayerMask.GetMask("Building", "Debree", "Tiles");
    }
    void Start()
    {
        if (GlobalSettings.GetSetDifficulty > 1)
        {
            currentResource = city.GetStatsTemplate.worth * (1f/2f * GlobalSettings.GetSetDifficulty);
        } else
        {
            currentResource = city.GetStatsTemplate.worth;
        }

        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        MouseControls();

        if (_buildingOnMouse)
            BuildingOnMouse();

        UpdateResources();

        if (lastMousePos != Input.mousePosition)
        {
            CheckBuildingOnMouse();
            lastMousePos = Input.mousePosition;
        }
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
                    Grabber grabber = hit.GetComponent<Grabber>();
                    if (grabber != null)
                    {
                        currentPrefab = Instantiate(grabber.GetPrefabUI, hit.gameObject.transform.position, Quaternion.identity);
                        currentPrefab.layer = 0;
                        _buildingOnMouse = true;
                        if (graber != null) graber.GetUI.SetActive(false);
                    } else if (towerUp != null)
                    {
                        RunUpgrader(towerUp);
                    } else if (debree != null)
                    {
                        RemoveDebree();
                    }
                }
            }
            else if (currentPrefab != null) //if you have something selected, tries to use it
            {
                PlaceNewBuilding();
            }
        }
    }
    private void CheckBuildingOnMouse()
    {
        Collider2D hit = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(_currentCellPosition), buildingLayer);

        BuildingBase newTower = hit != null ? hit.GetComponentInParent<BuildingBase>() : null;
        Debree newDebree = hit != null ? hit.GetComponent<Debree>() : null;
        Grabber newGraber = hit != null ? hit.GetComponent<Grabber>() : null;

        if (towerUp != null && towerUp != newTower)
        {
            if (towerUp.GetUpgradeText != null)
                towerUp.GetUpgradeText.gameObject.SetActive(false);
            towerUp = null;
        }
        if (debree != null && debree != newDebree)
        {
            if (debree.priceText != null)
                debree.priceText.gameObject.SetActive(false);
        }
        if (graber != null && graber != newGraber)
        {
            if (graber.GetUI != null)
                graber.GetUI.SetActive(false);
        }
        if (newTower != null)
        {
            towerUp = newTower;
            if (towerUp.GetWeaponType != WeaponType.None)
            {
                if (towerUp.GetUpgradeText == null || towerUp.GetWeaponLogic == null) return;

                towerUp.GetUpgradeText.text = "Upgrade:\n" + towerUp.GetStatsRuntime.worth * 0.5f + "$";
                towerUp.GetUpgradeText.gameObject.SetActive(true);
            }
        } else if (newDebree != null)
        {
            debree = newDebree;
            if (debree.priceText == null) return;

            debree.priceText.text = "Removal:\n" + debree.statsRuntime.debreeSize + "$";
            debree.priceText.gameObject.SetActive(true);
        } else if (newGraber != null)
        {
            graber = newGraber;
            if (graber.GetUI == null || currentPrefab != null) return;

            graber.UpdateInterface();
            graber.GetUI.SetActive(true);
        }
    }
    private void MouseToGrid()  
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        _currentCellPosition = tilemap.WorldToCell(mouseWorldPosition);
    }
    private void RunUpgrader(BuildingBase tower)
    {
        if (towerUp.GetWeaponType == WeaponType.None) return;
        if (tower.GetStatsRuntime.currentLevel == 3) return;
        if (tower.GetWeaponLogic == null) return;
        if (Transaction(tower.GetStatsRuntime.worth * 0.5f))
        {
            tower.LevelUp();
            if (towerUp.GetUpgradeText == null) return;
            towerUp.GetUpgradeText.text = "Upgrade:\n" + towerUp.GetStatsRuntime.worth * 0.5f + "$";
        }
    }
    private void PlaceNewBuilding()
    {
        if (tilemap.HasTile(_currentCellPosition) == false || IsCellOccupied(_currentCellPosition) == true)
        {
            BuilderReset(); 
            return;
        }
        if (Transaction(currentPrefab.GetComponent<BuildingBase>().GetStatsTemplate.worth))
        {
            GameObject building = Instantiate(currentPrefab);
            BuildingBase currentTower = building.GetComponent<BuildingBase>();

            if (currentTower.GetAttackRadius != null)
            {
                currentTower.GetAttackRadius.gameObject.SetActive(false); ;
            }
            currentTower.SetAlreadyPlaced = false;
            building.layer = 6; //Building layer
            building.transform.position = tilemap.GetCellCenterWorld(_currentCellPosition);
            occupiedCells.Add(_currentCellPosition);

            if (currentTower.GetAttackRadius != null) currentTower.GetAttackRadius.SetActive(false);

            currentTower.StartCoroutine(currentTower.StartBuilding());
        }

        BuilderReset();
    }
    public bool IsCellOccupied(Vector3Int cellPosition) //returns true if occupied
    {
        return occupiedCells.Contains(cellPosition);
    }
    private void BuildingOnMouse() //keeps the prefab of the current object on mouse
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        if (currentPrefab != null)
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
        if (Transaction(debree.statsRuntime.debreeSize))
            debree.StartRemoval(this);
    }
    public void AddResource(float amount)
    {
        currentResource += amount;
        currentResource = Mathf.Min(currentResource, 60f);
        Debug.Log("Enemy killed, current resource: " + currentResource);
    }
    private void UpdateResources()
    {
        if (resources == null) return;
        
        resources.fillAmount = currentResource / 60;
    }
    public bool Transaction(float m)
    {
        if (currentResource - m >= 0)
        {
            currentResource -= m;
            return true;
        }
        else return false;
    }
}
