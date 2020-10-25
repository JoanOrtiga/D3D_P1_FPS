using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHitState : State<DroneMachine>
{
    public static DroneHitState Instance { get; private set; }
    static DroneHitState()
    {
        Instance = new DroneHitState();
    }


    public override void Enter(DroneMachine entity)
    {
        entity.timer = 0.0f;
    }

    public override void Execute(DroneMachine entity)
    {
       

        entity.pStateMachine.ChangeState(DroneAlertState.Instance);
    }

    public override void Exit(DroneMachine entity)
    {

    }
}
