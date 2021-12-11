using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmokeNode : BTBaseNode
{
    private LayerMask enemyMask = default;
    private float range = 10;
    private GameObject self;

    private GameObject enemyObject = default;


    public ThrowSmokeNode(LayerMask enemy, float _range, GameObject _self)
    {
        enemyMask = enemy;
        range = _range;
        self = _self;
    }

    public override TaskStatus Run()
    {
        if (enemyObject == null)
        {
            enemyObject = GetEnemy();
        }


        if (enemyObject != null)
        {
            if (Vector3.Distance(enemyObject.transform.position, self.transform.position) < range)
            {
                enemyObject.GetComponent<Guard>().Blind();
                Debug.Log(enemyObject + " is: " + enemyObject.GetComponent<Guard>().IsBlinded);
            }
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private GameObject GetEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(self.transform.position, 100f, enemyMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Guard>().IsBlinded == false)
            {
                Debug.LogWarning("SMOKE HSA BEEN THORWN");
                return enemies[i].gameObject;
            }
        }
        return null;
    }


}
