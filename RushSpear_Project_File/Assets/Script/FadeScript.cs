using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float Speed = 0.01f;
    float Alfa;
    float Red, Green, Blue;

    public static bool FadeInFinish;  // フェードインが完了した際に使う変数
    public static bool FadeOutFinish; // フェードアウトが完了した際に使う変数

    public static bool FadeInPlease;  // フェードインが必要とされる時に使う変数
    public static bool FadeOutPlease; // フェードアウトが必要とされる時に使う変数

    Image FadeImage;

    // Start is called before the first frame update
    void Start()
    {
        FadeImage = GetComponent<Image>();

        Red = FadeImage.color.r;
        Green = FadeImage.color.g;
        Blue = FadeImage.color.b;

        Alfa = FadeImage.color.a;

        FadeInFinish = false;
        FadeOutFinish = false;

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

    void FadeOut()
    {
        FadeImage.color = new Color(Red, Green, Blue, Alfa);
        Alfa += Speed;

        //　フェードアウトが完了した場合
        if (Alfa >= 1.0f)
        {
            FadeOutPlease = false;
            FadeOutFinish = true;
        }

    }

    void FadeIn()
    {
        FadeImage.color = new Color(Red, Green, Blue, Alfa);
        Alfa -= Speed;

        //　フェードインが完了した場合
        if (Alfa <= 0f)
        {
            FadeInPlease = false;
            FadeInFinish = true;
            //FadeImage.enabled = false;
        }

    }
}
