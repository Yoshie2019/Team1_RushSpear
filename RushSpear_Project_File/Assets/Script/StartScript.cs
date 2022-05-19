using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public float Speed = 0.025f;
    float Alfa;
    float Red, Green, Blue;

    Image StartImage;

    private bool image;
    private bool imagestop;

    public AudioClip bgm;
    public AudioClip SelectSound;
    private AudioSource audiosource;

    private int Xcount;

    // Start is called before the first frame update
    void Start()
    {
        StartImage = GameObject.Find("StartImage").GetComponent<Image>();

        Red = StartImage.color.r;
        Green = StartImage.color.g;
        Blue = StartImage.color.b;

        Alfa = StartImage.color.a;

        image = true;
        imagestop = false;

        audiosource = GetComponent<AudioSource>();
        audiosource.PlayOneShot(bgm);

        Xcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 3")
                 || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 7"))
        {
            //StartText.color = new Color(Red, Green, Blue, 1.0f);
            imagestop = true;
            Alfa = 1.0f;
            StartImage.color = new Color(Red, Green, Blue, Alfa);
            audiosource.Stop();

            audiosource.PlayOneShot(SelectSound);
            FadeScript.FadeOutPlease = true;
        }

        if (FadeScript.FadeOutFinish)
        {
            FadeScript.FadeOutFinish = false;
            SceneManager.LoadScene("StageSelect");
        }

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

        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(bgm);
        }

        if(Input.GetKeyDown("joystick button 2"))
        {
            Xcount++;
        }

        if(Xcount == 3)
        {
            SceneManager.LoadScene("GotoSecret");
        }

    }
}
