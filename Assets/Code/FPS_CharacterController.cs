using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FPS_CharacterController : RestartableObject
{
    private float m_Yaw;
    private float m_Pitch;

    [Header("MOVEMENT OPTIONS")]
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


    [Header("CONTROLS")]
    public KeyCode m_LeftMovement;
    public KeyCode m_RightMovement;
    public KeyCode m_FrontMovement;
    public KeyCode m_BacktMovement;

    public KeyCode m_JumpKey;
    public KeyCode m_SprintKey = KeyCode.LeftShift;

    public KeyCode m_ReloadKey = KeyCode.R;
    public KeyCode m_ShootKey = KeyCode.Mouse0;

    [Header("GUN OPTIONS")]
    public Animation m_Weapon;
    public AnimationClip m_IdleWeapon;
    public AnimationClip m_ShootWeapon;
    public AnimationClip m_ReloadWeapon;

    public GameObject m_ShootEffect;

    public float m_MaxDistance;
    public GameObject m_HitCollisionParticlesPrefab;
    public LayerMask m_ShootLayerMask;

    public float m_GunCadency = 0.1f;
    private float m_TimeCadency;
    public float m_ReloadingTime;

    private float m_CurrentAmmoMagazines;
    private float m_CurrentAmmo;
    public float m_AmmoInMagazines;
    public float m_maxAmmo;

    [Header("Heal & Shield")]
    public float m_MaxHeal;
    private float m_CurrentHeal;

    public float m_MaxShield;
    private float m_CurrentShield;

    public float m_RecieveDamageHealPercentatge = 25f;
    public float m_RecieveDamageShieldPercentatge = 75f;

    [Header("References")]
    public PlayerStatsUI m_UpdateUI;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();

        m_Yaw = transform.rotation.eulerAngles.y;
        m_Pitch = m_PitchController.localRotation.eulerAngles.x;

        m_VerticalSpeed = 0;

        m_CurrentAmmo = m_AmmoInMagazines;
        m_CurrentAmmoMagazines = m_maxAmmo;

        SetIdleWeaponAnimation();

        m_CurrentHeal = m_MaxHeal;
        m_CurrentShield = m_MaxShield;

        m_UpdateUI.UpdateAmmo(m_CurrentAmmo, m_CurrentAmmoMagazines);
        m_UpdateUI.UpdateHeal(m_CurrentHeal);
        m_UpdateUI.UpdateShield(m_CurrentShield);
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



        m_TimeCadency -= Time.deltaTime;

        if (Input.GetKey(m_ShootKey) && !m_Weapon.IsPlaying(m_ReloadWeapon.name))
        {
            if (m_TimeCadency <= 0 && m_CurrentAmmo != 0)
            {
                m_TimeCadency = m_GunCadency;
                Shoot();
            }
        }
        else
        {
            m_ShootEffect.SetActive(false);
        }

        if (Input.GetKeyDown(m_ReloadKey) && m_CurrentAmmo < m_AmmoInMagazines)
            Reload();
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

        m_ShootEffect.SetActive(true);

        if (Physics.Raycast(l_Ray, out l_RayCastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            CreateShootHitParticles(l_RayCastHit.point, l_RayCastHit.normal);
        }

        SetShootWeaponAnimation();

        m_CurrentAmmo--;

        m_UpdateUI.UpdateAmmo(m_CurrentAmmo, m_CurrentAmmoMagazines);

        if (m_CurrentAmmo <= 0 && m_CurrentAmmoMagazines > 0)
            Reload();
    }

    void CreateShootHitParticles(Vector3 Position, Vector3 Normal)
    {
        GameObject.Instantiate(m_HitCollisionParticlesPrefab, Position, Quaternion.LookRotation(Normal));
    }

    void SetIdleWeaponAnimation()
    {
        m_Weapon.CrossFade(m_IdleWeapon.name);
    }

    void SetShootWeaponAnimation()
    {
        m_Weapon.Play(m_ShootWeapon.name);
        m_Weapon.CrossFadeQueued(m_IdleWeapon.name);
    }

    void SetReloadingWeaponAnimation()
    {
        m_Weapon.CrossFade(m_ReloadWeapon.name);
        m_Weapon.CrossFadeQueued(m_IdleWeapon.name);
    }

    private void Reload()
    {
        SetReloadingWeaponAnimation();

        m_TimeCadency = m_ReloadingTime;



        m_CurrentAmmoMagazines = Mathf.Max(0, m_CurrentAmmoMagazines - (m_AmmoInMagazines - m_CurrentAmmo));
        m_CurrentAmmo = m_AmmoInMagazines;

        m_UpdateUI.UpdateAmmo(m_CurrentAmmo, m_CurrentAmmoMagazines);
    }

    public bool IncreaseAmmo(float ammo)
    {
        if (m_CurrentAmmoMagazines == m_maxAmmo)
            return false;
        else
        {
            m_CurrentAmmoMagazines = Mathf.Min(m_CurrentAmmoMagazines + ammo, m_maxAmmo);

            m_UpdateUI.UpdateAmmo(m_CurrentAmmo, m_CurrentAmmoMagazines);

            return true;
        }
    }

    public bool IncreaseShield(float shield)
    {
        if (m_CurrentShield == m_MaxShield)
            return false;
        else
        {
            m_CurrentShield = Mathf.Min(m_CurrentShield + shield, m_MaxShield);

            m_UpdateUI.UpdateShield(m_CurrentShield);

            return true;
        }
    }

    public bool IncreaseHeal(float heal)
    {
        if (m_CurrentHeal == m_MaxHeal)
            return false;
        else
        {
            m_CurrentHeal = Mathf.Min(m_CurrentHeal + heal, m_MaxHeal);

            m_UpdateUI.UpdateHeal(m_CurrentHeal);

            return true;
        }
    }

    public void LoseHeal(float incomingDamage)
    {
        if (m_CurrentShield - (incomingDamage * m_RecieveDamageShieldPercentatge / 100) >= 0)
        {
            m_CurrentShield = m_CurrentShield - incomingDamage * m_RecieveDamageShieldPercentatge / 100;
            m_CurrentHeal = m_CurrentHeal - incomingDamage * m_RecieveDamageHealPercentatge / 100;
        }
        else
        {
            m_CurrentHeal -= m_CurrentHeal - (incomingDamage * m_RecieveDamageHealPercentatge / 100 + ((incomingDamage * m_RecieveDamageShieldPercentatge / 100) - m_CurrentShield));
            m_CurrentShield = 0;
        }

        if (m_CurrentHeal <= 0)
        {
            print("Has Mort");
        }

        m_UpdateUI.UpdateHeal(m_CurrentHeal);
        m_UpdateUI.UpdateShield(m_CurrentShield);
    }

    public override void RestartObject()
    {
        base.RestartObject();
    }
}

