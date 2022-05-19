using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//ゲーム本編内の制限時間
public class TimeScript : MonoBehaviour
{
    public float time;

    public static bool GameFinish;

    public static int intTime;

    GameObject[] EnemyObjects;
    GameObject[] StopEnemyObjects;
    GameObject[] FixedEnemyObjects;
    GameObject[] FixedMoveEnemyObjects;

    int EnemyTotal;

    public bool startCamera = true;
    bool countDS_flag = true;
    public AudioClip CountdownSound;
    public AudioClip FinishSound;
    private AudioSource audiosource;

    //public AudioClip CountDownSound;
    //private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        //StartCount = 3.00f;
        time = 120.00f;

        GameFinish = false;

        EnemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        StopEnemyObjects = GameObject.FindGameObjectsWithTag("StopEnemy");
        FixedEnemyObjects = GameObject.FindGameObjectsWithTag("FixedEnemy");
        FixedMoveEnemyObjects = GameObject.FindGameObjectsWithTag("FixedMoveEnemy");

        EnemyTotal = (EnemyObjects.Length + StopEnemyObjects.Length + FixedMoveEnemyObjects.Length + FixedEnemyObjects.Length) * 100;
        Debug.Log("EnemyTotal:" + EnemyTotal);

        audiosource = GetComponent<AudioSource>();

        //audiosource = GetComponent<AudioSource>();
        //audiosource.PlayOneShot(CountDownSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCountDownScript.GameStart && !InputPauseScript.Pause)
        {

            time -= Time.deltaTime;

            //Text TimeText = GetComponent<Text>();
            TextMeshProUGUI TimeText = GetComponent<TextMeshProUGUI>();
            intTime = Mathf.FloorToInt(time);
            TimeText.text = "Time:" + intTime;

            if (intTime < 77)
            {
                TimeText.color = new Color(255f, 255f, 0f, 255f);
                //TimeText.color = new Color(255f, 165f, 0f, 255f);
            }

            if (intTime < 37)
            {
                TimeText.color = new Color(255f, 0f, 0f, 255f);
            }

            if (intTime <= 0 || Input.GetKeyDown(KeyCode.E) || EnemyManager.EnemyDestryCount == EnemyTotal)  //End
            {
                GameFinish = true;
                audiosource.PlayOneShot(FinishSound);
            }

            //if (intTime < 77 && intTime > 36)
            //{
            //    TimeText.color = new Color(255f, 165f, 0f, 255f);
            //}

            //if (intTime < 37)
            //{
            //    TimeText.color = new Color(255f, 0f, 0f, 255f);
            //}

        }

        //if (startCamera == true)
        //{
        //    if (StartCameraScript.swing_flag == 3)
        //    {
        //        startCamera = false;
        //    }
        //}

        if (startCamera == false/*StartCameraScript.swing_flag == 3*/) //←森阪記述
        {
            if (countDS_flag)
            {
                audiosource.PlayOneShot(CountdownSound);
                countDS_flag = false;
            }
        }
    }

}
