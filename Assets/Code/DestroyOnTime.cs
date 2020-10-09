using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float m_DestroyTime = 3.0f;

    private void Start()
    {
        StartCoroutine(DestroyIn());
    }

    private IEnumerator DestroyIn()
    {
        yield return new WaitForSeconds(m_DestroyTime);
        Destroy(gameObject);
    }
}
