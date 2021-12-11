using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Displays the current node that is running
/// </summary>
public class ShowNodeTreeStatus : MonoBehaviour
{
    private BTBaseNode tree;
    private Transform origin;

    public void AddConstructor(Transform origin, BTBaseNode tree)
    {
        this.origin = origin;
        this.tree = tree;
    }

    private void OnDrawGizmos()
    {
        if (tree != null)
        {
            string info = "";

            info += tree.name + "   " + tree.status.ToString();

            GUI.color = Color.black;
            Handles.Label(origin.position + Vector3.up * 4, info);
        }
    }
}