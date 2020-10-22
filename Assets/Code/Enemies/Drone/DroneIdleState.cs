using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdleState : IState
{
    DroneMachine owner;

    private float currentTimer;

    public DroneIdleState(DroneMachine owner) 
    { 
        this.owner = owner; 
    }

    public void Enter()
    {
        owner.GetNavMeshAgent().isStopped = true;
        currentTimer = 0.0f;
    }

    public void Execute()
    {
        currentTimer -= Time.deltaTime;
    }

    public void Exit()
    {
        
    }
}
