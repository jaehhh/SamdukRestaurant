using UnityEngine;

public class SeatState : Interection
{
    private GameObject table; // 레벨테이블
    [SerializeField] private GameObject gameController;

   [HideInInspector] public bool isEmpty = true;

   [HideInInspector]  public int needFood;

    private NPCState npc;
    public FoodState food;

    [Header("이펙트출력")]
    [SerializeField] private GameObject effect;
    [SerializeField] private Transform effectPos;

    private void Awake()
    {
        table = GameObject.FindGameObjectWithTag("Table");
    }

    public void SetTable(NPCState npc)
    {
        this.npc = npc;

        needFood = npc.needFood;
    }

    public void LeaveTable()
    {
        Debug.Log($"LeaveTable() : {this.name} 초기화 ( npc = null 등)");

        isEmpty = true;

        npc = null;
        needFood = -1;
    }

    public override void Interect(GameObject user, FoodState food = null)
    {
        if(npc == null)
        {
            Debug.Log("npc없음");
            return;
        }
        else
        {
            Debug.Log(npc.name);
        }

        if(food != null && npc.myState == NPCStates.Wait)
        {
            if(food.IsBase)
            {
                if (food.GetComponent<BaseDish>().CompleteFoodCode == needFood)
                {
                    this.food = food;

                    food.StopHandling(transform);
                    food.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponentInChildren<SpriteRenderer>().sortingOrder + 1; // 레이어정리
                    food.GetComponent<Transform>().position += new Vector3(0, 0.7f);

                    food.GetComponent<Collider2D>().enabled = false;

                    user.GetComponent<PlayerHands>().Food = null; // 유저 손 비우기


                    Vector3 pos = transform.position + Vector3.up * 2;
                    Instantiate(effect, pos, Quaternion.identity); // 하트 이펙트

                    npc.Eat();
                }
            }       
        }
        else if(npc.myState == NPCStates.Pay)
        {
            int price = table.GetComponent<Recipe>().GetPrice(needFood); 

            gameController.GetComponent<CoinController>().GetCoin(price);

            Instantiate(effect, effectPos.position, Quaternion.identity); // 코인이펙트

            npc.GetComponent<NPCController>().GoToExit();

            npc = null;
        }
    }
}
