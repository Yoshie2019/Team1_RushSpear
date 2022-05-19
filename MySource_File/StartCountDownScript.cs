using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartCountDownScript : MonoBehaviour
{
    //ゲーム本編が開始、または終了の際に使用するUIを描画するためのスクリプト

    // 開始時のカウントダウン演出に使用するための画像
    public Image ThreeImage;
    public Image TweImage;
    public Image OneImage;
    public Image StartImage;

    // ゲーム本編が終了した際に表示させる画像
    public Image FinishImage;

    // カウントダウン時間を設定するための変数
    float time;

    // カウントダウン用の画像を格納するための配列
    Image[] CountDownImage = new Image[3];

    // ゲームスタート時の判定を行うための変数
    public static bool GameStart;
    public static int random;
    public bool startCamera = true;

    // 効果音を鳴らすための変数
    public AudioClip bgm;
    private AudioSource audiosource;

    void Start()
    {
        // セレクト画面にて選択したモードの情報を格納
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

        // 各カウントダウン用画像を配列へ格納
        CountDownImage[0] = ThreeImage;
        CountDownImage[1] = TweImage;
        CountDownImage[2] = OneImage;

        // カウントダウン用画像を全て非表示
        for (int i = 0; i < 3; i++)
        {
            CountDownImage[i].enabled = false;
        }

        // スタート時に使用する変数はfalse
        StartImage.enabled = false;
        GameStart = false;

        // カウントダウン時間を設定
        time = 4.00f;

        // 終了時の画像を非表示
        FinishImage.enabled = false;

        // 効果音を取得
        audiosource = GetComponent<AudioSource>();
        audiosource.PlayOneShot(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ画面を開く、または閉じる際に効果音を鳴らす
        if (InputPauseScript.Pause)
        {
            audiosource.Pause();
        }
        else
        {
            audiosource.UnPause();
        }

        // ゲーム本編が始まってない、又は終了したら時間を計る-----------------------------------------------------
        if (!GameStart && startCamera == false)
        {
            time -= Time.deltaTime;
        }

        // 本編開始前のカウントダウンを表示
        if (time > 3.0f && startCamera == false)
        {
            CountDownImage[2].enabled = true;
        }
        else if (time < 3.0f && time > 2.0f)
        {
            CountDownImage[2].enabled = false;
            CountDownImage[1].enabled = true;
        }
        else if (time < 2.0f && time > 1.0f)
        {
            CountDownImage[1].enabled = false;
            CountDownImage[0].enabled = true;
        }
        else if (time < 1.0f && time > 0.0f)
        {
            CountDownImage[0].enabled = false;
            StartImage.enabled = true;
        }
        else if (time < 0.0f)
        {
            StartImage.enabled = false;
            GameStart = true;
        }

        // 制限時間が終わったら
        if (TimeScript.GameFinish)
        {
            // 終了時用の効果音を鳴らす
            audiosource.Stop();

            // 終了時用の画像を表示
            FinishImage.enabled = true;

            // GameStartをfalseにし、本編終了判定にする
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

        // 本編が終了していない、またはポーズ画面を開いていない場合
        if (GameStart && !InputPauseScript.Pause && !audiosource.isPlaying)
        {
            // BGMを鳴らし続ける
            audiosource.PlayOneShot(bgm);
        }

    }

}