using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSelectScript : MonoBehaviour
{
    private int Select;

    public Image ReTryImage;
    public Image StageSelectImage;

    bool NextSelectOn;
    bool NextSelectNow;

    //public Text ResultAText;
    //public Text ResultBText;
    //public Text ResultCText;
    //public Text ResultDText;

    //Text[] ResultText = new Text[4];

    public Image ResultAImage;
    public Image ResultBImage;
    public Image ResultCImage;
    public Image ResultDImage;

    Image[] ResultImage = new Image[4];

    int EnemyPoint;
    int TimePoint;
    int Total;
    int TotalPoint;

    int RemainingTime;

    public Text EnemyPointText;
    public Text TimePointText;
    public Text TotalPointText;

    private bool TotalOk;
    private bool RankOk;

    public static bool DebugResultOn;

    public AudioClip ResultSound2;
    public AudioClip SelectSound;
    private AudioSource audiosource;

    float SelectTime;

    // Start is called before the first frame update
    void Start()
    {
        // ↓アルファ版のため、一時的にコメントコメント化
        if (LoadScript.Stage == 1)
        {
            ClearControlScript.EasyClear = true;
        }
        else if (LoadScript.Stage == 2)
        {
            ClearControlScript.NormalClear = true;
        }
        else if (LoadScript.Stage == 3)
        {
            ClearControlScript.HardClear = true;
        }
        else if (LoadScript.Stage == 4)
        {
            ClearControlScript.VeryHardClear = true;
        }

        Select = 1;
        NextSelectOn = false;
        NextSelectNow = true;

        StageSelectImage.enabled = false;

        ResultImage[0] = ResultAImage;
        ResultImage[1] = ResultBImage;
        ResultImage[2] = ResultCImage;
        ResultImage[3] = ResultDImage;

        for (int i = 0; i < 4; i++)
        {
            ResultImage[i].enabled = false;
        }

        EnemyPoint = 0;
        TimePoint = 0;
        TotalPoint = 0;

        RemainingTime = TimeScript.intTime;

        TotalOk = false;
        RankOk = false;

        DebugResultOn = false;

        audiosource = GetComponent<AudioSource>();

        SelectTime = 0f;
        Total = 0;
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

                    //　総合得点数のけいさん
                    if (TotalPoint != (EnemyPoint + TimePoint))
                    {
                        TotalPoint += Total;
                        Debug.Log(TotalPoint);
                        TotalPointText.text = "" + TotalPoint;
                    }

                    //TotalPoint += 25;
                    //Debug.Log(TotalPoint);
                    //TotalPointText.text = "" + TotalPoint;
                }
                else if (TotalPoint == EnemyPoint + TimePoint)
                {
                    TotalOk = true;
                }

                if (TotalOk)
                {
                    //Invoke("RankOn", 1f);
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
                        GoReTry();
                    }
                }
                else if (StageSelectImage.enabled)
                {
                    if (Input.GetKeyDown("joystick button 1"))
                    {
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

    void GoReTry()
    {
        if (LoadScript.Stage == 1)
        {
            SceneManager.LoadScene("StageEasy");
        }
        else if (LoadScript.Stage == 2)
        {
            SceneManager.LoadScene("StageNormal");
        }
        else if (LoadScript.Stage == 3)
        {
            SceneManager.LoadScene("StageHard");
        }
        else if (LoadScript.Stage == 4)
        {
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

    void RankOn()
    {

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

        RankOk = true;

    }

    void NextSelect(float lsH)
    {

        if (!NextSelectNow)
        {
            SelectTime -= Time.deltaTime;
        }

        if (SelectTime < -0.3f)
        {
            SelectTime = 0f;
            NextSelectNow = true;
        }

        if (NextSelectNow)
        {
            audiosource.PlayOneShot(SelectSound);

            if (Select == 1)
            {
                Select = 2;
            }
            else if (Select == 2)
            {
                Select = 1;
            }

            NextSelectOn = true;
            NextSelectNow = false;
        }

    }

}
