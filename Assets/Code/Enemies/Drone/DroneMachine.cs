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
    public float searchTime = 1f;

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



    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
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
        /*Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        bool isOnCone = Vector3.Dot(transform.forward, direction) >= Mathf.Cos(coneAngle * Mathf.Deg2Rad * 0.5f);

        Ray ray = new Ray(eyes.position, direction);
        if (isOnCone && !Physics.Raycast(ray, maxDistanceToRaycast, sightLayerMask.value))
        {
            return true;
        }

        return false;*/

        /*  Vector3 direction = player.transform.position+Vector3.up * 1.6f - transform.position;
          direction /= direction.magnitude;
          bool isOnCone = Vector3.Dot(transform.forward, direction) >= Mathf.Cos(coneAngle * Mathf.Deg2Rad * 0.5f);

          Ray ray = new Ray(eyes.position, direction);
          if (isOnCone && !Physics.Raycast(ray, maxDistanceToRaycast, sightLayerMask.value))
          {
              return true;
          }

          return false;*/

        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        bool isOnCone = angle < coneAngle / 2f;

        Ray ray = new Ray(eyes.position, targetDir.normalized);

        if (isOnCone && !Physics.Raycast(ray, maxDistanceToRaycast, sightLayerMask.value))
        {
            print("true");

            return true;
        }
        else
        {
            return false;
        }

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
