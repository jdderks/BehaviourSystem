using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will continue once one node returns failed
/// </summary>
public class Selector : BTBaseNode
{
    private List<BTBaseNode> children = new List<BTBaseNode>();
    public List<BTBaseNode> Children { get => children; set => children = value; }

    public Selector(string name, List<BTBaseNode> children)
    {
        this.children = children;
        this.name = name;
    }

    public override TaskStatus Run()
    {

        foreach (BTBaseNode node in children)
        {
            TaskStatus result = node.Run();

            switch (result)
            {
                case TaskStatus.Success: return TaskStatus.Success;
                case TaskStatus.Failed:  continue;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        return TaskStatus.Failed;
    }
}
