using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrolState : State<DroneMachine>
{
    public override void Enter(DroneMachine entity)
    {
        entity.timer = 0.0f;
        entity.pNavMeshAgent.SetDestination(entity.target.position);
    }

    public override void Execute(DroneMachine entity)
    {

    }

    public override void Exit(DroneMachine entity)
    {

    }
}
