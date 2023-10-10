using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform[] targetTransforms;

    public bool canMove = true;

    [SerializeField] Vector3 targetPosition;
    [SerializeField] bool isTargetReached = false;
    

    int i;
    float dis;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNewTargetPos();
    }

    private void Update()
    {
        if (!canMove)
            return;

        if(isTargetReached)
        {
            GetNewTargetPos();
        }
        else
        {
            // Move the object towards the target position
            agent.SetDestination(targetPosition);

            // Check if the object has reached the target position
            dis = Vector3.Distance(transform.position, targetPosition);
            if (dis < 0.4f)
            {
                isTargetReached = true;
            }
        }
    }

    void GetNewTargetPos()
    {
        targetPosition = targetTransforms[i].position;

        if (i >= targetTransforms.Length-1)
            i = 0;
        else
            i++;

        isTargetReached = false;
    }
}
