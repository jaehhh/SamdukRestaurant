using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationChanger : MonoBehaviour
{
    private Animator anim;

    private Vector3 beforePos, nextPos;
    private Vector3 moveDirection;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        nextPos = new Vector3(transform.position.x, transform.position.y, 0);

        moveDirection = (nextPos - beforePos).normalized;

        // 주로 위쪽 이동
        if(moveDirection.y > 0.5f)
        {
            anim.SetInteger("direction", 8); // 2매개변수는 키패드 의미
        }
        // 주로 아래쪽 이동
        else if(moveDirection.y < -0.5f)
        {
            anim.SetInteger("direction", 2);
        }
        else
        {
            // 주로 오른쪽 이동
            if(moveDirection.x > 0)
            {
                anim.SetInteger("direction", 6);
            }
            // 주로 왼쪽 이동
            else if(moveDirection.x < 0)
            {
                anim.SetInteger("direction", 4);
            }
            // 이동하지 않음
            else
            {
                anim.SetInteger("direction", 0);
            }
        }

        beforePos = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
