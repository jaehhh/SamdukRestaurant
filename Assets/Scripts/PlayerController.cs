using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerHands playerHands;
    private Movement movement;

    float x, y;

    public bool CanMove { get; set; }

    private AnimatorChanger anim;
    private bool moveTrigger;

    private void Awake()
    {
        playerHands = GetComponent<PlayerHands>();
        movement = GetComponent<Movement>();

        anim = GetComponent<AnimatorChanger>();

        CanMove = false;
    }

    private void Start()
    {
        moveTrigger = true;
    }

    private void Update()
    {
        // 이동
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        if(CanMove)
        {
                movement.MoveTo(direction.normalized);
        }
        else
        {
            movement.MoveTo(Vector3.zero);
        }

        // 정지하면 애니메이션 교체
        if(x == 0 && y == 0)
        {
            if (moveTrigger)
            {
                StartCoroutine("Stay");

                moveTrigger = false;
            }
            
        }

        anim.AnimationXY((int)x, (int)y);

        // 상호작용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHands.Interect();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerHands.StopInterection();
        }
    }

    private IEnumerator Stay()
    {
        anim.AnimationMove(0);

        while(x == 0 && y == 0)
        {
            yield return null;
        }

        anim.AnimationMove(1);

        moveTrigger = true;
    }
}
