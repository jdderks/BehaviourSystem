using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Ally : MonoBehaviour
{
    public Transform coverPosition;

    public LayerMask enemyLayerMask;

    private BTBaseNode behaviourTree;
    private NavMeshAgent agent;
    private Animator animator;

    private VariableGameObject target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        target = (VariableGameObject)ScriptableObject.CreateInstance("VariableGameObject");
        target.Value = GameObject.FindGameObjectWithTag("Player");

        //Check if enemy exists
        CheckEnemyNode enemyExistsNode = new CheckEnemyNode(100, enemyLayerMask, transform);
        Invertor invertedEnemyExistsNode = new Invertor(enemyExistsNode);

        EnemyActiveNode enemyActiveNode = new EnemyActiveNode(100, enemyLayerMask, transform);

        //Follow player
        ChaseToTargetNode followPlayerNode = new ChaseToTargetNode(1, 100, target, agent);

        //Hide from enemy
        MoveToTransformNode moveToCover = new MoveToTransformNode(coverPosition, agent, 1);
        ThrowSmokeNode throwSmoke = new ThrowSmokeNode(enemyLayerMask, 10, gameObject);

        Sequence followPlayerSequence = new Sequence("Follow player", new List<BTBaseNode> { followPlayerNode });
        Sequence distractEnemySequence = new Sequence("Distract enemy", new List<BTBaseNode> { enemyActiveNode, moveToCover, throwSmoke });

        behaviourTree = new Selector("FollowPLayerSequence", new List<BTBaseNode> { distractEnemySequence, followPlayerSequence });

        //Display current state of tree above agent
        if (Application.isEditor)
        {
            gameObject.AddComponent<ShowNodeTreeStatus>().AddConstructor(transform, behaviourTree);
        }

    }

    private void FixedUpdate()
    {
        behaviourTree?.Run();
    }
}
