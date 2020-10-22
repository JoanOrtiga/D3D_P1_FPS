using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneChaseState : State<DroneMachine>
{
    public static DroneChaseState Instance { get; private set; }

    static DroneChaseState()
    {
        Instance = new DroneChaseState();
    }
    public override void Enter(DroneMachine entity)
    {
        entity.timer = 0.0f;

        CalculateChasePosition(entity);
    }

    public override void Execute(DroneMachine entity)
    {
        CalculateChasePosition(entity);

        if (!entity.SeesPlayer())
        {
            entity.pStateMachine.ChangeState(DronePatrolState.Instance);
        }

        if (entity.IsInAttackDistance())
        {
            entity.pStateMachine.ChangeState(DroneAttackState.Instance);
        }
        
    }

    public override void Exit(DroneMachine entity)
    {

    }

    private void CalculateChasePosition(DroneMachine entity)
    {
        Vector3 direction = entity.player.transform.position - entity.transform.position;
        float distanceToPlayer = direction.magnitude;
        float movementDistance = distanceToPlayer - entity.minDistanceToAttack;
        direction /= distanceToPlayer;
        Vector3 chasePosition = entity.transform.position + direction * movementDistance;

        entity.pNavMeshAgent.SetDestination(chasePosition);
        entity.pNavMeshAgent.isStopped = false;
    }
}
