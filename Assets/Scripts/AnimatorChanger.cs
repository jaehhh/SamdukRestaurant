using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction { front, back, left, right};

public class AnimatorChanger : MonoBehaviour
{
    private PlayerHands playerHands;
    private Animator animator;
    [SerializeField] private Transform dish;

    // private Vector3 dishFront = new Vector3(0, -.6f, 0), dishLeft = new Vector3(-0.6f, - 0.5f,0), dishRight = new Vector3(0.6f, -0.5f, 0);

    private void Awake()
    {
        playerHands = GetComponent<PlayerHands>();
        animator = GetComponent<Animator>();
    }

    public void AnimationXY(int x, int y)
    {
        animator.SetInteger("X", x);
        animator.SetInteger("Y", y);
    }

    public void AnimationIsHandling(bool handling)
    {
        animator.SetBool("isHandling", handling);
    }

    public void AnimationMove(int state)
    {
        // Stop Move : 0
        // Start Move : 1

        if(state == 0)
        {
            animator.SetTrigger("stopMove");
        }
        else
        {
            animator.SetTrigger("startMove");
        }
    }

    public void DishDirection(direction direction)
    {
        switch (direction)
        {
            case direction.front:
                {
                    playerHands.Food.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    //dish.localPosition = dishFront;
                    break;
                }
            case direction.back:
                {
                    playerHands.Food.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                }
            case direction.left:
                {
                    playerHands.Food.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    //dish.localPosition = dishLeft;
                    break;
                }
            case direction.right:
                {
                    playerHands.Food.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    //dish.localPosition = dishRight;
                    break;
                }
        }
    }
}
