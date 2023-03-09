using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker: Interection
{
    [HideInInspector] public AudioSource audio;

    public GameObject Effect; // 조리 이펙트

    [SerializeField] private CookType type = CookType.mince; // 조리타입

    private FoodState food;

    [SerializeField] private Vector3 foodPos; // 음식 위치 조정

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public override void Interect(GameObject user, FoodState food)
    {
        // 요리중인 음식 있고 플레이어손에 음식 없을 때 : 음식 상태 확인 후 계속 조리 or 픽업
        if(this.food != null && food == null)
        {
            if(this.food.IsEnded)
            {
                this.food.Interect(user); // 음식픽업
                //Debug.Log("조리도구 : 요리 픽업");
                this.food.Cooker = null; // 요리의 조리도구상태 null

                this.food = null;
            }
            else
            {
                this.food.StartCook(type); // 조리재개
                user.GetComponent<PlayerController>().CanMove = false;

               // Debug.Log("조리도구 : 조리 재개");
            }
        }
        // 요리중인 음식 없고 플레이어손 음식 없음
        else if (this.food == null && food == null)
        {
            //Debug.Log("조리도구 : 음식없음, 요리중 없음");
        }
        // 요리중인 음식 있고 플레이어손에 음식 있을 떄
        else if (this.food != null && food != null)
        {
            //Debug.Log("조리도구 : 손을 비우세요");
        }
        // 요리중인 음식이 없고 플레이어 손에 음식이 있을 때
        else
        {
            //Debug.Log("조리도구 : 요리중음식 없음 들고있는음식 있음");

            // 조리 방법 맞는지 확인
            for (int i = 0; i < food.cookType.Length; i++)
            {
                if(CheckCookType(food.cookType[i]))
                {
                   // Debug.Log("조리방법 맞음");

                    // 조리도구와 음식 링크
                    this.food = food;

                    // 음식 내려놓음
                    user.GetComponent<PlayerHands>().Food = null;
                    food.StopHandling(this.transform);
                    food.transform.position += foodPos;
                    food.Cooker = this; // 요리에 조리도구상태 this
                    // 레이어정리
                    SpriteRenderer mySprite = GetComponent<SpriteRenderer>(); // SpriteRenderer 찾음
                    if(mySprite == null) mySprite  =transform.GetComponentInParent<SpriteRenderer>(); // 없으면 부모객체에서 찾음
                    food.GetComponentInChildren<SpriteRenderer>().sortingOrder = mySprite.sortingOrder + 1;

                    food.StartCook(type);
                    user.GetComponent<PlayerController>().CanMove = false;

                    break;
                }
            }
        }
    }

    public override void StopInterection(GameObject user)
    {
        if(food != null)
        {
            user.GetComponent<PlayerController>().CanMove = true;

            if(type != CookType.boil)
            {
                food.StopCook();
            }
        }
    }

    private bool CheckCookType(CookType type)
    {
        if (this.type == type)
            return true;

        else
            return false;
    }
}