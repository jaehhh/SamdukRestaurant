using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [System.Serializable]
    public class RecipeTable
    {
        public string name = "";
        public int price = 0;
        public List<int> foodTypeCode = new List<int>();
        public List<int> cookTypeCode = new List<int>();
    }

    [HideInInspector] public List<RecipeTable> recipeTable = new List<RecipeTable>();

    // 요리이미지 : 0 = 요리중, 이후 레시피 순서대로 완성이미지
    [SerializeField] private Sprite[] sprite;

    [SerializeField] public GameObject[] allMatFood; // 모든 재료 프리팹 : 정보추출용

    private void Awake()
    {
        SetRecipe();

    }

    // 레시피 스크립트 내 작성, 재료는 오름차순으로 정렬할 것
    private void SetRecipe()
    {
        // 변수 선언
        

        {// #0 
            string nameSet = "None";
            int priceSet = 0;

            List<int> foodTypeCodeSet = new List<int>();
            List<int> cookTypeCodeSet = new List<int>();

            foodTypeCodeSet.Add((int)FoodType.dish);
            cookTypeCodeSet.Add((int)CookType.none);

            SetCode(nameSet,priceSet, foodTypeCodeSet, cookTypeCodeSet);
        }

        { // #1 초콜릿라면
            string nameSet = "chocolete Ramen";
            int priceSet = 250;

            List<int> foodTypeCodeSet = new List<int>();
            List<int> cookTypeCodeSet = new List<int>();

            foodTypeCodeSet.Add((int)FoodType.chocolate);
            cookTypeCodeSet.Add((int)CookType.mince);
            foodTypeCodeSet.Add((int)FoodType.noodle);
            cookTypeCodeSet.Add((int)CookType.boil);
            foodTypeCodeSet.Add((int)FoodType.pepperPowder);
            cookTypeCodeSet.Add((int)CookType.notNeed);

            SetCode(nameSet, priceSet, foodTypeCodeSet, cookTypeCodeSet);
        }

        { // #2 마카롱찌개
            string nameSet = "MakaZZi";
            int priceSet = 350;

            List<int> foodTypeCodeSet = new List<int>();
            List<int> cookTypeCodeSet = new List<int>();

            foodTypeCodeSet.Add((int)FoodType.noodle);
            cookTypeCodeSet.Add((int)CookType.boil);
            foodTypeCodeSet.Add((int)FoodType.pepperPowder);
            cookTypeCodeSet.Add((int)CookType.notNeed);
            foodTypeCodeSet.Add((int)FoodType.makaCookie);
            cookTypeCodeSet.Add((int)CookType.mince);

            SetCode(nameSet, priceSet, foodTypeCodeSet, cookTypeCodeSet);
        }

        { // #3 달달고춧가루무침
            string nameSet = "SweetPepperMix";
            int priceSet = 400;

            List<int> foodTypeCodeSet = new List<int>();
            List<int> cookTypeCodeSet = new List<int>();

            foodTypeCodeSet.Add((int)FoodType.chocolate);
            cookTypeCodeSet.Add((int)CookType.mince);
            foodTypeCodeSet.Add((int)FoodType.pepperPowder);
            cookTypeCodeSet.Add((int)CookType.notNeed);
            foodTypeCodeSet.Add((int)FoodType.makaCookie);
            cookTypeCodeSet.Add((int)CookType.mince);

            SetCode(nameSet, priceSet, foodTypeCodeSet, cookTypeCodeSet);
        }

        { // #4 달달곤죽
            string nameSet = "SweetKoreanSoup";
            int priceSet = 300;

            List<int> foodTypeCodeSet = new List<int>();
            List<int> cookTypeCodeSet = new List<int>();

            foodTypeCodeSet.Add((int)FoodType.chocolate);
            cookTypeCodeSet.Add((int)CookType.boil);
            foodTypeCodeSet.Add((int)FoodType.makaCookie);
            cookTypeCodeSet.Add((int)CookType.boil);
            foodTypeCodeSet.Add((int)FoodType.salt);
            cookTypeCodeSet.Add((int)CookType.notNeed);

            SetCode(nameSet, priceSet, foodTypeCodeSet, cookTypeCodeSet);
        }
    }

    private void SetCode(string nameSet, int priceSet, List<int> foodTypeCodeSet, List<int> cookTypeCodeSet)
    {
        recipeTable.Add(new RecipeTable
        {
            name = nameSet,
            price = priceSet,
            foodTypeCode = foodTypeCodeSet,
            cookTypeCode = cookTypeCodeSet
        });
    }

    public void Debuging()
    {
        for (int recipeNum = 0; recipeNum < recipeTable.Count; ++recipeNum)
        {
            for(int foodTypeIndex = 0; foodTypeIndex < recipeTable[recipeNum].foodTypeCode.Count; foodTypeIndex++)
            {
                Debug.Log($"레시피[{recipeNum}]의 foodTypeList : {recipeTable[recipeNum].foodTypeCode[foodTypeIndex]}");
            }
            
            for(int cookTypeIndex = 0; cookTypeIndex < recipeTable[recipeNum].cookTypeCode.Count; cookTypeIndex++)
            {
                Debug.Log($"레시피{recipeNum}의 cookTypeList : {recipeTable[recipeNum].cookTypeCode[cookTypeIndex]}");
            }
        }
    }

    // 레시피 체크하고 상태변환 시켜주는 메소드
    public void CheckRecipe(BaseDish dish)
    {
        // Debuging();

        // 레시피 마다 체크
        for (int recipeNum = 1; recipeNum < recipeTable.Count; ++recipeNum)
        {
            // 재료 수가 맞는가
            if (recipeTable[recipeNum].foodTypeCode.Count == dish.foodTypeList.Count)
            {
                for (int i = 0; i < dish.foodTypeList.Count; ++i)
                {
                    // 들어간 재료가 맞는가
                    if (recipeTable[recipeNum].foodTypeCode[i] == dish.foodTypeList[i])
                    {
                        // 조리된 방법이 맞는가
                        if (recipeTable[recipeNum].cookTypeCode[i] == dish.cookTypeList[i])
                        {
                            // i번째 요리 통과
                        }
                        else
                        {
                            dish.spriteRenderer.sprite = sprite[0];
                            dish.CompleteFoodCode = 0;
                            return;
                        }
                    }
                    else
                    {
                        dish.spriteRenderer.sprite = sprite[0];
                        dish.CompleteFoodCode = 0;
                        return;
                    }
                }
            }
            else
            {
                dish.spriteRenderer.sprite = sprite[0];
                dish.CompleteFoodCode = 0;
                return;
            }

            dish.spriteRenderer.sprite = sprite[recipeNum];
            dish.CompleteFoodCode = recipeNum;

            return;
        }
    }

    // 주문서 출력
    public void GetFoodInfo(int recipeCode, out Sprite[] mats, out Sprite doneFood)
    {
        doneFood = sprite[recipeCode];  // 완성이미지

        mats = new Sprite[3];
        int index = 0;

        bool skip = false;

        // 레시피에 들어가는 재료 추출
        for (int i = 0; i < recipeTable[recipeCode].foodTypeCode.Count; i++)
        {
            for (int k = 0; k < allMatFood.Length; k++)
            {
                // [i]번째재료 <=> ALL재료[k] 비교
                if(recipeTable[recipeCode].foodTypeCode[i] == (int)(allMatFood[k].GetComponent<FoodState>().foodType))
                {
                    // [i]번째재료 조리방법 <=> 재료[k] 조리방법 비교
                    for (int c = 0; c < allMatFood[k].GetComponent<FoodState>().cookType.Length ; c++)
                    {
                        skip = false;

                        if (recipeTable[recipeCode].cookTypeCode[i] == (int)(allMatFood[k].GetComponent<FoodState>().cookType[c]))
                        {
                            // 조리방법 일치하면 요리후이미지 추출
                            mats[index] = allMatFood[k].GetComponent<FoodState>().afterSprite[c];
                            index++;

                            skip = true;

                            break;
                        }
                    }

                    if (skip) break;
                }
            }
        }
    }

    public int GetPrice(int foodCode)
    {
        return recipeTable[foodCode].price;
    }

    public Sprite GetFoodImage(int foodCode)
    {
        return sprite[foodCode];
    }
}
