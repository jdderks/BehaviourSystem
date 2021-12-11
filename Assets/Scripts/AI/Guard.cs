using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    private BTBaseNode behaviourTree;

    [SerializeField] private WaypointManager waypointManager;
    [SerializeField] private VariableFloat walkSpeed;
    [SerializeField] private VariableBool weaponAvailable;
    [SerializeField] private GameObject weapon;

    [SerializeField] VariableGameObject target;

    public VariableBool active;
    private bool isBlinded = false;

    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private FieldOfView fov;

    public bool IsBlinded { get => isBlinded; set => isBlinded = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        fov = GetComponent<FieldOfView>();

        agent.speed = walkSpeed.Value;
    }

    private void Start()
    {
        target = (VariableGameObject)ScriptableObject.CreateInstance("VariableGameObject");
        weaponAvailable = (VariableBool)ScriptableObject.CreateInstance("VariableBool");

        weaponAvailable.Value = false;

        //Patrolbehaviour (walking from waypoint to waypoint)
        PatrolNode patrolNode = new PatrolNode(waypointManager, agent);

        //Targetvisiblenode (Checks if the target (player) is in the FOV of the guard)
        TargetVisibleNode node_TargetVisible = new TargetVisibleNode(agent.transform, target, fov);

        ChaseToTargetNode chaseNode = new ChaseToTargetNode(0.5f, 20f, target, agent);
        AttackNode attacknode = new AttackNode(0.5f, transform, target);

        Invertor node_TargetVisibleInvertor = new Invertor(node_TargetVisible);
        TargetAvailableNode node_TargetAvailable = new TargetAvailableNode(target, active);
        Invertor node_TargetAvailableInvertor = new Invertor(node_TargetAvailable);

        BoolNode weaponAvailableNode = new BoolNode(weaponAvailable);

        MoveToTransformNode moveToNode = new MoveToTransformNode(weapon.transform, agent, 2f);
        GrabWeaponNode grabWeaponNode = new GrabWeaponNode(weapon.transform, agent, 1f, weaponAvailable);

        Sequence RetrieveWeaponSequence = new Sequence("Retrieve weapon", new List<BTBaseNode> { moveToNode, grabWeaponNode });

        Selector WeaponGrabSelector = new Selector("WeaponGrabSelector", new List<BTBaseNode>() { weaponAvailableNode, RetrieveWeaponSequence });

        Sequence patrolSequence = new Sequence("Patrol Sequence", new List<BTBaseNode> { node_TargetAvailableInvertor, patrolNode, node_TargetVisibleInvertor });
        Sequence chaseSequence = new Sequence("ChaseSequence", new List<BTBaseNode> { node_TargetAvailable, WeaponGrabSelector, chaseNode, attacknode });


        behaviourTree = new Selector("GuardBehaviourTreeSelector", new List<BTBaseNode> { patrolSequence, chaseSequence });

        //Display current state of tree above agent
        if (Application.isEditor)
        {
            gameObject.AddComponent<ShowNodeTreeStatus>().AddConstructor(transform, behaviourTree);
        }
    }

    private void FixedUpdate()
    {
        if (IsBlinded) return;
        behaviourTree?.Run();

    }

    public void Blind()
    {
        IsBlinded = true;
    }
}
