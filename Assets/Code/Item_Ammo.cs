using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ammo : Item
{
    public float m_IncreaseAmmo;
    public override void Pick(Collider player)
    {
        if (player.GetComponent<FPS_CharacterController>().IncreaseAmmo(m_IncreaseAmmo))
            Destroy(gameObject);
    }
}
