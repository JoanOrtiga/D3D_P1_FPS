using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item
{
    public float increaseHeal = 25;
    public override void Pick(Collider player)
    {
        if (player.GetComponent<FPS_CharacterController>().IncreaseHeal(increaseHeal))
            Destroy(gameObject);
    }
}
