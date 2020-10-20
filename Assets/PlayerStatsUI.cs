using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Text m_Ammo = null;
    [SerializeField] private Text m_Heal = null;
    [SerializeField] private Text m_Shield = null;

    public void UpdateAmmo(float ammo, float ammoInMagazine)
    {
        m_Ammo.text = ammo + " / " + ammoInMagazine;
    }

    public void UpdateHeal(float heal)
    {
        m_Heal.text = heal.ToString();
    }

    public void UpdateShield(float shield)
    {
        m_Shield.text = shield.ToString();
    }
}
