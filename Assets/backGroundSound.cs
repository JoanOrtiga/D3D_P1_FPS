using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundSound : MonoBehaviour
{
    // Start is called before the first frame update
    public static backGroundSound backGroundInstance;

    private void Awake()
    {
        if(backGroundInstance != null && backGroundInstance != this)
        {
            Destroy(this.gameObject);
            return;

        }
        backGroundInstance = this;
        DontDestroyOnLoad(this);
    }
}
