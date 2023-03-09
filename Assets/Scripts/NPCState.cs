using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCStates {GoToTable, Wait, Eat, GoToCounter, Pay, GoToExit, WaitingSeat};

public class NPCState : MonoBehaviour
{
    private Recipe recipe;
    private OrderController orderController;
    private NPCController controller;

    [SerializeField] private Image foodUI;
    [SerializeField] private Image parentFoodUI;

    private int orderNumber; // 주문서 번호

    public NPCStates myState = NPCStates.GoToTable; // 상태

    public int needFood; // 원하는 음식 코드, 레시피 등록 순서

    private float maxWaitTime = 30; // 기다리는 시간
    private float waitTime = 0; // 기다린 시간
    private float maxPayTime = 25; // 지불 기다리는 시간
    private int eatTimeMax = 13; // 먹는 시간


    private void Awake()
    {
        recipe = GameObject.FindGameObjectWithTag("Table").GetComponent<Recipe>();
        orderController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OrderController>();
        controller = GetComponent<NPCController>();
    }

    // 목적지에 도착하면 하는 행동
    public void Do()
    {
        switch(myState)
        {
            case NPCStates.GoToTable:
                {
                    orderNumber = orderController.CreateOrder(needFood, (int)maxWaitTime); // 주문서생성

                    parentFoodUI.gameObject.SetActive(true); // 원하는 음식 UI
                    foodUI.sprite = recipe.GetFoodImage(needFood); // 원하는음식UI
                    SetTable(); // 테이블동기화

                    break;
                }
            case NPCStates.GoToCounter:
                {
                    Pay();

                    break;
                }
            case NPCStates.GoToExit:
                {
                    Destroy(gameObject);

                    break;
                }
            case NPCStates.WaitingSeat:
                {
                    break;
                }
        }
    }

    private void SetTable()
    {
        // 테이블에 자신의 정보를 제공
        controller.selectedDes.gameObject.GetComponentInParent<SeatState>().SetTable(this);

        StartCoroutine("Timer", maxWaitTime);

        myState = NPCStates.Wait;
    }

    // 카운터에 자신의 정보를 제공
    private void Pay()
    {
        controller.selectedDes.gameObject.GetComponentInParent<SeatState>().SetTable(this);

        myState = NPCStates.Pay;

        waitTime = 0;
        StartCoroutine("Timer",maxPayTime);
    }

    private IEnumerator Timer(int maxTime)
    {
        while(true)
        {
            waitTime += Time.deltaTime;

            if (waitTime >= maxTime)
            {
                parentFoodUI.gameObject.SetActive(false);

                controller.GoToExit();

                break;
            }

            yield return null;
        }
    }

    // 테이블에 원하는 음식이 놓여져있으면 식사
    public void Eat()
    {
        parentFoodUI.gameObject.SetActive(false);

        myState = NPCStates.Eat;

        StopCoroutine("Timer");

        StartCoroutine("Eating");

        RemoveOrder();
    }

    private IEnumerator Eating()
    {
        float eatCurrentTime = 0;

        while(eatCurrentTime < eatTimeMax)
        {
            eatCurrentTime += Time.deltaTime;

            yield return null;
        }

        // 식탁 음식 삭제
        Destroy(controller.selectedDes.gameObject.GetComponentInParent<SeatState>().food.gameObject);

        controller.GoToCounter();
    }

    public void RemoveOrder()
    {
        orderController.DestroyOrder(orderNumber);
    }
}
