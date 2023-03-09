using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool bakeObject;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if(bakeObject == true)
        {
            BakeSortLayer();
        }
    }

    private void Update()
    {
        if (bakeObject) return;

        if (pivot != null)
            spriteRenderer.sortingOrder = (int)(pivot.transform.position.y * -100);
        else
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

    private void BakeSortLayer()
    {
        if (pivot != null)
            spriteRenderer.sortingOrder = (int)(pivot.transform.position.y * -100);
        else
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
