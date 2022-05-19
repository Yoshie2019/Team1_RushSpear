using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPauseScript : MonoBehaviour
{
    // ポーズ画面に関する入力処理を管理するためのスクリプト

    // ポーズ画面が表示中であるかを判定するための変数
    public static bool Pause;
    bool PauseSelectNow;

    // ポーズ画面内の、どの項目を選択しているかを判別するための変数
    public static int Select;

    // 効果音を鳴らすために必要な変数
    public AudioClip SelectSound;
    private AudioSource audiosource;

    // 入力中の時間を計測するための変数
    float SelectTime;
    public static float PauseOutTime;

    // Start is called before the first frame update
    void Start()
    {
        // 最初はポーズ画面を非表示にするため、Pauseをfalseに
        Pause = false;

        // 各変数を設定
        Select = 0;
        PauseSelectNow = true;
        SelectTime = 0f;
        PauseOutTime = 0f;

        // 効果音を取得得
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // ゲーム本編が開始されている状態
        if (StartCountDownScript.GameStart)
        {

            //　ゲーム本編開始中にSTARTボタンが押されたら
            if (Input.GetKeyDown("joystick button 7"))
            {
                audiosource.PlayOneShot(SelectSound);

                if (!Pause)
                {
                    // ポーズ画面が非表示のままの場合、ポーズ画面を表示
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
                // float型変数lsvが、ジョイスティックによる上下入力の数値を格納
                float lsv = Input.GetAxis("L_Stick_V");
                PauseOutTime = 0f;

                if (lsv != 0)
                {
                    // ポーズ画面内で入力が行われた場合
                    PauseSelect(lsv);
                }
                else
                {
                    PauseSelectNow = true;
                }

                // 各項目選択中にBボタンの入力を行った際の処理
                if (Input.GetKeyDown("joystick button 1"))
                {
                    // 効果音を鳴らす
                    audiosource.PlayOneShot(SelectSound);

                    // 選択した項目に沿った処理へ移行
                    PauseDecision();
                }

            }
            else
            {
                PauseOutTime = Time.deltaTime;
            }


        }

        // 画面内の入力に関する処理のための関数
        void PauseSelect(float lsV)
        {

            // 入力操作中の時間を測定
            if (!PauseSelectNow)
            {
                SelectTime -= Time.deltaTime;
            }

            // 入力時間が長い分、入力に指定した方向へモード選択の表示がずれ続ける
            if (SelectTime < -0.3f)
            {
                SelectTime = 0f;
                PauseSelectNow = true;
            }

            if ((lsV == 1)) // 上入力の場合
            {

                if (PauseSelectNow)
                {
                    // 効果音を流す
                    audiosource.PlayOneShot(SelectSound);

                    // 入力処理に応じて、選択中の項目がずれる
                    if (Select == 0)
                    {
                        Select = 2;
                    }
                    else
                    {
                        Select--;
                    }

                    // 画面内で、入力操作を行ったことを判別
                    PauseSelectNow = false;
                }


            }
            else  // 下入力の場合
            {

                if (PauseSelectNow)
                {
                    // 効果音を流す
                    audiosource.PlayOneShot(SelectSound);

                    // 入力処理に応じて、選択中の項目がずれる
                    if (Select == 2)
                    {
                        Select = 0;
                    }
                    else
                    {
                        Select++;
                    }

                    // 画面内で、入力操作を行ったことを判別
                    PauseSelectNow = false;

                }

            }

        }

    }

    // 選択した項目に沿った処理へ移行するための関数
    void PauseDecision()
    {
        if (Select == 0)
        {
            // ポーズ画面を閉じて、本編再開
            Pause = false;
        }
        else if (Select == 1)
        {
            // 今いるステージに、初めから再チャレンジ
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }
        else
        {
            // タイトル画面へ移動
            SceneManager.LoadScene("Title");
        }

    }

}
