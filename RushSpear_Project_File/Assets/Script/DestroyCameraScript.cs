using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    //public Camera StartCamera;
    public Camera DebugCamera;
    private bool first = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(first)
        {
           //StartCamera.enabled = false;
            DebugCamera.enabled = false;
            first = false;
        }
    }
}
