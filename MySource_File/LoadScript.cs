using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    // ステージ選択画面の入力処理を管理するためのスクリプト

    // 選択しているステージが何番目のステージかを把握するための変数
    public static int ModeSelect;
    public static int Stage;

    // 入力の誤差を回避するための変数
    bool SelectOn;
    bool SelectNow;

    // 効果音を格納するための変数
    private AudioSource SelectSound;

    // スティック入力中の時間を測定するための変数
    float SelectTime;

    // Start is called before the first frame update
    void Start()
    {
        // 最初はEasyを選択中
        ModeSelect = 1;
        SelectOn = false;
        SelectNow = true;

        // 効果音を取得
        SelectSound = GetComponent<AudioSource>();

        // 初期値を設定
        SelectTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Fキーを入力した場合、タイトル画面へ移動
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
            }

        }

        // 各ステージ選択中にBボタンの入力を行った際の処理
        if (Input.GetKeyDown("joystick button 1"))
        {
            GoStage();
        }

    }

    // ボタン入力した際に、選択したモードへ移行できる状態であるかを判定するための関数
    void GoStage()
    {

        if (ModeSelect == 1)
        {
            Stage = 1;
        }
        else if (ModeSelect == 2)
        {

            // Easyモードがクリア済みの場合
            if (ClearControlScript.EasyClear)
            {
                Stage = 2;
            }
            else
            {
                // クリアしていない場合、何も選択していない状態にする
                Stage = 0;
            }
        }
        else if (ModeSelect == 3)
        {
            // Normalモードがクリア済みの場合
            if (ClearControlScript.NormalClear)
            {
                Stage = 3;
            }
            else
            {
                // クリアしていない場合、何も選択していない状態にする
                Stage = 0;
            }
        }
        else if (ModeSelect == 4)
        {

            // Hardモードがクリア済みの場合
            if (ClearControlScript.HardClear)
            {
                Stage = 4;
            }
            else
            {
                // クリアしていない場合、何も選択していない状態にする
                Stage = 0;
            }
        }
        
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

    // 入力処理によって、選択中のモードを変更させる関数
    void Select(float lsH)
    {

        if ((lsH == 1)) // 右入力の場合
        {
        
            if (SelectNow)
            {
                // 効果音を流す
                SelectSound.PlayOneShot(SelectSound.clip);

                // 右端のモード選択中の場合
                if (ModeSelect == 4)
                {
                    // 左端のモードへ移動させる
                    ModeSelect = 1;
                }
                else
                {
                    // それ以外の場合は、右隣へずらす
                    ModeSelect++;
                }

                // セレクト画面内で、入力操作を行ったことを判別
                SelectOn = true;  
                SelectNow = false;

            }
        }
        else  // 左入力の場合
        {

            if (SelectNow)
            {

                // 効果音を流す
                SelectSound.PlayOneShot(SelectSound.clip);

                // 左端のモード選択中の場合
                if (ModeSelect == 1)
                {
                    // 右端のモードへ移動させる
                    ModeSelect = 4;
                }
                else
                {
                    // それ以外の場合は、左隣へずらす
                    ModeSelect--;
                }

                // セレクト画面内で、入力操作を行ったことを判別
                SelectOn = true;
                SelectNow = false;
            }

        }

        // 入力操作中の時間を測定
        if (!SelectNow)
        {
            SelectTime -= Time.deltaTime;
        }

        // 入力時間が長い分、入力に指定した方向へモード選択の表示がずれ続ける
        if (SelectTime < -0.1f)
        {
            SelectTime = 0f;
            SelectNow = true;
        }

    }

}
