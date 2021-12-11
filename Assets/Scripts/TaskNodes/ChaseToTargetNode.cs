using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseToTargetNode : BTBaseNode
{
    private float minDis = 0.5f; //Minimum distance it chases
    private float maxDis = 25f; //Maximum distance it chases

    private NavMeshAgent agent;
    private VariableGameObject target;

    public ChaseToTargetNode(float minimumDis, float maximumDis, VariableGameObject targetObject, NavMeshAgent navAgent)
    {
        minDis = minimumDis;
        maxDis = maximumDis;
        target = targetObject;
        agent = navAgent;

        navAgent.stoppingDistance = minimumDis;
    }

    public override TaskStatus Run()
    {
        if (target != null)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogError(name + ": Agent has no valid path");
                return TaskStatus.Failed;
            }

            float distanceToTarget = Vector3.Distance(agent.transform.position, target.Value.transform.position);
            Debug.Log("Agent is now chasing " + target.Value.name + "!");
            agent.SetDestination(target.Value.transform.position);

            if (distanceToTarget > maxDis)
            {
                agent.SetDestination(Vector3.zero);
                return TaskStatus.Failed;
            }

            if (distanceToTarget < minDis)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
        return TaskStatus.Failed;
    }
}
