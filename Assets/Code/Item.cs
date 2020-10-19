using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void Pick()
    {
        print("This shouldn't be called " + gameObject.name);
    }
}
