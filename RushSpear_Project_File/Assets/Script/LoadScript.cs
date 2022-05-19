using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ステージ選択画面の入力処理を管理
public class LoadScript : MonoBehaviour
{

    // 選択しているステージが何番目のステージかを把握するための変数
    public static int ModeSelect;
    public static int Stage;

    // 入力の誤差を回避するための変数
    bool SelectOn;
    bool SelectNow;

    private AudioSource SelectSound;

    float SelectTime;

    // Start is called before the first frame update
    void Start()
    {
        // 最初はEasyを選択中
        ModeSelect = 1;
        SelectOn = false;
        SelectNow = true;

        SelectSound = GetComponent<AudioSource>();
        SelectTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Title");
        }

        //　選択画面に突入した直後にフェードインを行う
        FadeScript.FadeInPlease = true;

        // フェードインが終わったら、操作可能
        if (FadeScript.FadeInFinish)
        {

            // 入力の様子を管理するための変数
            float lsh = Input.GetAxis("L_Stick_H");

            // 十字キー(横)の入力が行われた場合
            if ((lsh != 0))
            {
                Select(lsh);
            }
            else
            {
                SelectOn = false;
                //SelectNow = true;
            }

        }

        // 各ステージ選択中にBボタンの入力を行った際の処理
        if (Input.GetKeyDown("joystick button 1"))
        {
            GoStage();
        }

        //　フェードアウトが完了したら
        //if (FadeScript.FadeOutFinish)
        //{
        //    FadeScript.FadeOutFinish = false;

        //    if (Stage == 1)
        //    {
        //        // Easyステージへ移動
        //        SceneManager.LoadScene("StageEasy");
        //    }
        //    else if (Stage == 2)
        //    {
        //        // Normalステージへ移動
        //        SceneManager.LoadScene("StageNormal");
        //    }
        //    else if (Stage == 3)
        //    {
        //        // Hardステージへ移動
        //        SceneManager.LoadScene("StageHard");
        //    }
        //    else if (Stage == 4)
        //    {
        //        // VeryHardステージへ移動
        //        SceneManager.LoadScene("StageVeryHard");
        //    }

        //}

    }

    void GoStage()
    {

        if (ModeSelect == 1)
        {
            Stage = 1;
        }
        else if (ModeSelect == 2)
        {

            if (ClearControlScript.EasyClear)
            {
                Stage = 2;
            }
            else
            {
                Stage = 0;
            }
        }
        else if (ModeSelect == 3)
        {

            if (ClearControlScript.NormalClear)
            {
                Stage = 3;
            }
            else
            {
                Stage = 0;
            }
        }
        else if (ModeSelect == 4)
        {

            if (ClearControlScript.HardClear)
            {
                Stage = 4;
            }
            else
            {
                Stage = 0;
            }
        }
        

        //if(Stage != 0)
        //{
        //    //　フェードアウト開始
        //    FadeScript.FadeOutPlease = true;
        //}

        if (Stage == 1)
        {
            // Easyステージへ移動
            SceneManager.LoadScene("StageEasy");
        }
        else if (Stage == 2)
        {
            // Normalステージへ移動
            SceneManager.LoadScene("StageNormal");
        }
        else if (Stage == 3)
        {
            // Hardステージへ移動
            SceneManager.LoadScene("StageHard");
        }
        else if (Stage == 4)
        {
            // VeryHardステージへ移動
            SceneManager.LoadScene("StageVeryHard");
        }

    }

    void Select(float lsH)
    {

        if (!SelectNow)
        {
            SelectTime -= Time.deltaTime;
        }
         
        if(SelectTime < -0.1f)
        {
            SelectTime = 0f;
            SelectNow = true;
        }

        if ((lsH == 1)) // 右入力の場合
        {

            if (SelectNow)
            {
                SelectSound.PlayOneShot(SelectSound.clip);

                if (ModeSelect == 4)
                {
                    ModeSelect = 1;
                }
                else
                {
                    ModeSelect++;
                }

                SelectOn = true;
                SelectNow = false;

            }
        }
        else  // 左入力の場合
        {

            if (SelectNow)
            {

                SelectSound.PlayOneShot(SelectSound.clip);

                if (ModeSelect == 1)
                {
                    ModeSelect = 4;
                }
                else
                {
                    ModeSelect--;
                }

                SelectOn = true;
                SelectNow = false;
            }

        }

        //}

    }

}
