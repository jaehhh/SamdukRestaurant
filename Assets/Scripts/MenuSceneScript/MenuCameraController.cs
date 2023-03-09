using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private Transform[] camPos;

    private int current;
    private Vector3 vel;

    public void Setup(int stage)
    {
        int num = stage / 4;
        transform.position = camPos[num].position;
        current = num;
    }

    public void MoveWithKeyBoard(int stage)
    {
        // 목적지 설정
        int num = stage / 4;

        if(current != num)
        {
            Transform direction = camPos[num];

            StopCoroutine("Move");
            StartCoroutine("Move", direction);

            current = num;
        }
    }

    public void MoveWithMouse(int value)
    {
        int dir = current + value;

        if (dir < 0 || dir >= camPos.Length) return; 

        Transform direction = camPos[dir];

        StopCoroutine("Move");
        StartCoroutine("Move", direction);

        current = dir;
    }

    private IEnumerator Move(Transform direction)
    {
        while (Mathf.Abs(transform.position.x - direction.position.x) > 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, direction.position, ref vel, 0.1f);

            yield return null;
        }

        transform.position = direction.position;
    }
}
