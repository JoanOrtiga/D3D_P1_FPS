using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FPS_CharacterController : MonoBehaviour
{
    float m_Yaw;
    float m_Pitch;

    public float m_Min_Pitch = -35f;
    public float m_Max_Pitch = 105f;

    public float m_YawRotationSpeed = 1f;
    public float m_PitchRotationSpeed = 1f;

    public Transform m_PitchController;
    private CharacterController m_CharacterController;

    public float m_Speed = 10f;
    private float m_VerticalSpeed;
    public float m_JumpSpeed = 2.5f;

    private bool m_OnGround = true;
    private CollisionFlags m_CollisionFlags;

    public KeyCode m_LeftMovement;
    public KeyCode m_RightMovement;
    public KeyCode m_FrontMovement;
    public KeyCode m_BacktMovement;

    public KeyCode m_JumpKey;
    public KeyCode m_SprintKey = KeyCode.LeftShift;


    

    public float m_MaxDistance;
    public GameObject m_HitCollisionParticlesPrefab;
    public LayerMask m_ShootLayerMask;
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        m_Yaw = transform.rotation.eulerAngles.y;
        m_Pitch = m_PitchController.localRotation.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;

        m_VerticalSpeed = 0;
    }
    
    private void Update()
    {
        CameraUpdate();

        Vector3 l_Movement = Vector3.zero;
        Vector3 l_Right = transform.right;
        Vector3 l_Forward = transform.forward;

        if (Input.GetKey(m_RightMovement))
            l_Movement += l_Right;
        if (Input.GetKey(m_LeftMovement))
            l_Movement += -l_Right;
        if (Input.GetKey(m_FrontMovement))
            l_Movement += l_Forward;
        if (Input.GetKey(m_BacktMovement))
            l_Movement += -l_Forward;

        print(m_CharacterController.isGrounded);

        if (Input.GetKeyDown(m_JumpKey) && m_OnGround)
        {
            m_VerticalSpeed = m_JumpSpeed;
        }

        l_Movement.Normalize();

        m_VerticalSpeed = m_VerticalSpeed + Physics.gravity.y * Time.deltaTime;

        l_Movement = l_Movement * m_Speed * Time.deltaTime;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime;

        m_CollisionFlags = m_CharacterController.Move(l_Movement);

        GravityUpdate();
        if(Input.GetMouseButtonDown(0))
        Shoot();
    }

    private void CameraUpdate()
    {
        float l_MouseAxisX = Input.GetAxis("Mouse X");
        float l_MouseAxisY = Input.GetAxis("Mouse Y");

        m_Yaw = m_Yaw + l_MouseAxisX * m_YawRotationSpeed * Time.deltaTime;
        m_Pitch = m_Pitch + l_MouseAxisY * m_PitchRotationSpeed * Time.deltaTime * -1f; // *-1 to invert mouse.

        m_Pitch = Mathf.Clamp(m_Pitch, m_Min_Pitch, m_Max_Pitch);

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
    }

    private void GravityUpdate()
    {
        m_OnGround = (m_CollisionFlags & CollisionFlags.CollidedBelow) != 0;
        if (m_OnGround || ((m_CollisionFlags & CollisionFlags.CollidedAbove) != 0 && m_VerticalSpeed > 0.0f))
        {
            m_VerticalSpeed = 0.0f;
        }
    }
   private void Shoot()
    {
        Ray l_Ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RayCastHit;
        if(Physics.Raycast(l_Ray,out l_RayCastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            CreateShootHitParticles(l_RayCastHit.point, l_RayCastHit.normal);
        }
    }
    void CreateShootHitParticles(Vector3 Position, Vector3 Normal)
    {
        GameObject.Instantiate(m_HitCollisionParticlesPrefab, Position, Quaternion.identity);
    }
}
    