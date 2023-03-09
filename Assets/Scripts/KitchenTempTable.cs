using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTempTable : Interection
{
    private FoodState myFood; // 올려져있는 음식

    public override void Interect(GameObject user, FoodState handFood)
    {
        // 음식 있고 플레이어손에 음식 없을 때 : 들기
        if (this.myFood != null && handFood == null)
        {
            myFood.Interect(user);

            myFood = null;
        }
        // 음식 없고 플레이어손 음식 없음 : 
        else if (this.myFood == null && handFood == null)
        {
        }
        // 음식 있고 플레이어손에 음식 있을 떄 : 음식 합체
        else if (this.myFood != null && handFood != null)
        {
            // 놓여져있는음식이 베이스이고, 손에있는음식이 완성된노베이스 음식일 때
            if (myFood.IsBase && handFood.IsBase == false && handFood.IsEnded) 
            {
                // 요리 합체
                myFood.GetComponent<BaseDish>().Base(handFood);

                // 손에 들고 있던 음식 삭제
                user.GetComponent<PlayerHands>().Food = null;
                Destroy(handFood.gameObject);
            }
        }
        // 음식이 없고 플레이어 손에 음식이 있을 때 : 내려놓기
        else
        {
            myFood = handFood;

            myFood.StopHandling(transform);
            myFood.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;

            user.GetComponent<PlayerHands>().Food = null;
        }
    }
}
