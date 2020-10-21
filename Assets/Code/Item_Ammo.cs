using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ammo : Item
{
    public int increaseAmmo = 30;
    public override void Pick(Collider player)
    {
        Gun gunRef = player.GetComponentInChildren<Gun>();

        if (gunRef != null)
        {
            if (player.GetComponentInChildren<Gun>().IncreaseAmmo(increaseAmmo))
                Destroy(gameObject);
        }

    }
}
