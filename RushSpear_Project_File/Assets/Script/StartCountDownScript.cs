using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//開始終了の際のUI表示の処理
public class StartCountDownScript : MonoBehaviour
{
    //public Text ThreeText;
    //public Text TweText;
    //public Text OneText;
    //public Text StartText;

    public Image ThreeImage;
    public Image TweImage;
    public Image OneImage;
    public Image StartImage;

    //public Text FinishText;
    public Image FinishImage;

    float time;

    //Text[] CountDownText = new Text[3];
    Image[] CountDownImage = new Image[3];

    public static bool GameStart;
    public static int random;
    public bool startCamera = true;

    public AudioClip bgm;
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {

        //CountDownText[0] = ThreeText;
        //CountDownText[1] = TweText;
        //CountDownText[2] = OneText;

        if (SceneManager.GetActiveScene().name == "StageEasy")
        {
            LoadScript.Stage = 1;
        }
        else if (SceneManager.GetActiveScene().name == "StageNormal")
        {
            LoadScript.Stage = 2;
        }
        else if (SceneManager.GetActiveScene().name == "StageHard")
        {
            LoadScript.Stage = 3;
        }
        else if (SceneManager.GetActiveScene().name == "StageVeryHard")
        {
            LoadScript.Stage = 4;
        }
        else if (SceneManager.GetActiveScene().name == "StageSecret")
        {
            LoadScript.Stage = 5;
        }

        CountDownImage[0] = ThreeImage;
        CountDownImage[1] = TweImage;
        CountDownImage[2] = OneImage;

        for (int i = 0; i < 3; i++)
        {
            CountDownImage[i].enabled = false;

            //if (i == 2)
            //{
            //    //CountDownText[i].enabled = true;
            //    CountDownImage[i].enabled = true;
            //}
            //else
            //{
            //    //CountDownText[i].enabled = false;
            //    CountDownImage[i].enabled = false;
            //}

        }

        //StartText.enabled = false;
        StartImage.enabled = false;
        GameStart = false;
        time = 4.00f;

        //FinishText.enabled = false;
        FinishImage.enabled = false;

        audiosource = GetComponent<AudioSource>();
        audiosource.PlayOneShot(bgm);
    }

    // Update is called once per frame
    void Update()
    {

        if (InputPauseScript.Pause)
        {
            audiosource.Pause();
        }
        else
        {
            audiosource.UnPause();
        }

        //if (startCamera == true)
        //{
        //    if (StartCameraScript.swing_flag == 3)
        //    {
        //        startCamera = false;
        //    }
        //}

        // ゲーム本編が始まってない、又は終了したら時間を計る-----------------------------------------------------
        if (!GameStart && startCamera == false/*StartCameraScript.swing_flag == 3*/)
        {
            time -= Time.deltaTime;
        }

        // 本編開始前のカウントダウンを表示
        if (time > 3.0f && startCamera == false/*StartCameraScript.swing_flag == 3*/)
        {
            //CountDownText[2].enabled = true;
            CountDownImage[2].enabled = true;
        }
        else if (time < 3.0f && time > 2.0f)
        {
            //CountDownText[2].enabled = false;
            //CountDownText[1].enabled = true;

            CountDownImage[2].enabled = false;
            CountDownImage[1].enabled = true;
        }
        else if (time < 2.0f && time > 1.0f)
        {
            //CountDownText[1].enabled = false;
            //CountDownText[0].enabled = true;

            CountDownImage[1].enabled = false;
            CountDownImage[0].enabled = true;
        }
        else if (time < 1.0f && time > 0.0f)
        {
            //CountDownText[0].enabled = false;
            //StartText.enabled = true;

            CountDownImage[0].enabled = false;
            StartImage.enabled = true;
        }
        else if (time < 0.0f)
        {
            //StartText.enabled = false;
            StartImage.enabled = false;

            GameStart = true;
        }

        // 制限時間が終わったら
        if (TimeScript.GameFinish)
        {
            audiosource.Stop();
            //FinishText.enabled = true;
            FinishImage.enabled = true;
            GameStart = false;

            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3")
                || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 7"))
            {
                //// 仮の処理
                random = Random.Range(0, 4);

                // リザルト画面へ
                SceneManager.LoadScene("Result");
            }

        }

        if (GameStart && !InputPauseScript.Pause && !audiosource.isPlaying)
        {
            audiosource.PlayOneShot(bgm);
        }

    }

}