using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    private UserData userData;
    [SerializeField] private MenuCameraController menuCameraController;
    [SerializeField] private Transform[] stage;
    public Transform[] Stage { get => stage; }

    private Animator animator;

    public int currentStage { get; set; } // 캐릭터 위치

    private void Awake()
    {
        userData = GameObject.FindGameObjectWithTag("DD").GetComponent<UserData>();
        currentStage = userData.SelectedStage;
        menuCameraController.Setup(currentStage);

        animator = GetComponent<Animator>();
        StartAnimation();

        transform.position = stage[currentStage].position + Vector3.up * 1.5f;
    }

    private void StartAnimation()
    {
        animator.Play("Player_Move_Right");
    }

    public void MoveWithMouse(Stage stage)
    {
        transform.position = stage.transform.position + Vector3.up * 1.5f;
    }

    public void MoveWithKeyBoard(int value)
    {
        currentStage += value;

        if (currentStage == -1)
        {
            currentStage = 0;
        }
        else if(currentStage == stage.Length)
        {
            currentStage = stage.Length - 1;
        }

        transform.position = stage[currentStage].position + Vector3.up * 1.5f;

        menuCameraController.MoveWithKeyBoard(currentStage);
    }
}
