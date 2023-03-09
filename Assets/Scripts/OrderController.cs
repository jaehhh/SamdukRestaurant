using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    private List<GameObject> orderList = new List<GameObject>();

    private Recipe recipe; // 레시피를 읽고 필요한 재료 정보 획득

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject orderUIPrefab; // 주문서 UI 프리팹
    private Sprite doneFood; // 완성음식 이미지
    private Sprite[] matFood = new Sprite[3]; // 재료 이미지
    private int waitTime; // 남은 시간

    [SerializeField] private Transform[] orderPos;

    private StageController stageController;

    private int orderNumber = 1;

    private void Awake()
    {
        recipe = GameObject.FindGameObjectWithTag("Table").GetComponent<Recipe>();
        stageController = GetComponent<StageController>();
    }

    // 손님으로부터 호출되는 주문서 생성 함수
    public int CreateOrder(int needFood, int waitTime)
    {
        // 초기화
        for (int i = 0; i < matFood.Length; i++)
            matFood[i] = null;

        // 레시피 읽고 이미지 가져옴
        recipe.GetFoodInfo(needFood, out matFood, out doneFood);

        // 주문서 생성
        GameObject cloneUI = Instantiate(orderUIPrefab);
        // 리스트에 추가
        orderList.Add(cloneUI);
        // 주문서를 캔버스의 자식으로 지정
        cloneUI.transform.SetParent(canvas.transform);
        // 주문서 위치 조정
        MoveUI();
        // 주문서 내부 스크립트에 변수 전달
        cloneUI.GetComponent<OrderUI>().Setup(doneFood, matFood, waitTime, orderNumber);

        if (stageController.isGameOver)
        {
            cloneUI.SetActive(false);
        }

            return orderNumber++;
    }

    public void DestroyOrder(int orderNumber)
    {
        for(int i = 0; i < orderList.Count; i ++)
        {
            // 주문서 번호랑 같은 주문서 삭제
            if(orderList[i].GetComponent<OrderUI>().orderNumber == orderNumber)
            {
                GameObject obj = orderList[i];

                orderList.RemoveAt(i);
                Destroy(obj.gameObject);

                MoveUI();

                return;
            }
        }
    }

    public void DestroyAllOreder()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            Destroy(orderList[i].gameObject);
        }
        orderList.Clear();
    }

    private void MoveUI()
    {
        for (int i = 0; i < orderList.Count; i++)
            orderList[i].transform.position = orderPos[i].position;
    }
}
