using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundSound : MonoBehaviour
{
    // Start is called before the first frame update
    public static backGroundSound instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
