using TMPro;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject prefabIU;
    public GameObject UI;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI stats;
    [SerializeField] private Vector3 worldOffset;
    private StatsTemplate statsTemplate;
    private RectTransform rectTransform;
    void Start()
    {
        statsTemplate = prefabIU.GetComponent<Unit>().statsTemplate;
        rectTransform = UI.GetComponent<RectTransform>();
    }
    public void UpdateInterface()
    {
        if (UI == null || towerName == null || stats == null) return;

        Vector3 targetWorldPos = transform.position + worldOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetWorldPos);
        RectTransform canvasRect = UI.transform.parent.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out Vector2 canvasPoint);
        rectTransform.localPosition = canvasPoint;

        towerName.text = gameObject.name;
        stats.text = "Health: " + statsTemplate.health + "\nPrice: " + statsTemplate.worth + "\nAttack Range: " + (statsTemplate.attackRange / 1.4).ToString("F0") + "\nDamage: " + statsTemplate.damage + "\nRecharge: " + statsTemplate.rechargeTime + "s\nBullet Speed: " + statsTemplate.projectileSpeed + "\nBuilding Time: " + statsTemplate.buildingSpeed + "s";
    }
}
