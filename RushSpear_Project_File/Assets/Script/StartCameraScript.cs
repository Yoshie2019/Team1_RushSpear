using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startcam;
    [SerializeField] Camera cam;
    public static int swing_flag = 0;
    [SerializeField] float speed = 3.0f;
    float swing_time = 0;

    public StartCountDownScript startCountSc;
    public TimeScript timeSc;
    public PlayerAnimScript pAnimSc;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = player.transform.position;
        pos.y += 8;
        pos.z += 5;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        swing_time++;
        if (swing_flag == 0)
        {
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y -= speed;
            transform.localEulerAngles = newRotation;

            if (swing_time >= 40)
            {
                swing_flag++;
                swing_time = 0;
            }
        }
        if (swing_flag == 1)
        {
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += speed;
            transform.localEulerAngles = newRotation;

            if (swing_time >= 80)
            {
                swing_flag++;
                swing_time = 0;
            }
        }
        if (swing_flag == 2)
        {
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y -= speed;
            transform.localEulerAngles = newRotation;

            if (swing_time >= 40)
            {
                swing_flag++;
                swing_time = 0;
            }
        }

        if (swing_flag == 3)
        {
            startCountSc.startCamera = false;
            timeSc.startCamera = false;
            pAnimSc.startCamera = false;
            cam.depth = -1;
            startcam.SetActive(false);
        }
    }
}