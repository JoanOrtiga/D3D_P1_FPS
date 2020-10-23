using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAlertState : State<DroneMachine>
{
    public static DroneAlertState Instance { get; private set; }

    static DroneAlertState()
    {
        Instance = new DroneAlertState();
    }


    public override void Enter(DroneMachine entity)
    {
        entity.pNavMeshAgent.isStopped = true;

        entity.startRotation = entity.transform.rotation.eulerAngles.y;
    }

    public override void Execute(DroneMachine entity)
    {
        entity.transform.Rotate(new Vector3(0, entity.rotateSpeedAlert * Time.deltaTime, 0));

        if (entity.transform.rotation.eulerAngles.y > entity.startRotation + 360)
        {
            entity.pStateMachine.ChangeState(DronePatrolState.Instance);
        }

        if (entity.SeesPlayer())
        {
            if (entity.IsInAttackDistance())
                entity.pStateMachine.ChangeState(DroneAttackState.Instance);
            else
                entity.pStateMachine.ChangeState(DroneChaseState.Instance);
        }
    }

    public override void Exit(DroneMachine entity)
    {

    }
}
