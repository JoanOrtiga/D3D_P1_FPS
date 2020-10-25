using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingTarget : MonoBehaviour
{
    public Animator anim;
    public FPS_CharacterController fps;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hit()
    {
        fps.addPoints(points);
        anim.Play("hit");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            this.anim.SetBool("reset", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            this.anim.SetBool("reset", false);
        }
    }
  }