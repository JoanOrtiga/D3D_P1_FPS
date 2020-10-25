using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveOnTime : MonoBehaviour
{
    public float timeUntilOff;
    private void OnEnable()
    {
        StartCoroutine(DeActivate());
    }

    IEnumerator DeActivate()
    {
        yield return new WaitForSeconds(timeUntilOff);

        gameObject.SetActive(false);
    }
}
