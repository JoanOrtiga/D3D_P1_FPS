using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killer : MonoBehaviour
{
    public FPS_CharacterController fps;
    private int damage=50;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            fps.LoseHeal(damage);
    }
}
