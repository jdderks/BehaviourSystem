using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	//Parent object of the waypoint objects in the scene (empty object transforms)
	[SerializeField] private Transform waypointsParent;

	//List of empty gameobject transforms
	[SerializeField] private List<Transform> waypoints = new List<Transform>();

	public Transform WaypointsParent { get => waypointsParent; set => waypointsParent = value; }
	public List<Transform> Waypoints { get => waypoints; set => waypoints = value; }

	private void Awake()
	{
		GetWaypointsFromParent();
	}

	private void GetWaypointsFromParent()
	{
		waypoints.Clear();
		for (int i = 0; i < waypointsParent.transform.childCount; i++)
		{
			waypoints.Add(waypointsParent.transform.GetChild(i));
		}
	}
	public Transform GetBaseWayPoint()
    {
		return Waypoints[0];
    }
}
