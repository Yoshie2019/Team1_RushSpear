using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCameraScript : MonoBehaviour
{
    Camera cam;
    //private PlayerScript playerScript;

    //Text SpeedText;
    Text MagtnitudeText;
    Text NormalizedText;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.depth = 0;

        //SpeedText = GameObject.Find("SpeedText").GetComponent<Text>();
        MagtnitudeText = GameObject.Find("MagtnitudeText").GetComponent<Text>();
        NormalizedText = GameObject.Find("NormalizedText").GetComponent<Text>();

        //SpeedText.enabled = false;
        MagtnitudeText.enabled = false;
        NormalizedText.enabled = false;

        //playerScript = GameObject.Find("player").GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(cam.depth == 0)
            {
                cam.depth = 2;

                //SpeedText.enabled = true;
                MagtnitudeText.enabled = true;
                NormalizedText.enabled = true;

            }
            else
            {
                //cam.depth = 0;

                //SpeedText.enabled = false;
                MagtnitudeText.enabled = false;
                NormalizedText.enabled = false;
            }
        }

    }

    public void DebugText(Vector3 Input_vector, float Speed)
    {

        //SpeedText.text = "Speed:" + Speed;
        MagtnitudeText.text = "Magtnitude:" + Input_vector.magnitude;
        NormalizedText.text = "Normalized:" + Input_vector.normalized;

    }

}
