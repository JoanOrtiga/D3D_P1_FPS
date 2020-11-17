using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Text ammoText = null;
    [SerializeField] private Text healText = null;
    [SerializeField] private Text shieldText = null;
    [SerializeField] private Text galleryPoints = null;

    public void UpdateAmmo(float ammo, float ammoInMagazine)
    {

        if(ammoText != null)
            ammoText.text = ammo + " / " + ammoInMagazine;
    }

    public void UpdateHeal(int heal)
    {
        healText.text = heal.ToString();
    }

    public void UpdateShield(int shield)
    {
        shieldText.text = shield.ToString();
    }
    public void UpdatePoints(int points)
    {
        galleryPoints.text = "Points" + points.ToString();
    }
}
