using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMachine : MonoBehaviour
{
    public Transform target;

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

    public float timer;

    [Header("IDLE")]
    public float stayIdleTime;

    [Header("ALERT")]
    public float minDistanceToAlert = 5.0f;

    public float minDistanceToAttack = 3.0f;
    public float maxDistanceToAttack = 7.0f;

    public float maxDistanceToPatrol = 15.0f;

    public float coneAngle = 60f;
    public float lerpAttackRotation = 0.6f;


    public FPS_CharacterController player;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        stateMachine = new StateMachine<DroneMachine>(this);
    }

    private void Update()
    {
        stateMachine.UpdateMachine();
    }
}


/*
 * https://forum.unity.com/threads/c-proper-state-machine.380612/
 * https://developpaper.com/implementing-an-ai-with-finite-state-machine-in-unity/
 */
