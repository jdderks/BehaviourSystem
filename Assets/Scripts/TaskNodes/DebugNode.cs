using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : BTBaseNode
{
    private string debugText;

    public DebugNode(string _debugText)
    {
        debugText = _debugText;
    }

    public override TaskStatus Run()
    {
        Debug.Log(debugText);
        return TaskStatus.Success;
    }
}
