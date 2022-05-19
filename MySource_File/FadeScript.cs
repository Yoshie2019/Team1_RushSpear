using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    // ステージセレクト画面へ入る際に、フェードイン・フェードアウト演出を流すためのスクリプト

    public float Speed = 0.01f;       // フェード演出のスピード具合を設定するための変数
    float Alfa;                       // 演出時に使用するアルファの数値を扱うための変数
    float Red, Green, Blue;           // 演出時に使用する各3色の数値を扱うための変数

    public static bool FadeInFinish;  // フェードインが完了した際に使う変数
    public static bool FadeOutFinish; // フェードアウトが完了した際に使う変数

    public static bool FadeInPlease;  // フェードインが必要とされる時に使う変数
    public static bool FadeOutPlease; // フェードアウトが必要とされる時に使う変数

    Image FadeImage;

    // Start is called before the first frame update
    void Start()
    {
        // フェード演出時に使う画像を取得
        FadeImage = GetComponent<Image>();

        // 画像から3色の数値を取得し、各変数へ代入
        Red = FadeImage.color.r;
        Green = FadeImage.color.g;
        Blue = FadeImage.color.b;

        // 画像からアルファの数値を取得し、各変数へ代入
        Alfa = FadeImage.color.a;

        // フェード演出判定用の変数をfalse
        FadeInFinish = false;
        FadeOutFinish = false;

        // フェード演出要求用の変数をfalse
        FadeInPlease = false;
        FadeOutPlease = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (FadeInPlease)
        {
            //　他のスクリプトからフェードインが要求された場合
            FadeIn();
        }

        if (FadeOutPlease)
        {
            //　他のスクリプトからフェードアウトが要求された場合
            FadeOut();
        }

        
    }

    // フェードアウト演出用の関数
    void FadeOut()
    {
        // 要求時に、フェード演出用の画像へ設定済みの数値を代入
        FadeImage.color = new Color(Red, Green, Blue, Alfa);

        // アルファの数値を、Speedの分だけ上げていく
        Alfa += Speed;

        //　フェードアウトが完了した場合
        if (Alfa >= 1.0f)
        {
            // 要求用の変数をfalse
            FadeOutPlease = false;

            // 判定用の変数をtrue
            FadeOutFinish = true;
        }

    }

    // フェードイン演出用の関数
    void FadeIn()
    {
        // 要求時に、フェード演出用の画像へ設定済みの数値を代入
        FadeImage.color = new Color(Red, Green, Blue, Alfa);

        // アルファの数値を、Speedの分だけ下げていく
        Alfa -= Speed;

        //　フェードインが完了した場合
        if (Alfa <= 0f)
        {
            // 要求用の変数をfalse
            FadeInPlease = false;

            // 判定用の変数をtrue
            FadeInFinish = true;
        }

    }
}
