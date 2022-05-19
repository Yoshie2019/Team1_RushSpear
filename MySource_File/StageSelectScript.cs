using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StageSelectScript : MonoBehaviour
{
    // ステージ選択画面の描画処理を管理

    // 開放済みステージ用の画像
    public Image SelectImage1;
    public Image SelectImage2;
    public Image SelectImage3;
    public Image SelectImage4;

    // 未開放ステージ用の画像
    public Image NotSelectImage1;
    public Image NotSelectImage2;
    public Image NotSelectImage3;
    public Image NotSelectImage4;

    // 各画像を格納するための配列
    Image[] SelectImage = new Image[4];
    Image[] NotSelectImage = new Image[4];

    //各ステージを選択している際に表示されるText
    public Text EasyInformationText;

    public Text NormalInformationText;
    public Text NotNormalInformationText;

    public Text HardInformationText;
    public Text NotHardInformationText;

    public Text VeryHardInformationText;
    public Text NotVeryHardInformationText;

    Text[] InformationText = new Text[4];
    Text[] NotInformationText = new Text[4];

    // Start is called before the first frame update
    void Start()
    {
        // 各画像を配列へ格納
        SelectImage[0] = SelectImage1;
        SelectImage[1] = SelectImage2;
        SelectImage[2] = SelectImage3;
        SelectImage[3] = SelectImage4;

        NotSelectImage[0] = SelectImage1;
        NotSelectImage[1] = NotSelectImage2;
        NotSelectImage[2] = NotSelectImage3;
        NotSelectImage[3] = NotSelectImage4;

        // 各テキストを配列へ格納
        InformationText[0] = EasyInformationText;
        InformationText[1] = NormalInformationText;
        InformationText[2] = HardInformationText;
        InformationText[3] = VeryHardInformationText;

        NotInformationText[0] = EasyInformationText;
        NotInformationText[1] = NotNormalInformationText;
        NotInformationText[2] = NotHardInformationText;
        NotInformationText[3] = NotVeryHardInformationText;

        for (int i = 0; i < 4; i++)
        {

            if (i == 0)
            {
                // Easyステージの画像とテキストを表示
                SelectImage[i].enabled = true;
                InformationText[i].enabled = true;
            }
            else
            {
                // それ以外のステージの画像とテキストは非表示に
                SelectImage[i].enabled = false;
                InformationText[i].enabled = false;

                // 未開放用の画像とテキストを表示
                NotSelectImage[i].enabled = false;
                NotInformationText[i].enabled = false;
            }

        }

        
    }

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < 4; i++)
        {
            // iがModeSelectから1引いた値と同じ値なら、描画を行う
            if (i == LoadScript.ModeSelect - 1)
            {

                // 画像とTextの描画
                if (i > 0)
                {


                    if (i == 1)
                    {
                        
                        if (ClearControlScript.EasyClear)
                        {
                            // Easyモードがクリア済みの場合、Normalモード開放済み用の画像とテキストを表示
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            // そうでない場合、Normalモード未開放用の画像とテキストを表示
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }
                    else if (i == 2)
                    {

                        if (ClearControlScript.NormalClear)
                        {
                            // Normalモードがクリア済みの場合、Hardモード開放済み用の画像とテキストを表示
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            // そうでない場合、Hardモード未開放用の画像とテキストを表示
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }
                    else if (i == 3)
                    {

                        if (ClearControlScript.HardClear)
                        {
                            // Hardモードがクリア済みの場合、VeryHardモード開放済み用の画像とテキストを表示
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            // そうでない場合、VeryHardモード未開放用の画像とテキストを表示
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }

                }
                else
                {
                    // Easyモード用の画像とテキストを表示
                    SelectImage[i].enabled = true;
                    InformationText[i].enabled = true;
                }

            }
            else
            {
                // 選択中でないモードに関する画像とテキストは、全て非表示
                SelectImage[i].enabled = false;
                NotSelectImage[i].enabled = false;
                InformationText[i].enabled = false;
                NotInformationText[i].enabled = false;
            }


        }

    }


}
