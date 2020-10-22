﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdleState : State<DroneMachine>
{
    private float currentTimer;

    public override void Enter(DroneMachine entity)
    {
        entity.pNavMeshAgent.isStopped = true;
        entity.timer = 0.0f;
    }

    public override void Execute(DroneMachine entity)
    {
        entity.timer -= Time.deltaTime;

        if (currentTimer < entity.stayIdleTime)
        {
           entity.pStateMachine.ChangeState(new DronePatrolState());
        }
    }

    public override void Exit(DroneMachine entity)
    {

    }
}
