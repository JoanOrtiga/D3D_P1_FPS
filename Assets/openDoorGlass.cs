using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorGlass : MonoBehaviour
{
    GameObject text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<openDoorWithKey>().hasKey==true)
            {
                //text.SetActive(true);
                this.GetComponent<Animator>().SetBool("character_nearby", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<openDoorWithKey>().hasKey == true)
            {
                //text.SetActive(true);
                this.GetComponent<Animator>().SetBool("character_nearby", false);
            }
        }
    }
}
