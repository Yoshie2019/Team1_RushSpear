using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimeScript : MonoBehaviour
{
    // ゲーム本編内の制限時間と、ステージ上の敵の数を管理するためのスクリプト

    // 制限時間を設定するための変数
    public float time;

    // ゲーム本編が終了した時を判定するための変数
    public static bool GameFinish;

    // 制限時間をint型へ変換して扱うための変数
    public static int intTime;

    // 敵オブジェクト各種の情報を格納するための変数
    GameObject[] EnemyObjects;
    GameObject[] StopEnemyObjects;
    GameObject[] FixedEnemyObjects;
    GameObject[] FixedMoveEnemyObjects;

    // ステージ上の敵の合計数を格納するための変数
    int EnemyTotal;

    // 自分以外のメンバーが追加した変数
    public bool startCamera = true;
    bool countDS_flag = true;

    // 効果音を取得するための変数
    public AudioClip CountdownSound;
    public AudioClip FinishSound;
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        // 制限時間を設定
        time = 120.00f;

        // ゲーム本編終了時の判定を行うGameFinishをfalse
        GameFinish = false;

        // 各敵オブジェクトの情報を、タグ検索によって取得し、変数へ格納
        EnemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        StopEnemyObjects = GameObject.FindGameObjectsWithTag("StopEnemy");
        FixedEnemyObjects = GameObject.FindGameObjectsWithTag("FixedEnemy");
        FixedMoveEnemyObjects = GameObject.FindGameObjectsWithTag("FixedMoveEnemy");

        // 敵の合計数を格納
        EnemyTotal = (EnemyObjects.Length + StopEnemyObjects.Length + FixedMoveEnemyObjects.Length + FixedEnemyObjects.Length) * 100;
        Debug.Log("EnemyTotal:" + EnemyTotal);

        // 効果音を取得
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム本編が開始されている、かつポーズ画面が表示されていない場合
        if (StartCountDownScript.GameStart && !InputPauseScript.Pause)
        {

            // 制限時間の値を減らす
            time -= Time.deltaTime;

            // 制限時間表示用のテキストを取得
            TextMeshProUGUI TimeText = GetComponent<TextMeshProUGUI>();

            // float型変数のtimeを、int型へ変換
            intTime = Mathf.FloorToInt(time);

            // 制限時間をテキストで表示
            TimeText.text = "Time:" + intTime;

            // 制限時間の経過に合わせて、テキストの色が変化
            if (intTime < 77)
            {
                TimeText.color = new Color(255f, 255f, 0f, 255f);
            }

            if (intTime < 37)
            {
                TimeText.color = new Color(255f, 0f, 0f, 255f);
            }

            // 制限時間が0になる、またはEキーが入力される、または全ての敵が倒された場合
            if (intTime <= 0 || Input.GetKeyDown(KeyCode.E) || EnemyManager.EnemyDestryCount == EnemyTotal)  //End
            {
                // 本編終了判定にするため、GameFinishをtrueにする
                GameFinish = true;
                audiosource.PlayOneShot(FinishSound);
            }

        }

        if (startCamera == false/*StartCameraScript.swing_flag == 3*/) //←自分以外のメンバーが記述
        {
            if (countDS_flag)
            {
                audiosource.PlayOneShot(CountdownSound);
                countDS_flag = false;
            }
        }
    }

}
