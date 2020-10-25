using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMachine : Enemy
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
    public float startRotation;

    [Header("ATTACK")]
    public float minDistanceToAttack = 3.0f;
    public float maxDistanceToAttack = 7.0f;
    public float rotationAttackLerp = 0.05f;

    [Header("PATROL")]
    public List<Transform> waypoints;
    [HideInInspector] public int currentWaypointID = 0;

    [Header("CHASE")]
    public float maxDistanceToRaycast = 15f;
    public float coneAngle = 60f;
    public LayerMask sightLayerMask;

    [Header("DIE")]
    [Tooltip("Chances must be a total of 100%")]
    public List<DropChance> droppingItems;
    [Tooltip("y must be between 1 and 0")]
    public AnimationCurve fadeOut;
    [HideInInspector] public Material material;
    public Renderer[] droneRenderer { get; private set; }


    [Header("REFERENCES")]
    public FPS_CharacterController player;
    public Transform eyes;

    [Header("HEALTH")]
    public int maxHP = 100;
    public int currentHP { get; private set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        droneRenderer = GetComponentsInChildren<Renderer>();
    }

    protected override void Start()
    {
        base.Start();

        currentHP = maxHP;

        HealthUpdate();

        material = new Material(droneRenderer[0].material);

        stateMachine = new StateMachine<DroneMachine>(this);
        stateMachine.ChangeState(DroneIdleState.Instance);

    }

    private void Update()
    {
        //print(stateMachine.CurrentState());

        stateMachine.UpdateMachine();

        if (hpBar != null)
            HPBarUpdate();
    }

    public bool SeesPlayer()
    {
        Vector3 direction = (player.transform.position + Vector3.up * 1.5f) - eyes.position;
        float distanceToPlayer = direction.magnitude;
        direction /= distanceToPlayer;

        bool isOnCone = Vector3.Dot(transform.forward, direction) >= Mathf.Cos(coneAngle * Mathf.Deg2Rad * 0.5f);

        if (isOnCone && !Physics.Linecast(eyes.position, player.transform.position + Vector3.up * 1.5f, sightLayerMask.value))
        {
            return true;
        }

        return false;
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

    private void HPBarUpdate()
    {
        bool hit = Physics.Linecast(transform.position, player.transform.position, sightLayerMask);

        if (hit || !droneRenderer[0].isVisible)
        {
            if (hpBar.gameObject.activeSelf)
                hpBar.gameObject.SetActive(false);
            return;
        }
        else if (!hit && droneRenderer[0].isVisible)
        {
            if (!hpBar.gameObject.activeSelf)
                hpBar.gameObject.SetActive(true);
        }


        if(GameManager.instance.mainCamera != null)
        {
            Vector2 position = GameManager.instance.mainCamera.WorldToViewportPoint(transform.position + hpBarOffSet);

            position.x *= hpBar.canvas.pixelRect.size.x;
            position.y *= hpBar.canvas.pixelRect.size.y;

            hpBar.rectTransform.anchoredPosition = position;
        }
            
    }

    private void HealthUpdate()
    {
        healthBar.fillAmount = (float)currentHP / maxHP;
    }

    public override void RestartObject()
    {
        base.RestartObject();


        for (int i = 0; i < droneRenderer.Length; i++)
        {
            droneRenderer[i].material.color = new Color(droneRenderer[i].material.color.r, droneRenderer[i].material.color.g, droneRenderer[i].material.color.b, 1);
        }

        currentHP = maxHP;
        HealthUpdate();

        stateMachine.ChangeState(DroneIdleState.Instance);

    }
    public void RecieveDamage(int damage)
    {
        currentHP -= damage;
        HealthUpdate();

        if (currentHP <= 0)
        {
            stateMachine.ChangeState(DroneDieState.Instance);
        }
        else
        {
            stateMachine.ChangeState(DroneHitState.Instance);
        }

    }
}


/*
 * https://forum.unity.com/threads/c-proper-state-machine.380612/
 * https://developpaper.com/implementing-an-ai-with-finite-state-machine-in-unity/
 */
