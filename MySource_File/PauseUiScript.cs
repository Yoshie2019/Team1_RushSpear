using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PauseUiScript : MonoBehaviour
{
    // ポーズ画面の描画を行うためのスクリプト

    // ポーズ画面の背景用画像とテキスト
    public Image PauseImage0;
    public Image PauseImage1;
    public TextMeshProUGUI PauseText;

    // リトライモード用の背景用画像とテキスト
    public Image RetryImage;
    public TextMeshProUGUI RetryText;

    // リターンモード用の背景用画像とテキスト
    public Image ReturnImage;
    public TextMeshProUGUI ReturnText;

    // スタートモード用の背景用画像とテキスト
    public Image GoStartImage;
    public TextMeshProUGUI GoStartText;

    // 各モードを選択中に使用する画像
    public Image SelectImage1;
    public Image SelectImage2;
    public Image SelectImage3;

    // 選択用画像を格納するための配列
    Image[] SelectImage = new Image[3];

    // 各モードが選択されていない場合に使用する画像を格納するための配列
    Image[] NotSelectImage = new Image[3];

    // Start is called before the first frame update
    void Start()
    {
        // 最初はポーズ関連のImageは非表示
        PauseImage0.enabled = false;
        PauseImage1.enabled = false;
        RetryImage.enabled = false;
        ReturnImage.enabled = false;
        GoStartImage.enabled = false;

        // ポーズ関連のTextも非表示
        PauseText.enabled = false;
        RetryText.enabled = false;
        ReturnText.enabled = false;
        GoStartText.enabled = false;

        // 選択用画像を配列へ格納
        SelectImage[0] = SelectImage1;
        SelectImage[1] = SelectImage2;
        SelectImage[2] = SelectImage3;

        // 選択されていない場合の画像を配列へ格納
        NotSelectImage[0] = RetryImage;
        NotSelectImage[1] = ReturnImage;
        NotSelectImage[2] = GoStartImage;

        // 選択に関連する画像を全て非表示
        for(int i = 0;i < 3; i++)
        {
            SelectImage[i].enabled = false;
            NotSelectImage[i].enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム本編でSTARTボタンが押された場合
        if (InputPauseScript.Pause)
        {
            // ポーズ画面用のテキストと画像を表示
            PauseImage0.enabled = true;
            PauseImage1.enabled = true;
           
            PauseText.enabled = true;
            RetryText.enabled = true;
            ReturnText.enabled = true;
            GoStartText.enabled = true;

            for (int i = 0; i < 3; i++)
            {

                if (i == InputPauseScript.Select)
                {
                    // 選択中のモードは、選択用の画像を表示
                    SelectImage[i].enabled = true;
                    NotSelectImage[i].enabled = false;
                }
                else
                {
                    // それ以外は選択されていない状態用の画像を表示
                    SelectImage[i].enabled = false;
                    NotSelectImage[i].enabled = true;
                }

            }

        }
        else
        {
            // ポーズ画面を開いていない場合は、全て非表示

            PauseImage0.enabled = false;
            PauseImage1.enabled = false;

            PauseText.enabled = false;
            RetryText.enabled = false;
            ReturnText.enabled = false;
            GoStartText.enabled = false;

            for (int i = 0; i < 3; i++)
            {
                SelectImage[i].enabled = false;
                NotSelectImage[i].enabled = false;
            }

        }

    }
}
