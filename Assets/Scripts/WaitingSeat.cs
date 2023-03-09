using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  WaitingSeat : MonoBehaviour
{
    [SerializeField] private SeatState seatState;

    [SerializeField] private Transform[] waitingSeat; // 대기좌석
    private GameObject[] NPC; // 대기하는 NPC

    private void Awake()
    {
        NPC = new GameObject[waitingSeat.Length];
    }

    public Transform SetNPC(GameObject npc)
    {
        for (int i = 0; i < NPC.Length; i++)
        {
            if(NPC[i] == null)
            {
                NPC[i] = npc;

                return waitingSeat[i];
            }
        }

        Debug.Log("대기좌석 꽉참");
        return null;
    }

    private void Update()
    {
        // 카운터가 비어있는지 체크
        if(seatState.isEmpty)
        {
            // 카운터 비면 제일 앞 손님 카운터로
            if (NPC[0] != null)
            {
                NPC[0].GetComponent<NPCController>().GoToCounter();

                NPC[0] = null;

                // 한칸씩 땡기기
                for (int i = 1; i < NPC.Length; i++)
                {
                    if (NPC[i] != null)
                    {
                        NPC[i].GetComponent<NPCController>().MoveWaitingSeat(waitingSeat[i - 1]);

                        NPC[i - 1] = NPC[i];

                        NPC[i] = null;
                    }
                }
            }
        }
    }
}
