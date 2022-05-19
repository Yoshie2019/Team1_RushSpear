using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPauseScript : MonoBehaviour
{
    public static bool Pause;
    public static int Select;

    bool PauseSelectNow;

    public AudioClip SelectSound;
    private AudioSource audiosource;

    float SelectTime;

    public static float PauseOutTime;

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        Select = 0;

        PauseSelectNow = true;
        audiosource = GetComponent<AudioSource>();

        SelectTime = 0f;
        PauseOutTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCountDownScript.GameStart)
        {

            //　ゲーム本編開始中にSTARTボタンが押されたら
            if (Input.GetKeyDown("joystick button 7"))
            {
                audiosource.PlayOneShot(SelectSound);

                if (!Pause)
                {
                    Pause = true;
                    StartCountDownScript.GameStart = false;
                }
                else
                {
                    Pause = false;
                    StartCountDownScript.GameStart = true;
                }


            }

            //　ポーズ中の操作
            if (Pause)
            {
                float lsv = Input.GetAxis("L_Stick_V");
                PauseOutTime = 0f;

                if (lsv != 0)
                {
                    PauseSelect(lsv);
                }
                else
                {
                    PauseSelectNow = true;
                }

                // 各項目選択中にBボタンの入力を行った際の処理
                if (Input.GetKeyDown("joystick button 1"))
                {
                    audiosource.PlayOneShot(SelectSound);
                    PauseDecision();
                }

            }
            else
            {
                PauseOutTime = Time.deltaTime;
            }


        }


        void PauseSelect(float lsV)
        {

            if (!PauseSelectNow)
            {
                SelectTime -= Time.deltaTime;
            }

            if (SelectTime < -0.3f)
            {
                SelectTime = 0f;
                PauseSelectNow = true;
            }

            if ((lsV == 1)) // 上入力の場合
            {

                if (PauseSelectNow)
                {

                    audiosource.PlayOneShot(SelectSound);

                    if (Select == 0)
                    {
                        Select = 2;
                    }
                    else
                    {
                        Select--;
                    }

                    PauseSelectNow = false;
                }


            }
            else  // 下入力の場合
            {

                if (PauseSelectNow)
                {

                    audiosource.PlayOneShot(SelectSound);

                    if (Select == 2)
                    {
                        Select = 0;
                    }
                    else
                    {
                        Select++;
                    }

                    PauseSelectNow = false;

                }

            }

            //}

        }

    }

    void PauseDecision()
    {
        if (Select == 0)
        {
            Pause = false;
        }
        else if (Select == 1)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("Title");
        }

    }

}
