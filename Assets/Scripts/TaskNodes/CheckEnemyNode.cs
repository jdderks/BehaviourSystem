using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyNode : BTBaseNode
{
    private float lookRange = 50f;
    private LayerMask detectLayerMask = default;
    private Transform transform;

    public CheckEnemyNode(float _range, LayerMask _mask, Transform _transform)
    {
        lookRange = _range;
        detectLayerMask = _mask;
        transform = _transform;
    }

    public override TaskStatus Run()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, lookRange, detectLayerMask);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            if (enemiesInRange[i].GetComponent<Guard>() != null)
            {
                Debug.Log(enemiesInRange[i] + "is somewhere!");
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failed;
    }
}
