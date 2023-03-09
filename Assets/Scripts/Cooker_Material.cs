using UnityEngine;

public class Cooker_Material : Interection
{
    public GameObject foodPrefab;

    private bool canGive = false; // player 상태 체크

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(foodPrefab != null)
        spriteRenderer.sprite = foodPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public override void Interect(GameObject user, FoodState food = null)
    {
        if (foodPrefab == null)
        {
            Debug.Log("재료 빔");
            return;
        }

        // 플레이어 손 비면 줄 수 있음
        if (user.GetComponent<PlayerHands>().Food == null)
        {
            canGive = true;
        }

        if (canGive == false) return;

        // 음식 생성
        GameObject clone = Instantiate(foodPrefab);
        // 생성된 음식 상호작용 메소드 실행
        clone.GetComponent<FoodState>().Interect(user);

        canGive = false;
    }
}