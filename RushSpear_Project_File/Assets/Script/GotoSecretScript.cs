using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoSecretScript : MonoBehaviour
{

    public Image StartImage;

    private bool image;
    private bool imagestop;

    public float Speed = 0.025f;
    float Alfa;
    float Red, Green, Blue;

    // Start is called before the first frame update
    void Start()
    {
        StartImage.enabled = true;

        image = true;
        imagestop = false;

        Red = StartImage.color.r;
        Green = StartImage.color.g;
        Blue = StartImage.color.b;

        Alfa = StartImage.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        StartImage.color = new Color(Red, Green, Blue, Alfa);

        if (!imagestop)
        {

            if (image)
            {
                Alfa -= Speed;
            }
            else
            {
                Alfa += Speed;
            }

            if (Alfa >= 1.0f)
            {
                image = true;
            }
            else if (Alfa <= 0f)
            {
                image = false;
            }

        }


        if ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3")
                 || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 7")))
        {
            SceneManager.LoadScene("StageSecret");
        }

    }
}
