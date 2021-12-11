using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : BTBaseNode
{
    private float currentTime;
    private float waitTime = 0f;
    public WaitNode(float _waitTime)
    {
        waitTime = _waitTime;
    }
    public override TaskStatus Run()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            currentTime = 0f;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
