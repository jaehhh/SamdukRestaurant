using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    

    private NPCState state;
    private NavMeshAgent agent;

    // 위치
    private GameObject[] seat;
    private GameObject counter;
    private GameObject exit;
    private GameObject waitingSeat;

    public Transform selectedDes;

    [SerializeField] private float distance = 0;
    [SerializeField] private float reachDistance = 1.4f;

    private void Awake()
    {
        state = GetComponent<NPCState>();

        seat = GameObject.FindGameObjectsWithTag("Seat");
        counter = GameObject.FindGameObjectWithTag("Counter");
        exit = GameObject.FindGameObjectWithTag("Exit");
        waitingSeat = GameObject.FindGameObjectWithTag("WaitingSeat");
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(SelectSeat())
        {
            StartCoroutine("MoveTo", selectedDes);
        }
    }

    public void GoToCounter()
    {
        if(selectedDes.GetComponentInParent<SeatState>() != null) // 변경전 selectedDes는 머물던 위치이므로, 대기좌석>카운터 경우 대기좌석에는 SeatState가 없으므로 selectedDes는 null임
        selectedDes.GetComponentInParent<SeatState>().LeaveTable();

        // 카운터에 이미 사람 있으면 대기좌석으로
        if (counter.GetComponentInParent<SeatState>().isEmpty == false)
        {
            selectedDes = waitingSeat.GetComponent<WaitingSeat>().SetNPC(this.gameObject);

            // 대기좌석 있으면
            if (selectedDes != null)
            {
                state.myState = NPCStates.WaitingSeat;

                StartCoroutine("MoveTo", selectedDes);
            }
            // 대기좌석 꽉차면 즉시 나감
            else
            {
                GoToExit();
            }
        }
        // 카운터에 사람 없으면 카운터로 이동
        else
        {
            counter.GetComponentInParent<SeatState>().isEmpty = false;
            selectedDes = counter.transform;
            state.myState = NPCStates.GoToCounter;

            StopCoroutine("MoveTo");
            StartCoroutine("MoveTo", selectedDes);
        }
    }

    // 대기좌석 간 이동
    public void MoveWaitingSeat(Transform nextSeat)
    {
        selectedDes = nextSeat;
        state.myState = NPCStates.WaitingSeat;

        StopCoroutine("MoveTo");
        StartCoroutine("MoveTo", selectedDes);
    }

    public void GoToExit()
    {
        state.RemoveOrder();

        Debug.Log("GoToExit()");

        if (selectedDes.GetComponentInParent<SeatState>() != null)
        {
            selectedDes.GetComponentInParent<SeatState>().LeaveTable();
            Debug.Log( $"NPC가 {selectedDes.name}의 <SeatState>().LeaveTable() 호출");
        }
        else
        {
            Debug.Log($"NPC의 {selectedDes.name}에 SeatState없음");
        }

        selectedDes = exit.transform;
        state.myState = NPCStates.GoToExit;

        StartCoroutine("MoveTo", selectedDes);
    }

    private bool SelectSeat()
    {
        if(CheckEmpty())
        {
            Debug.Log("좌서 꽉차서 생성된 NPC 삭제");
            Destroy(gameObject);

            return false;
        }

        int select = Random.Range(0, seat.Length);

        while (seat[select].GetComponentInParent<SeatState>().isEmpty == false)
        {
            select = Random.Range(0, seat.Length);
        }

        seat[select].GetComponentInParent<SeatState>().isEmpty = false;

        selectedDes = seat[select].transform;

        return true;
    }

    private bool CheckEmpty()
    {
        int fullCount = 0;

        for (int i = 0; i < seat.Length; ++i)
        {
            if (seat[i].GetComponentInParent<SeatState>().isEmpty == false)
            {
                fullCount++;
            }
        }

        if (fullCount == seat.Length)
        {
            return true;
        }
        else return false;
    }

    private IEnumerator MoveTo(Transform destination)
    {
        while (true)
        {
            agent.SetDestination(destination.position);

            distance = Vector3.Distance(transform.position, destination.position);

            if(distance < reachDistance)
            {
                transform.position = destination.position;

                Debug.Log("NPC이동 끝남");

                break;
            }

            yield return null;
        }

        state.Do();
    }
}
