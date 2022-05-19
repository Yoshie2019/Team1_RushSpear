using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//enum Stage {Easy, Normal, Hard, VeryHard}

//　ステージ選択画面の描画処理を管理
public class StageSelectScript : MonoBehaviour
{
    // ステージセレクト画面で扱うImage
    public Image SelectImage1;
    public Image SelectImage2;
    public Image SelectImage3;
    public Image SelectImage4;

    public Image NotSelectImage1;
    public Image NotSelectImage2;
    public Image NotSelectImage3;
    public Image NotSelectImage4;

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

    //public AudioClip bgm;
    //private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {

        // Easy以外を非表示に
        
        SelectImage[0] = SelectImage1;
        SelectImage[1] = SelectImage2;
        SelectImage[2] = SelectImage3;
        SelectImage[3] = SelectImage4;

        NotSelectImage[0] = SelectImage1;
        NotSelectImage[1] = NotSelectImage2;
        NotSelectImage[2] = NotSelectImage3;
        NotSelectImage[3] = NotSelectImage4;


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
                SelectImage[i].enabled = true;
                InformationText[i].enabled = true;
            }
            else
            {
                SelectImage[i].enabled = false;
                InformationText[i].enabled = false;

                NotSelectImage[i].enabled = false;
                NotInformationText[i].enabled = false;
            }

        }

        //audiosource = GetComponent<AudioSource>();
        //audiosource.PlayOneShot(bgm);

    }

    // Update is called once per frame
    void Update()
    {

        //if (!audiosource.isPlaying)
        //{
        //    audiosource.PlayOneShot(bgm);
        //}

        for (int i = 0; i < 4; i++)
        {
            // iがModeSelectから1引いた値と同じ値なら、描画を行う
            if (i == LoadScript.ModeSelect - 1)
            {

                // イメージとTextの描画
                if (i > 0)
                {


                    if (i == 1)
                    {
                        if (ClearControlScript.EasyClear)
                        {
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }
                    else if (i == 2)
                    {

                        if (ClearControlScript.NormalClear)
                        {
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }
                    else if (i == 3)
                    {

                        if (ClearControlScript.HardClear)
                        {
                            SelectImage[i].enabled = true;
                            InformationText[i].enabled = true;
                        }
                        else
                        {
                            NotSelectImage[i].enabled = true;
                            NotInformationText[i].enabled = true;
                        }

                    }

                }
                else
                {
                    SelectImage[i].enabled = true;
                    InformationText[i].enabled = true;
                }

            }
            else
            {
                SelectImage[i].enabled = false;
                NotSelectImage[i].enabled = false;
                InformationText[i].enabled = false;
                NotInformationText[i].enabled = false;
            }


        }

    }


}
