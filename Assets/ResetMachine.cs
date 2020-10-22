using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResetMachine : MonoBehaviour
{
    [SerializeField] private GameObject pressE = null;
    public Animator target1;
    public Animator target2;
    public Animator target3;


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        pressE.gameObject.SetActive(true);
        
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            target1.SetBool("reset", true);
            target2.SetBool("reset", true);
            target3.SetBool("reset", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        pressE.gameObject.SetActive(false);
        target1.SetBool("reset", false);
        target2.SetBool("reset", false);
        target3.SetBool("reset", false);
    }

}
