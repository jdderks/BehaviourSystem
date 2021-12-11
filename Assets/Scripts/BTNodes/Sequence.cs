using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Similair to the Selector. But instead all children need to succeed or the whole sequence will be stopped.
/// </summary>

public class Sequence : BTBaseNode
{

    private List<BTBaseNode> children = new List<BTBaseNode>();
    public List<BTBaseNode> Children { get => children; set => children = value; }

    public Sequence(string name, List<BTBaseNode> children/*params BTBaseNode[] children*/)
    {
        this.children = children;
        this.name = name;
    }

    public override TaskStatus Run()
    {
    
        foreach(BTBaseNode node in children)
        {
            TaskStatus result = node.Run();

            switch (result)
            {
                case TaskStatus.Failed:   return TaskStatus.Failed;
                case TaskStatus.Success: break;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        return TaskStatus.Success;
    }
}



//for (; currentIndex < children.Length; currentIndex++)
//{
//    TaskStatus result = children[currentIndex].Run();

//    switch (result)
//    {
//        case TaskStatus.Failed:  currentIndex = 0; return TaskStatus.Failed;
//        case TaskStatus.Success: break;
//        case TaskStatus.Running: return TaskStatus.Running;
//    }
//}
//currentIndex = 0;
//return TaskStatus.Success;