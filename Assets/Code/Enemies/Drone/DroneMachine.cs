using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMachine : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();

    public Transform target;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        stateMachine.ChangeState(new DroneIdleState(this));
    }

    private void Update()
    {
        stateMachine.UpdateMachine();
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }
}


/*
 * https://forum.unity.com/threads/c-proper-state-machine.380612/
 * https://developpaper.com/implementing-an-ai-with-finite-state-machine-in-unity/
 */