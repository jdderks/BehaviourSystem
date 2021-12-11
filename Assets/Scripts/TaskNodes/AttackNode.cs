using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : BTBaseNode
{
    private float attackRange = 1f;
    private Transform transform;
    private VariableGameObject target;

    public AttackNode(float range, Transform agentTransform, VariableGameObject targetObject)
    {
        attackRange = range;
        transform = agentTransform;
        target = targetObject;
    }


    public override TaskStatus Run()
    {
        Collider[] hittableObjectsInRange = Physics.OverlapSphere(transform.position, attackRange);
        if (hittableObjectsInRange.Length < 1)
        {
            return TaskStatus.Failed;
        }

        foreach (Collider c in hittableObjectsInRange)
        {
            if (c.GetComponent<IDamageable>() != null)
            {
                Debug.Log("bonk");
                c.GetComponent<IDamageable>().TakeDamage(transform.gameObject, 1);
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Running;
    }
}
