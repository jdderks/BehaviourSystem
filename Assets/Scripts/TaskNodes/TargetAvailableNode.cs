using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAvailableNode : BTBaseNode
{
	private VariableGameObject target;
    private VariableBool agentActive;

	public TargetAvailableNode(VariableGameObject _target)
	{
		target = _target;
	}
    public TargetAvailableNode(VariableGameObject _target, VariableBool _agentActive)
	{
		target = _target;
        agentActive = _agentActive;
	}

	public override TaskStatus Run()
	{
		if (target.Value == null || target.Value.gameObject.activeSelf == false)
		{
			status = TaskStatus.Failed;
            agentActive.Value = false;
			Debug.Log(name + " Target not available      " + agentActive.Value);
		}
		else
		{
			status = TaskStatus.Success;
            agentActive.Value = true;
			Debug.Log(" Succ" + agentActive.Value);
		}

		return status;
	}
}
