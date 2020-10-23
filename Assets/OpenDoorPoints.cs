using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorPoints : MonoBehaviour
{
    public FPS_CharacterController fps;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("points", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (fps.currentPoints >= 15)
        {
            anim.SetBool("points", true);
        }
        else if (fps.currentPoints < 15)
        {
            anim.SetBool("points", false);
        }
    }
}
