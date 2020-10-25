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

    public GameObject Points;
    private float distancePj;
    public GameObject pj;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        distancePj=Vector3.Distance(pj.transform.position, this.gameObject.transform.position);
        if (distancePj <= 25.0f)
        {
            Points.SetActive(true);
        }
        else
        {
            Points.SetActive(false);
        }
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
