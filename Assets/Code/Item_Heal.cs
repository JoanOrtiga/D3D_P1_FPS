using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item
{
    public float m_IncreaseHeal;
    public override void Pick(Collider player)
    {
        if (player.GetComponent<FPS_CharacterController>().IncreaseHeal(m_IncreaseHeal))
            Destroy(gameObject);
    }
}
