using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReActivateParticle : MonoBehaviour
{
    ParticleSystem ps;

    private void OnEnable()
    {
        if(ps == null)
        {
            ps = GetComponentInChildren<ParticleSystem>();
        }

        ps.Stop(true);
        ps.Clear();
        ps.Play(true);
    }
}
