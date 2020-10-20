using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Item
{
    public float m_IncreaseShield;
    public override void Pick(Collider player)
    {
        if (player.GetComponent<FPS_CharacterController>().IncreaseShield(m_IncreaseShield))
            Destroy(gameObject);
    }
}
