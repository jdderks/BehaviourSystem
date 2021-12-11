using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVisibleNode : BTBaseNode
{
	private Transform transform;
	private VariableGameObject target;
	private FieldOfView fov;
	public TargetVisibleNode(Transform transform, VariableGameObject target, FieldOfView fov)
	{
		this.transform = transform;
		this.target = target;
		this.fov = fov;
	}
    public override TaskStatus Run()
    {
		if (fov.VisibleTargets.Count <= 0)
		{
			target.Value = null;
			Debug.Log(name + ": no targets");
			status = TaskStatus.Failed;
			return status;
		}
		else
		{
			target.Value = fov.GetNearestTarget(transform).gameObject;
			Debug.Log(name + ": target in sight");
			status = TaskStatus.Success;
			return status;
		}
	}
}
