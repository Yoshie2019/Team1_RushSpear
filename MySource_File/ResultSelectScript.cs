using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSelectScript : MonoBehaviour
{
    // リザルト画面内の処理を行うためのスクリプト

    // 選択中の項目を把握するための変数
    private int Select;

    // 選択できる各項目を表すための画像
    public Image ReTryImage;
    public Image StageSelectImage;

    // 入力が行われているかを確認するための変数
    bool NextSelectOn;
    bool NextSelectNow;

    // スコア結果用の画像
    public Image ResultAImage;
    public Image ResultBImage;
    public Image ResultCImage;
    public Image ResultDImage;

    // スコア画像を格納するための配列
    Image[] ResultImage = new Image[4];

    // 敵を倒した分だけ加算されるポイントを格納するための変数
    int EnemyPoint;

    // 残った制限時間の分だけ加算されるポイントを格納するための変数
    int TimePoint;

    // ポイントの合計値を格納するための変数
    int Total;
    int TotalPoint;

    // 残った制限時間の値を格納するための変数
    int RemainingTime;

    // 各ポイントを表示するために扱うテキスト
    public Text EnemyPointText;
    public Text TimePointText;
    public Text TotalPointText;

    // 計算演出の完了具合を確認するための変数
    private bool TotalOk;
    private bool RankOk;
    public static bool DebugResultOn;

    // 効果音を鳴らすための変数
    public AudioClip ResultSound2;
    public AudioClip SelectSound;
    private AudioSource audiosource;

    // 入力中の時間を計測するための変数
    float SelectTime;

    // Start is called before the first frame update
    void Start()
    {

        // 終了したステージの種類を判別
        if (LoadScript.Stage == 1)
        {
            // Easyステージクリアの状態にする
            ClearControlScript.EasyClear = true;
        }
        else if (LoadScript.Stage == 2)
        {
            // Normalステージクリアの状態にする
            ClearControlScript.NormalClear = true;
        }
        else if (LoadScript.Stage == 3)
        {
            // Hardステージクリアの状態にする
            ClearControlScript.HardClear = true;
        }
        else if (LoadScript.Stage == 4)
        {
            // VeryHardステージクリアの状態にする
            ClearControlScript.VeryHardClear = true;
        }

        // ステージ設定をリセット
        Select = 1;

        // 
        NextSelectOn = false;
        NextSelectNow = true;

        // 扱う画像を全て非表示
        StageSelectImage.enabled = false;

        ResultImage[0] = ResultAImage;
        ResultImage[1] = ResultBImage;
        ResultImage[2] = ResultCImage;
        ResultImage[3] = ResultDImage;

        for (int i = 0; i < 4; i++)
        {
            ResultImage[i].enabled = false;
        }

        // 変数の初期値を設定
        EnemyPoint = 0;
        TimePoint = 0;
        TotalPoint = 0;

        SelectTime = 0f;
        Total = 0;

        // 残った制限時間の値を格納
        RemainingTime = TimeScript.intTime;

        // 処理判定用変数をfalse
        TotalOk = false;
        RankOk = false;

        DebugResultOn = false;

        // 効果音を取得
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!DebugResultOn)
        {

            // 本データ用
            if (!RankOk)
            {
                //　計算中の処理

                if (EnemyPoint != EnemyManager.EnemyDestryCount)
                {
                    //　敵の撃破数分の計算
                    EnemyPoint += 100;
                    EnemyPointText.text = "" + EnemyPoint;
                }
                else if (EnemyPoint == EnemyManager.EnemyDestryCount && TimePoint != (RemainingTime * 125))
                {
                    //  残り時間分の計算
                    TimePoint += 125;
                    TimePointText.text = "" + TimePoint;
                }
                else if (EnemyPoint == EnemyManager.EnemyDestryCount && TimePoint == (RemainingTime * 125) && TotalPoint != EnemyPoint + TimePoint)
                {

                    Total = (EnemyPoint + TimePoint) / 5;

                    //　総合得点数の計算
                    if (TotalPoint != (EnemyPoint + TimePoint))
                    {
                        TotalPoint += Total;
                        Debug.Log(TotalPoint);
                        TotalPointText.text = "" + TotalPoint;
                    }

                }
                else if (TotalPoint == EnemyPoint + TimePoint)
                {
                    // 総合得点の計算完了
                    TotalOk = true;
                }

                if (TotalOk)
                {
                    // 総合得点の計算完了後、ランク判定へ移行
                    RankOn();
                }

            }
            else
            {

                // 計算が完了していたら操作可能
                float lsh = Input.GetAxis("L_Stick_H");

                if ((lsh != 0))
                {
                    NextSelect(lsh);
                }
                else
                {
                    NextSelectOn = false;
                    NextSelectNow = true;
                }

                if (NextSelectOn && TotalOk)
                {

                    if (Select == 1)
                    {
                        StageSelectImage.enabled = false;
                        ReTryImage.enabled = true;
                        NextSelectOn = false;
                    }
                    else if (Select == 2)
                    {
                        ReTryImage.enabled = false;
                        StageSelectImage.enabled = true;
                        NextSelectOn = false;
                    }

                }

                
                if (ReTryImage.enabled)
                {
                    if (Input.GetKeyDown("joystick button 1"))
                    {
                        // リトライ項目を選択中、かつボタン入力が行われた場合
                        GoReTry();
                    }
                }
                else if (StageSelectImage.enabled)
                {
                    if (Input.GetKeyDown("joystick button 1"))
                    {
                        // ステージセレクト項目を選択中、かつボタン入力が行われた場合
                        GoStageSelect();
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                DebugResultOn = true;
            }

        }

    }

    // リトライ項目を選択し、入力した場合の処理を行うための変数
    void GoReTry()
    {

        if (LoadScript.Stage == 1)
        {   
            // 最後にプレイしたステージがEasyステージの場合、Easyステージへ移動
            SceneManager.LoadScene("StageEasy");
        }
        else if (LoadScript.Stage == 2)
        {
            // 最後にプレイしたステージがNormalステージの場合、Normalステージへ移動
            SceneManager.LoadScene("StageNormal");
        }
        else if (LoadScript.Stage == 3)
        {
            // 最後にプレイしたステージがHardステージの場合、Hardステージへ移動
            SceneManager.LoadScene("StageHard");
        }
        else if (LoadScript.Stage == 4)
        {
            // 最後にプレイしたステージがVeryHardステージの場合、VeryHardステージへ移動
            SceneManager.LoadScene("StageVeryHard");
        }
        else if (LoadScript.Stage == 5)
        {
            SceneManager.LoadScene("StageSecret");
        }
    }

    // ステージセレクト画面へ移動
    void GoStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    // 合計ポイントから、ランクを判別するための変数
    void RankOn()
    {
        // 効果音を鳴らす
        audiosource.PlayOneShot(ResultSound2);

        if (TotalPoint > 9999)
        {
            ResultImage[0].enabled = true;
        }
        else if (TotalPoint < 10000 && TotalPoint > 5499)
        {
            ResultImage[1].enabled = true;
        }
        else if (TotalPoint < 10000 && TotalPoint > 5499)
        {
            ResultImage[2].enabled = true;
        }
        else
        {
            ResultImage[3].enabled = true;
        }

        // ランク判定完了
        RankOk = true;

    }

    // リザルト画面内の入力に関する処理のための関数
    void NextSelect(float lsH)
    {

        // 入力操作中の時間を測定
        if (!NextSelectNow)
        {
            SelectTime -= Time.deltaTime;
        }

        // 入力時間が長い分、入力に指定した方向へモード選択の表示がずれ続ける
        if (SelectTime < -0.3f)
        {
            SelectTime = 0f;
            NextSelectNow = true;
        }


        if (NextSelectNow)
        {
            // 効果音を流す
            audiosource.PlayOneShot(SelectSound);

            // 入力処理に応じて、選択中の項目がずれる
            if (Select == 1)
            {
                Select = 2;
            }
            else if (Select == 2)
            {
                Select = 1;
            }

            // 画面内で、入力操作を行ったことを判別
            NextSelectOn = true;
            NextSelectNow = false;
        }

    }

}
