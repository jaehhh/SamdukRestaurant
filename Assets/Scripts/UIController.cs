using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject foodImagePrefab;

    public void CreateFoodUI(Sprite sprite, List<GameObject> list)
    {
        GameObject clone = Instantiate(foodImagePrefab);

        clone.GetComponent<SpriteRenderer>().sprite = sprite;
        list.Add(clone);

        clone.SetActive(false);
    }
}
