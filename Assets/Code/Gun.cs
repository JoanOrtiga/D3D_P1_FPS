using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("GUN CONTROLS")]
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("GUN ANIMATIONS")]
    public Animation weapon;
    public AnimationClip idleWeapon;
    public AnimationClip shootWeapon;
    public AnimationClip reloadWeapon;

    public GameObject shootEffect;

    [Header("GUN OPTIONS")]
    public float maxDistance;
    public GameObject hitCollisionParticlesPrefab;
    public LayerMask shootLayerMask;

    public float gunCadency = 0.1f;
    private float timeCadency;
    public float reloadingTime;


    [Header("GUN MAGAZINES")]
    public int ammoInMagazines;
    public int maxAmmo;
    private int currentAmmoMagazines;
    private int currentAmmo;

    private PlayerStatsUI updateUI;

    private void Start()
    {
        currentAmmo = ammoInMagazines;
        currentAmmoMagazines = maxAmmo;

        SetIdleWeaponAnimation();
    }

    private void Update()
    {
        timeCadency -= Time.deltaTime;

        if (Input.GetKey(shootKey) && !weapon.IsPlaying(reloadWeapon.name))
        {
            if (timeCadency <= 0 && currentAmmo > 0)
            {
                timeCadency = gunCadency;
                Shoot();
            }
        }
        else
        {
            shootEffect.SetActive(false);
        }

        if (Input.GetKeyDown(reloadKey) && currentAmmo < ammoInMagazines)
            Reload();
    }

    private void Shoot()
    {
        Ray l_Ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RayCastHit;

        shootEffect.SetActive(true);

        if (Physics.Raycast(l_Ray, out l_RayCastHit, maxDistance, shootLayerMask.value))
        {
            CreateShootHitParticles(l_RayCastHit.point, l_RayCastHit.normal);
        }

        SetShootWeaponAnimation();

        currentAmmo--;

        updateUI.UpdateAmmo(currentAmmo, currentAmmoMagazines);

        if (currentAmmo <= 0 && currentAmmoMagazines > 0)
            Reload();
    }

    void CreateShootHitParticles(Vector3 Position, Vector3 Normal)
    {
        GameObject.Instantiate(hitCollisionParticlesPrefab, Position, Quaternion.LookRotation(Normal));
    }

    void SetIdleWeaponAnimation()
    {
        weapon.CrossFade(idleWeapon.name);
    }

    void SetShootWeaponAnimation()
    {
        weapon.Play(shootWeapon.name);
        weapon.CrossFadeQueued(idleWeapon.name);
    }

    void SetReloadingWeaponAnimation()
    {
        weapon.CrossFade(reloadWeapon.name);
        weapon.CrossFadeQueued(idleWeapon.name);
    }

    private void Reload()
    {
        SetReloadingWeaponAnimation();

        timeCadency = reloadingTime;

        currentAmmoMagazines = Mathf.Max(0, currentAmmoMagazines - (ammoInMagazines - currentAmmo));
        currentAmmo = ammoInMagazines;

        updateUI.UpdateAmmo(currentAmmo, currentAmmoMagazines);
    }

    public bool IncreaseAmmo(int ammo)
    {
        if (currentAmmoMagazines == maxAmmo)
            return false;
        else
        {
            currentAmmoMagazines = Mathf.Min(currentAmmoMagazines + ammo, maxAmmo);

            updateUI.UpdateAmmo(currentAmmo, currentAmmoMagazines);

            return true;
        }   
    }

    public void ConfigurateFromPlayer(PlayerStatsUI ui)
    {
        updateUI = ui;

        updateUI.UpdateAmmo(currentAmmo, currentAmmoMagazines);
        
    }
}
