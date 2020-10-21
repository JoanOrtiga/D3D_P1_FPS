using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Item
{
    public float increaseShield = 25;
    public override void Pick(Collider player)
    {
        if (player.GetComponent<FPS_CharacterController>().IncreaseShield(increaseShield))
            Destroy(gameObject);
    }
}
