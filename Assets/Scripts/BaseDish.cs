using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDish : MonoBehaviour
{
    private Recipe recipe;
    private UIController uiController;

    [HideInInspector] public List<int> foodTypeList = new List<int>();
    [HideInInspector] public List<int> cookTypeList = new List<int>();

    [HideInInspector] public List<GameObject> UIObject = new List<GameObject>(); // 음식UI 오브젝트

    [HideInInspector] public SpriteRenderer spriteRenderer;
    private FoodState foodState;

    public int CompleteFoodCode = -1;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foodState = GetComponent<FoodState>();


        recipe = GameObject.FindGameObjectWithTag("Table").GetComponent<Recipe>();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    // 재료를 합성하고 조합상태를 저장하는 메소드
    public void Base(FoodState food)
    {
        foodTypeList.Add((int)food.foodType);
        cookTypeList.Add((int)food.cookTypeSelect);

        // 재료 오름차순 정렬
        for(int i = 0; i < foodTypeList.Count; ++i)
        {
            if(foodTypeList[foodTypeList.Count-1] < foodTypeList[i])
            {
                int num = foodTypeList[i];
                foodTypeList[i] = foodTypeList[foodTypeList.Count - 1];
                foodTypeList[foodTypeList.Count - 1] = num;

                num = cookTypeList[i];
                cookTypeList[i] = cookTypeList[cookTypeList.Count - 1];
                cookTypeList[cookTypeList.Count - 1] = num;
            }
        }

        uiController.CreateFoodUI(food.GetComponentInChildren<SpriteRenderer>().sprite, UIObject);

        // 요리 담길 때마다 레시피 확인해서 알맞은 재료 담기면 상태 변경
        recipe.CheckRecipe(this);

        // + 똑같은 재료 두 개 이상 필요할 때, 조리방법 순서 맞지 않으므로 조리불가 버그 
    }

    public void FoodImageOn()
    {
        for(int i = 0; i < UIObject.Count; i++)
        {
            // UI 가동
            UIObject[i].SetActive(true);
            // UI의 부모를 플레이어로 지정
            UIObject[i].transform.SetParent(foodState.lastUserTransform);
            // UI의 위치를 플레이어로 설정
            UIObject[i].transform.position = foodState.lastUserTransform.position;
            // UI의 로컬좌표y를 인덱스에 따라 조금씩 높게 설정
            UIObject[i].transform.localPosition = new Vector3(0, (i*0.4f) +1.5f, 0);
        }
    }

    public void FoodImageOff()
    {
        for (int i = 0; i < UIObject.Count; i++)
        {
            UIObject[i].SetActive(false);
            UIObject[i].transform.SetParent(this.transform);
        }
    }
}
