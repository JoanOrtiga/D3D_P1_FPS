using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FPS_CharacterController : RestartableObject
{
    private float yaw;
    private float pitch;

    [Header("MOVEMENT OPTIONS")]
    public float min_Pitch = -35f;
    public float max_Pitch = 105f;

    public float yawRotationSpeed = 1f;
    public float pitchRotationSpeed = 1f;

    public Transform pitchController;
    private CharacterController characterController;

    public float movementSpeed = 10f;
    public float movementSpeedSprinting = 14f;
    private float currentMovementSpeed = 0f;
    private float verticalSpeed;
    public float jumpSpeed = 2.5f;
    public float sensitivy = 0.02f;

    private bool onGround = true;
    private CollisionFlags collisionFlags;

    [Header("CONTROLS")]
    public KeyCode leftMovement = KeyCode.A;
    public KeyCode rightMovement = KeyCode.D;
    public KeyCode frontMovement = KeyCode.W;
    public KeyCode backMovement = KeyCode.S;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("HEAL, SHIELDS & POINTS")]
    public float maxHeal;
    private float currentHeal;

    public float maxShield;
    private float currentShield;

    public float recieveDamageHealPercentatge = 25f;
    public float recieveDamageShieldPercentatge = 75f;

    public int currentPoints;

    [Header("REFERENCES")]
    public PlayerStatsUI updateUI;
    public Gun gunReference;

    private float initialPitch;
    private AudioSource walk;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        walk=this.gameObject.GetComponent<AudioSource>();
        base.Start();

        yaw = transform.rotation.eulerAngles.y;
        pitch = pitchController.localRotation.eulerAngles.x;

        initialPitch = pitch;

        verticalSpeed = 0;
        currentMovementSpeed = movementSpeed;

        currentHeal = maxHeal;
        currentShield = maxShield;

        updateUI.UpdateHeal(Mathf.RoundToInt(currentHeal));
        updateUI.UpdateShield(Mathf.RoundToInt(currentShield));

        if (gunReference != null)
        {
            gunReference.ConfigurateFromPlayer(updateUI);
        }
    }

    private void Update()
    {
        if (GameManager.instance.isPaused)
            return;

        CameraUpdate();

        Vector3 l_Movement = Vector3.zero;
        Vector3 l_Right = transform.right;
        Vector3 l_Forward = transform.forward;

        if (Input.GetKey(rightMovement))
        {
            l_Movement += l_Right;
            if (walk.isPlaying != true)
            {
                walk.Play();
            }
        }
        if (Input.GetKey(leftMovement))
        {  
            l_Movement += -l_Right;
        
            if (walk.isPlaying != true)
            {
                walk.Play();
            }
        }
        if (Input.GetKey(frontMovement))
        {
            l_Movement += l_Forward;
            if (walk.isPlaying != true)
            {
                walk.Play();
            }
        }
        if (Input.GetKey(backMovement))
        {
            l_Movement += -l_Forward;
            if (walk.isPlaying != true)
            {
                walk.Play();
            }
        }
        if (Input.GetKeyDown(jumpKey) && onGround)
        {
            verticalSpeed = jumpSpeed;
        }

        if (Input.GetKey(sprintKey))
        {
            currentMovementSpeed = movementSpeedSprinting;
        }
        else
        {
            currentMovementSpeed = movementSpeed;
        }
       
        l_Movement.Normalize();

        verticalSpeed = verticalSpeed + Physics.gravity.y * Time.deltaTime;

        l_Movement = l_Movement * currentMovementSpeed * Time.deltaTime;
        l_Movement.y = verticalSpeed * Time.deltaTime;

        collisionFlags = characterController.Move(l_Movement);

       

        GravityUpdate();


        if (Input.GetKey(KeyCode.O))
        {
            currentHeal = 0;
            updateUI.UpdateHeal(Mathf.RoundToInt(currentHeal));

            GameManager.instance.GameOver();
        }
      
    }

    private void CameraUpdate()
    {
        float mouseAxisX = Input.GetAxis("Mouse X");
        float mouseAxisY = Input.GetAxis("Mouse Y");

        yaw = yaw + mouseAxisX * yawRotationSpeed * sensitivy;
        pitch = pitch + mouseAxisY * pitchRotationSpeed * sensitivy * -1f; // *-1 to invert mouse.

        pitch = Mathf.Clamp(pitch, min_Pitch, max_Pitch);

        transform.rotation = Quaternion.Euler(0.0f, yaw, 0.0f);
        pitchController.localRotation = Quaternion.Euler(pitch, 0.0f, 0.0f);
    }

    private void GravityUpdate()
    {
        onGround = (collisionFlags & CollisionFlags.CollidedBelow) != 0;
        if (onGround || ((collisionFlags & CollisionFlags.CollidedAbove) != 0 && verticalSpeed > 0.0f))
        {
            verticalSpeed = 0.0f;
        }
    }

    public bool IncreaseShield(float shield)
    {
        if (currentShield == maxShield)
            return false;
        else
        {
            currentShield = Mathf.Min(currentShield + shield, maxShield);

            updateUI.UpdateShield(Mathf.RoundToInt(currentShield));

            return true;
        }
    }

    public bool IncreaseHeal(float heal)
    {
        if (currentHeal == maxHeal)
            return false;
        else
        {
            currentHeal = Mathf.Min(currentHeal + heal, maxHeal);

            updateUI.UpdateHeal(Mathf.RoundToInt(currentHeal));

            return true;
        }
    }

    public void LoseHeal(float incomingDamage)
    {
        if (Mathf.RoundToInt(currentShield) == 0)
        {
            currentHeal -= incomingDamage;
        }
        else
        {
            if (currentShield - (incomingDamage * recieveDamageShieldPercentatge / 100) >= 0)
            {
                currentShield = currentShield - incomingDamage * recieveDamageShieldPercentatge / 100;
                currentHeal = currentHeal - incomingDamage * recieveDamageHealPercentatge / 100;
            }
            else
            {
                currentHeal -= currentHeal - (incomingDamage * recieveDamageHealPercentatge / 100 + ((incomingDamage * recieveDamageShieldPercentatge / 100) - currentShield));
                currentShield = 0;
            }
        }

        currentHeal = Mathf.Clamp(currentHeal, 0, maxHeal);
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        if (currentHeal <= 0)
        {
            GameManager.instance.GameOver();
        }

        updateUI.UpdateHeal(Mathf.RoundToInt(currentHeal));
        updateUI.UpdateShield(Mathf.RoundToInt(currentShield));
    }
    public void addPoints(int PointsAmmount)
    {
        currentPoints += PointsAmmount;
        updateUI.UpdatePoints(currentPoints);
    }

    public override void RestartObject()
    {
        characterController.enabled = false;

        base.RestartObject();

        characterController.enabled = true;

        yaw = initialRotation.eulerAngles.y;
        pitch = initialPitch;

        currentHeal = maxHeal;
        currentShield = maxShield;

        updateUI.UpdateHeal(Mathf.RoundToInt(currentHeal));
        updateUI.UpdateShield(Mathf.RoundToInt(currentShield));
    }
}

