using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdleState : State<DroneMachine>
{
    public static DroneIdleState Instance { get; private set; }

    static DroneIdleState()
    {
        Instance = new DroneIdleState();
    }


    public override void Enter(DroneMachine entity)
    {
        entity.pNavMeshAgent.isStopped = true;
        entity.timer = entity.stayIdleTime;
    }

    public override void Execute(DroneMachine entity)
    {
        entity.timer -= Time.deltaTime;

        if (entity.timer <= 0)
        {
            entity.pStateMachine.ChangeState(DronePatrolState.Instance);
        }
    }

    public override void Exit(DroneMachine entity)
    {

    }
}
