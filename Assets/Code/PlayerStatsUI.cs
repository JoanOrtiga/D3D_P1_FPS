using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Text ammoText = null;
    [SerializeField] private Text healText = null;
    [SerializeField] private Text shieldText = null;

    public void UpdateAmmo(float ammo, float ammoInMagazine)
    {
        ammoText.text = ammo + " / " + ammoInMagazine;
    }

    public void UpdateHeal(float heal)
    {
        healText.text = heal.ToString();
    }

    public void UpdateShield(float shield)
    {
        shieldText.text = shield.ToString();
    }
}
