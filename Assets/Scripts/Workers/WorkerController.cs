using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform[] targetTransforms;
    [SerializeField] Storage workerStorage;

    public bool canMove = true;

    [SerializeField] Vector3 targetPosition;
    [SerializeField] bool isTargetReached = false;
    

    int i;
    float dis;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("leng" + targetTransforms.Length);
        GetNewTargetPos(true);
    }

    private void Update()
    {
        if (!canMove)
        {
            if(i==0)
            {
                if(workerStorage.maxCountReached())
                {
                    canMove = true;
                }
            }
            else if(i >= targetTransforms.Length - 1)
            {
                if (workerStorage.minCountReached())
                    canMove = true;
            }

            return;
        }

        if(isTargetReached)
        {
            GetNewTargetPos(false);
        }
        else
        {
            // Move the object towards the target position
            agent.SetDestination(targetPosition);

            // Check if the object has reached the target position
            dis = Vector3.Distance(transform.position, targetPosition);
            if (dis < 1f)
            {
                isTargetReached = true;
            }
        }
    }

    void GetNewTargetPos(bool _Canmove)
    {
        /*
        if (i == 0)//it reached storeroom //check wheteher it collected or not
        {
            if (workerStorage != null)
            {
                if (workerStorage.maxCountReached())
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
        }
        else if (i == targetTransforms.Length)//it reached VerificationRoom //check wheteher it Empty or not
        {
            if (workerStorage != null)
            {
                if (workerStorage.minCountReached())
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
        }*/
        canMove = _Canmove;

        targetPosition = targetTransforms[i].position;

        if (i >= targetTransforms.Length - 1)
            i = 0;
        else
            i++;

        isTargetReached = false;

    }
}
