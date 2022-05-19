using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoSecretScript : MonoBehaviour
{
    // シークレットステージへ移る前の画面を管理するためのスクリプト

    // 画面内にて使用する画像を格納するための変数
    public Image StartImage;

    // 画像の点滅処理を行うための変数
    private bool image;
    // 画像の点滅処理を止めるための変数
    private bool imagestop;

    // 画像の点滅スピードを設定するための変数
    public float Speed = 0.025f;

    // 点滅中に使用するアルファの数値を扱うための変数
    float Alfa;

    // 点滅中に使用する各3色の数値を扱うための変数
    float Red, Green, Blue;

    // Start is called before the first frame update
    void Start()
    {
        // 画像をtrue(表示)
        StartImage.enabled = true;

        // 点滅処理用の変数をtrue
        image = true;

        // 点滅停止用の変数をfalse
        imagestop = false;

        // 画像から3色の数値を取得し、各変数へ代入
        Red = StartImage.color.r;
        Green = StartImage.color.g;
        Blue = StartImage.color.b;

        // 画像からアルファの数値を取得し、各変数へ代入
        Alfa = StartImage.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        // 点滅用の画像へ設定済みの数値を代入
        StartImage.color = new Color(Red, Green, Blue, Alfa);

        // 点滅停止の判定が無効の場合
        if (!imagestop)
        {
            if (Alfa >= 1.0f)
            {
                // 画像のAlfa数値が1,0以上の場合 
                image = true;
            }
            else if (Alfa <= 0f)
            {
                // 画像のAlfa数値が0以下の場合
                image = false;
            }

            if (image)
            {
                // 画像のAlfa数値をSpeed分下げる
                Alfa -= Speed;
            }
            else
            {
                // 画像のAlfa数値をSpeed分上げる
                Alfa += Speed;
            }

        }


        if ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3")
                 || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 7")))
        {
            // 画面表示中にボタン入力が行われた場合

            // 点滅停止用の変数をtrue
            imagestop = true;

            // シークレットステージへ移動
            SceneManager.LoadScene("StageSecret");
        }

    }
}
