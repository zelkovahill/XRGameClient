using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ECustomerState
{
    Idle,
    WalkingToShelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem
}

public class Timer
{
    private float timeRemaining;

    public void Set(float time)
    {
        timeRemaining = time;
    }

    public void Update(float deltaTime)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= deltaTime;
        }
    }

    public bool IsFinished()
    {
        return timeRemaining <= 0;
    }
}

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerFSM : MonoBehaviour
{
    [Header("상태")]
    public ECustomerState currentState;
    public Timer timer;
    private NavMeshAgent agent;
    public bool isMoveDone = false;

    [Space(10)]

    [Header("이동할 목표 위치")]
    public Transform target;        // 이동할 목표 위치

    [Space(10)]

    [Header("장소")] // 장소
    public Transform counter;
    public List<GameObject> targetPos = new List<GameObject>();
    public List<GameObject> myBox = new List<GameObject>();
    [Space(10)]

    private static int nextPriority = 0;    // 다음 에이전트의 우선 순위
    private static readonly object priorityLock = new object();     // 우선 순위 할당을 위한 동기화 객체

    public int boxesToPick = 5;
    private int boxesPicked = 0;


    void Start()
    {
        timer = new Timer();
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
        currentState = ECustomerState.Idle;
    }

    private void AssignPriority()
    {
        lock (priorityLock)  // 동기화 블록을 사용하여 우선 순위를 할당
        {
            agent.avoidancePriority = nextPriority;
            nextPriority = (nextPriority + 1) % 100;    // NavMeshAgent 우선 순위 범위는 0 ~ 99
        }
    }

    private void MoveToTarget()
    {
        isMoveDone = false;

        if (target != null)
        {
            agent.SetDestination(target.position);  // agent에 목적지 타겟 설정
        }
    }


    void Update()
    {
        timer.Update(Time.deltaTime);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;

            }
        }

        switch (currentState)
        {
            case ECustomerState.Idle:
                Idle();
                break;
            case ECustomerState.WalkingToShelf:
                WalkingToShelf();
                break;
            case ECustomerState.PickingItem:
                PickingItem();
                break;
            case ECustomerState.WalkingToCounter:
                WalkingToCounter();
                break;
            case ECustomerState.PlacingItem:
                PlacingItem();
                break;
        }
    }



    private void ChangeState(ECustomerState nextState, float waitTime = 0.0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }

    private void Idle()
    {
        if (timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            
            ChangeState(ECustomerState.WalkingToShelf, 2.0f);
        }
    }

    private void WalkingToShelf()
    {
        if (timer.IsFinished() && isMoveDone) 
        {
            ChangeState(ECustomerState.PickingItem, 2.0f);
        }
    }

    private void PickingItem()
    {
        if (timer.IsFinished())
        {
            if (boxesPicked < boxesToPick)
            {
                print("피킹");
                // 상자 생성
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);    // 다음 상자 생성 까지 대기 시간 설정
            }
            else
            {
                target = counter;
                MoveToTarget();
                ChangeState(ECustomerState.WalkingToCounter, 2.0f);
            }
        }
    }

    private void WalkingToCounter()
    {
        if (timer.IsFinished()&& isMoveDone)
        {
            ChangeState(ECustomerState.PlacingItem, 2.0f);
        }
    }

    private void PlacingItem()
    {
        if (timer.IsFinished())
        {
            if(myBox.Count !=0)
            {
                myBox[0].transform.position = counter.transform.position;
                myBox[0].transform.parent = counter.transform;
                myBox.RemoveAt(0);

                timer.Set(1.0f);
            }
            else
            {
                ChangeState(ECustomerState.Idle, 2.0f);
            }
         
        }
    }



}
