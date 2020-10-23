using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorWithKey : MonoBehaviour
{
   public bool hasKey;
    private void Start()
    {
        hasKey = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            other.gameObject.SetActive(false);
            Debug.LogError("hola Joan, t'has cagat? xDD. banana");
        }
    }
}
