using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ポーズ中の描画の処理
public class PauseUiScript : MonoBehaviour
{
    
    public Image PauseImage0;
    public Image PauseImage1;
    public TextMeshProUGUI PauseText;

    public Image RetryImage;
    public TextMeshProUGUI RetryText;

    public Image ReturnImage;
    public TextMeshProUGUI ReturnText;

    public Image GoStartImage;
    public TextMeshProUGUI GoStartText;

    public Image SelectImage1;
    public Image SelectImage2;
    public Image SelectImage3;

    Image[] SelectImage = new Image[3];

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

        SelectImage[0] = SelectImage1;
        SelectImage[1] = SelectImage2;
        SelectImage[2] = SelectImage3;

        NotSelectImage[0] = RetryImage;
        NotSelectImage[1] = ReturnImage;
        NotSelectImage[2] = GoStartImage;

        for(int i = 0;i < 3; i++)
        {
            SelectImage[i].enabled = false;
            NotSelectImage[i].enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (InputPauseScript.Pause)
        {
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
                    SelectImage[i].enabled = true;
                    NotSelectImage[i].enabled = false;
                }
                else
                {
                    SelectImage[i].enabled = false;
                    NotSelectImage[i].enabled = true;
                }

            }

        }
        else
        {
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
