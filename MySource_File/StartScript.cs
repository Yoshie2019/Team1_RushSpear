using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // スタート画面内の処理を管理するためのスクリプト

    // START画像の点滅するスピードを設定するための変数
    public float Speed = 0.025f;

    // START画像のAlfaの数値を取得するための変数
    float Alfa;

    // START画像の3色の数値を取得するための変数
    float Red, Green, Blue;

    // START画像を取得するための変数
    Image StartImage;

    // 点滅処理を管理するための変数
    private bool image;
    private bool imagestop;

    // スタート画面内の効果音やBGMを管理するための変数
    public AudioClip bgm;
    public AudioClip SelectSound;
    private AudioSource audiosource;

    // Xボタンの入力回数を把握するための変数
    private int Xcount;

    // Start is called before the first frame update
    void Start()
    {
        // START画像を取得
        StartImage = GameObject.Find("StartImage").GetComponent<Image>();

        // START画像の3色の数値を取得
        Red = StartImage.color.r;
        Green = StartImage.color.g;
        Blue = StartImage.color.b;

        // START画像のAlfaの数値を取得
        Alfa = StartImage.color.a;

        // 管理用の変数をそれぞれ設定
        image = true;
        imagestop = false;

        // 効果音やBGMを取得
        audiosource = GetComponent<AudioSource>();

        // BGMを鳴らす
        audiosource.PlayOneShot(bgm);

        // 初期値を設定
        Xcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ボタンの入力が行われた場合
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 3")
                 || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 7"))
        {
            // 点滅処理を停止させるため、imagestopをtrueに変更
            imagestop = true;

            // Alfaの数値を設定
            Alfa = 1.0f;

            // 画像の3色の数値を再設定
            StartImage.color = new Color(Red, Green, Blue, Alfa);
            audiosource.Stop();

            // 効果音を鳴らす
            audiosource.PlayOneShot(SelectSound);

            // フェードアウト演出開始
            FadeScript.FadeOutPlease = true;
        }

        // フェーどアウト演出が終了した場合
        if (FadeScript.FadeOutFinish)
        {
            // ステージセレクト画面へ移動
            FadeScript.FadeOutFinish = false;
            SceneManager.LoadScene("StageSelect");
        }

        // 画像の3色の数値を再設定
        StartImage.color = new Color(Red, Green, Blue, Alfa);

        // ボタン入力が行われていない状態
        if (!imagestop)
        {
            
            if (Alfa >= 1.0f)
            {
                // Alfaの数値が1.0以上の場合
                image = true;
            }
            else if (Alfa <= 0f)
            {
                // Alfaの数値が0以下の場合
                image = false;
            }

            if (image)
            {
                // Alfaの数値をSpeed分だけ下げる
                Alfa -= Speed;
            }
            else
            {
                // Alfaの数値をSpeed分だけ上げる
                Alfa += Speed;
            }

        }

        // BGMが鳴り終わった場合
        if (!audiosource.isPlaying)
        {
            // 再度BGMを鳴らす
            audiosource.PlayOneShot(bgm);
        }

        // Xボタンが入力された場合
        if(Input.GetKeyDown("joystick button 2"))
        {
            Xcount++;
        }

        // Xボタンが3回入力された場合
        if (Xcount == 3)
        {
            // シークレットステージ移行画面へ
            SceneManager.LoadScene("GotoSecret");
        }

    }
}
