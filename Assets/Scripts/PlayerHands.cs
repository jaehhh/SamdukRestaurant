using UnityEngine;
using System;

/* 
 * 현재 PlayerHands는 "음식을 손에 든다"라는 판정을 할 때
 * 음식 오브젝트에 있는 FoodState컴포넌트를 스크립트 내부 클래스변수에 저장하고
 * 음식 오브젝트 FoodState의 내부 함수에 플레이어위치 매개변수를 전달하고 이동 코루틴을 호출하여 음식을 든 것처럼 한다
 * 
 * 따라서 베이스그릇도 음식 상태는 없지만 FoodState가 필요함
 */

public class PlayerHands : MonoBehaviour
{
    // 중첩 상호작용물체 여부 체크
    private bool checkOtherTrigger = false;

    // 상호작용 물체
    public Interection Other;
    // 들고 있는 물체
    public FoodState Food;

    // 음식 상태 변경 값
    public Transform handsPosition;
    [SerializeField] private Transform footPosition;

    private AnimatorChanger anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<AnimatorChanger>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Food == null)
        {
            anim.AnimationIsHandling(false);
        }
        else
        {
            anim.AnimationIsHandling(true);
        }
    }

    public void Interect()
    {
        // 상호작용 물체 있고, 음식 들고 있을 때
        if(Other != null && Food != null)
        {
            // 음식 합치기 시도, 조리시도 : 실패
            Other.Interect(this.gameObject, Food);

            audioSource.Play();

        // Debug.Log("플레이어 : 조리도구와 음식간의 상호작용, 출동물체가 바닥음식일 경우");
        }

        // 상호작용 물체 있고, 음식 없을 때 : 바닥음식 들거나, 조리도구에 있는 음식조리
        else if (Other != null && Food == null)
        {
            Other.Interect(this.gameObject, Food);

            audioSource.Play();

            //  Debug.Log("플레이어 : 음식 들거나 이어서 음식조리함");
        }

        // 상호작용 물체 없고, 음식 들고 있을 때 : 바닥에 음식 내림
        else if (Other == null && Food != null)
        {
            Food.StopHandling(footPosition);

            Food = null;

            audioSource.Play();

            //  Debug.Log("플레이어 : 음식 내려놓음");
        }

        // 상호작용 물체 없고, 음식 없을 때
        else if (Other == null && Food == null)
        {
         //   Debug.Log("플레이어 : 아무 것도 하지 않음");
        }
    }

    public void StopInterection()
    {
        if (Other != null)

        Other.StopInterection(this.gameObject);
    }

    // 상호작용 오브젝트에 닿으면 서로를 링크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InterectionObject"))
        {
            Other = collision.GetComponent<Interection>();
        }
    }

    // player가 상호작용오브젝트를 벗어났을 때 또다른 상호작용물체가 있는지 판단
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (checkOtherTrigger == false) return; // player가 상호작용오브젝트를 벗어나지 않으면 stay2D실행하지 않음

        if (collision.CompareTag("InterectionObject"))
        {
            Other = collision.GetComponent<Interection>();
        }

        checkOtherTrigger = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InterectionObject"))
        {
            // Other.SetUser(null);

            Other = null;

            checkOtherTrigger = true; // 상호작용중인 또다른 오브젝트가 있는지 확인
        }
    }
}
