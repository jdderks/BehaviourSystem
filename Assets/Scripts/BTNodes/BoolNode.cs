using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNode : BTBaseNode
{
    private VariableBool value;

    public BoolNode(VariableBool value)
    {
        this.value = value;
    }
    public override TaskStatus Run()
    {
        if (value.Value == true)
        {
            status = TaskStatus.Success;
            return status;
        }
        else
        {
            status = TaskStatus.Failed;
            return status;
        }
    }
}
