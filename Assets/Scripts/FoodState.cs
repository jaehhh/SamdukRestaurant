using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 재료마다 들어가는 스크립트
// 재료마다 다른 조리시간과 조리방법 선택해줘야 함

public class FoodState : Interection
{
    // 재료 이름 역할
    public FoodType foodType; 

    public CookType[] cookType; // 재료종류마다 정해진 조리방법
    [SerializeField] private float[] cookTime; // 조리방법마다 정해진 조리시간

    public CookType cookTypeSelect { get; set; } // 선택된 조리방법
    private float cookTimeSelect; // 선택된 조리방법에 따라 정해진 조리시간

    private float currentTime;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite[] afterSprite; // 요리완료 후 이미지 목록
    private Sprite selectedAfterSprite;

    private bool isCooking = false;
    public bool IsEnded;

    private Collider2D collider; 

    [SerializeField] private Canvas cookTimeUI; // 조리시간 UI
    [SerializeField] private Image image;

    // 상태에 따른 레이어 값
    private SpriteRenderer userSprite;

    // 이용중인 조리도구
    public Cooker Cooker { get; set; }


    // ----------------- 이하 베이스그릇

    [SerializeField] private bool isBase; // 베이스그릇인지 체크
    public bool IsBase { get => isBase; private set => value = isBase; }
    private BaseDish dish;

    [HideInInspector] public Transform lastUserTransform;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        dish = GetComponent<BaseDish>();

        cookTimeUI.enabled = false;
    }

    // 조리도구가 아니면 음식의 FoodState로 바로 넘어온다
    // 음식이 바닥에 있었을 때 손이 비었으면 든다, 음식이 그릇일 경우 조합 후 플레이어손에 있는 음식 삭제
    public override void Interect(GameObject user, FoodState food = null) // GameObject user : 플레이어, FoodState food : 들고있는 음식
    {
        // 플레이어 들고있는 음식이 조합가능 상태이며 내가(FoodState) 베이스 음식일 때 
        if (food != null)
        {
            if(isBase && food.isBase == false && food.IsEnded)
            {
                // 음식 합성
                dish.Base(food);

                user.GetComponent<PlayerHands>().Food = null;

                Destroy(food.gameObject);
            }

            Debug.Log("바닥에 음식있고 손에 다르 음식 있음");
        }
        else
        {
            userSprite = user.GetComponent<SpriteRenderer>();
            // 음식 손에 들기
            Handling(user.GetComponent<PlayerHands>().handsPosition);
            // 플레이어 상태 변경
            user.GetComponent<PlayerHands>().Food = this;
            // 사용자
            lastUserTransform = user.transform;
        }
    }

    // 들면 position 변경 메소드
    public void Handling(Transform hands)
    {
        collider.enabled = false;

        StartCoroutine("UpdatePosition", hands);

        if(dish != null) // 베이스그릇일 때
        {
            dish.FoodImageOn();
        }
    }

    // 내렿놓으면 position 변경 메소드 중지
    public void StopHandling(Transform putdownP)
    {
        collider.enabled = true;
        spriteRenderer.enabled = true;

        StopCoroutine("UpdatePosition");

        transform.position = putdownP.position;
        this.spriteRenderer.sortingOrder = (int)(transform.position.y * -100 - 100);

        if (dish != null) // 베이스그릇일 때
        {
            dish.FoodImageOff();
        }
    }

    // 음식 위치값 계속 변경 루틴
    private IEnumerator UpdatePosition(Transform hands)
    {
        while(true)
        {
            transform.position = hands.position;

            spriteRenderer.sortingOrder = userSprite.sortingOrder + 1;

            yield return null;
        }
    }

    public void StartCook(CookType type)
    {
        if (IsEnded || isBase) return;

        // 요리중이면 함수 호출 계속 돼도 루틴 중복실행 안되도록 함
        if(isCooking == false)
        {
            // 요리 방법 적용
            cookTypeSelect = type;
            // 요리시작 처음이면 요리시간 설정
            if(currentTime == 0)
            {
                SetCookTime();
            }

            StartCoroutine("Cook");
            Cooker.Effect.SetActive(true); // 조리이펙트 on
            if(cookTypeSelect == CookType.boil) // 끓이기면 sprite off
            {
                Debug.Log("스트라이트 숨기기");
                spriteRenderer.enabled = false;
            }

            isCooking = true;

            // UI 
            cookTimeUI.enabled = true;
        }
    }

    public void StopCook()
    {
        StopCoroutine("Cook");
        Cooker.Effect.SetActive(false); // 조리이펙트 off
        Cooker.audio.Pause(); // 음식 조리 사운드 스탑
        spriteRenderer.enabled = true;

        isCooking = false;
    }
    
    private void SetCookTime()
    {
        // 정해진 요리방법에 따라 시간 설정
        for(int i = 0; i <cookType.Length; ++i)
        { 
            if(cookType[i] == cookTypeSelect)
            {
                cookTimeSelect = cookTime[i]; // 조리시간 설정
                selectedAfterSprite = afterSprite[i]; // 조리후 이미지 설정
            }
        }
    }

    private IEnumerator Cook()
    {
        // if 조리방법 틀리면 실패
        // 조리방법에 따라 변경될 이미지 선택

        Cooker.audio.Play(); // 조리 사운드 재생

        while (currentTime < cookTimeSelect)
        {
            currentTime += Time.deltaTime;

            image.fillAmount = currentTime / cookTimeSelect;

            yield return null;
        }

        spriteRenderer.sprite = selectedAfterSprite;

        cookTimeUI.enabled = false;
        IsEnded = true;

        Cooker.Effect.SetActive(false); // 조리이펙트 off
        spriteRenderer.enabled = true;

        Cooker.audio.Pause(); // 조리 완료되면 조리 사운드 정지
    }
}
