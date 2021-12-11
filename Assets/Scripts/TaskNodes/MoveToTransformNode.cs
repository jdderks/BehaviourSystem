using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTransformNode : BTBaseNode
{
    private Transform target;
    private NavMeshAgent navAgent;
    private float stopDistance;

    public MoveToTransformNode(Transform targetTransform, NavMeshAgent agent, float dis)
    {
        this.target = targetTransform;
        this.navAgent = agent;
        this.stopDistance = dis;
    }


    public override TaskStatus Run()
    {
        navAgent.SetDestination(target.position);
        if (Vector3.Distance(navAgent.transform.position, target.position) <= stopDistance)
        {
            status = TaskStatus.Success;
            return status;
        }

        status = TaskStatus.Running;
        return status;
    }
}
