using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMachine : MonoBehaviour
{
    private StateMachine<DroneMachine> stateMachine;
    public StateMachine<DroneMachine> pStateMachine
    {
        get { return stateMachine; }
    }

    private NavMeshAgent navMeshAgent;
    public NavMeshAgent pNavMeshAgent
    {
        get { return navMeshAgent; }
    }

    public float timer { get; set; }

    [Header("IDLE")]
    public float stayIdleTime;

    [Header("ALERT")]
    public float maxDistanceToAlert = 5.0f;
    public float rotateSpeedAlert = 10f;
    public float startRotation;

    [Header("ATTACK")]
    public float minDistanceToAttack = 3.0f;
    public float maxDistanceToAttack = 7.0f;

    [Header("PATROL")]
    public List<Transform> waypoints;
    [HideInInspector] public int currentWaypointID = 0;

    [Header("CHASE")]
    public float maxDistanceToRaycast = 15f;
    public float coneAngle = 60f;
    public LayerMask sightLayerMask;

    [Header("REFERENCES")]
    public FPS_CharacterController player;
    public Transform eyes;

    [Header("HEALTH")]
    public int maxHP = 100;
    private int currentHP;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentHP = maxHP;

        stateMachine = new StateMachine<DroneMachine>(this);
        stateMachine.ChangeState(DroneIdleState.Instance);
    }

    private void Update()
    {
        print(stateMachine.CurrentState());

        stateMachine.UpdateMachine();
    }

    public bool SeesPlayer()
    {
        Vector3 direction = (player.transform.position + Vector3.up * 1.5f) - eyes.position;
        float distanceToPlayer = direction.magnitude;
        direction /= distanceToPlayer;

        bool isOnCone = Vector3.Dot(transform.forward, direction) >= Mathf.Cos(coneAngle * Mathf.Deg2Rad * 0.5f);

        if (isOnCone && !Physics.Linecast(eyes.position, player.transform.position + Vector3.up * 1.5f, sightLayerMask.value))
        {
            return true;
        }

        return false;
    }

    public bool HearsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < maxDistanceToAlert;
    }

    public bool IsInAttackDistance()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < maxDistanceToAttack;
    }
}


/*
 * https://forum.unity.com/threads/c-proper-state-machine.380612/
 * https://developpaper.com/implementing-an-ai-with-finite-state-machine-in-unity/
 */
