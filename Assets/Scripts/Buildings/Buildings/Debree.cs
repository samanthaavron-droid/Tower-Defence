using System.Collections;
using TMPro;
using UnityEngine;

public class Debree : MonoBehaviour
{
    public StatsRuntime statsRuntime;
    public TextMeshProUGUI priceText;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (priceText != null)
            priceText.gameObject.SetActive(false);
    }
    public void StartRemoval(Builder builder)
    {
        if (priceText != null)
        {
            priceText.gameObject.SetActive(false);
            priceText = null;
        }
        StartCoroutine(FadeIn());
        StartCoroutine(Timer(builder));
    }
    private IEnumerator Timer(Builder builder)
    {
        yield return new WaitForSeconds(statsRuntime.debreeSize);
        builder.getOccupiedCells.Remove(builder.getTilemap.WorldToCell(transform.position));
        Destroy(gameObject);
    }
    private IEnumerator FadeIn()
    {
        float timeElapse = 0;
        float timeToElapse = statsRuntime.debreeSize;
        Color c = spriteRenderer.color;

        while (timeElapse < timeToElapse)
        {
            timeElapse += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, timeElapse / timeToElapse);
            spriteRenderer.color = c;

            yield return null;
        }
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
