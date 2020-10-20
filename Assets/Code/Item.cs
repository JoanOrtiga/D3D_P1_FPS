using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void Pick(Collider player)
    {
        print("This shouldn't be called " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
            Pick(other);
    }
}
