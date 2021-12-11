using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { Success, Failed, Running }
public abstract class BTBaseNode
{
    public string name;

    public abstract TaskStatus Run();

    public TaskStatus status { get; set; }
}
