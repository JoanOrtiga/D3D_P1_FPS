using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FPS_CharacterController>().SaveStateCheckPoint();
            gameObject.SetActive(false);
        }
    }
}
