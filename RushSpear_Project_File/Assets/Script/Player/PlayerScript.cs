using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //・スピア関連変数
    [SerializeField] float initV = 2000;        //初期、常時のスピード
    [SerializeField] float deltaPower = 200;   //加減速度
    [SerializeField] float spearCoolTime = 3;   //スピア加速のクールタイム
    float spear_elapsedTime;                    //スピア入力後の経過時間
    [SerializeField] int holdInterval = 3;      //次の壁張り付きまでに必要な反射回数
    [SerializeField] float holdTimelimite = 3;  //壁に張り付いていられる秒数
    float hold_elapsedTime;                     //壁張り付き経過時間
    bool holdW_flag = false;
    bool releaseW_flag = true;
    public bool toHold_flag = false;
    public bool toSpear_flag = false;

    [SerializeField] Rigidbody rb;
    private Vector3 lastVelocity;

    //ガイドライン//--------------------------------//
    [SerializeField] GameObject pGuideline_prefab;
    [SerializeField] GameObject rGuideline_prefab;
    [SerializeField] GameObject aGuideline_prefab;
    //---------------------------------------------//
    private RaycastHit Hit_p; //プレイヤーからのレイ着地点
    private RaycastHit Hit_r; //反射先のレイ着地点
    float reflect_rad_normal;
    float select_rad;
    int reflect_count; //壁加速後の反射回数
    bool holdLine_flag = false;　//

    //Lスティックからの入力
    float input_h;
    float input_v;
    Vector3 natural_vector;  //自然反射のベクトル
    Vector3 newInput_vector;
    Vector3 input_vector;    //プレイヤーへ反映させるベクトル
    //.magnitude(大きさ）.normalized(単位ベクトル)を表す
    Vector3 accel_vector;

    bool p_reflect_flag = true; //反射後のフレームか判断

    int layerMask = 1 << 10 | 1 << 11; //レイヤー10番、11番のみRayCastを処理

    int InitinayVelocity;
    bool pause_flag = false;

    public static int EnemyDestryCount;

    //オーディオ//------------------------//
    public AudioClip SpeedUpSound;
    public AudioClip WallSound;
    private AudioSource audiosource;
    //-----------------------------------//
    bool returnPause_oncetime = false;

    public TextMeshProUGUI ReflectAgainTimeText;
    private int reflectAgainCount;

    private DebugCameraScript debugcameraScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitinayVelocity = 0;
        reflect_count = holdInterval + 1;
        EnemyDestryCount = 0;
        audiosource = GetComponent<AudioSource>();

        debugcameraScript = GameObject.Find("DebugCamera").GetComponent<DebugCameraScript>();

        ReflectAgainTimeText.enabled = true;
        reflectAgainCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy撃破カウント" + EnemyDestryCount);

        //築花、森阪 記述------------------------------------------------------------------------------//
        reflectAgainCount = holdInterval + 1 - reflect_count;

        if (reflect_count >= holdInterval + 1)
        {
            ReflectAgainTimeText.text = "HoldOK!";
        }
        else
        {
            ReflectAgainTimeText.text = "" + reflectAgainCount;
        }

        //スタートカウントダウン、ポーズ中に止める
        if (StartCountDownScript.GameStart && !InputPauseScript.Pause)
        {
            if (InitinayVelocity == 0)
            {
                rb.AddForce(transform.forward * initV, ForceMode.Force);
                InitinayVelocity++;
            }
            else if (pause_flag == true)
            {
                rb.AddForce(transform.forward * lastVelocity.magnitude * 50, ForceMode.Force);
                pause_flag = false;
            }
        }
        else if (InputPauseScript.Pause || !StartCountDownScript.GameStart)
        {
            this.rb.velocity = Vector3.zero;
            pause_flag = true;
        }
        //---------------------------------------------------------------------------------------//

        //・デバッグ用、加速
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(">>>");
            rb.AddForce(transform.forward * initV, ForceMode.Force);
        }

        debugcameraScript.DebugText(rb.velocity, initV);

        //・Sキー or Bボタン でスピア
        Spear();

        // 空気抵抗がある状態でinitV以下になったら空気抵抗off
        if (this.rb.velocity.magnitude <= initV / 50.0f && this.rb.drag > 0)
            this.rb.drag = 0;
        //Debug.Log("プレイヤーのベクトル量：" + this.rb.velocity.magnitude);

        spear_elapsedTime += Time.deltaTime;

        if (StartCountDownScript.GameStart && holdLine_flag != true)
            DrawPlayerGuideline();
        else if (holdLine_flag == true)
            DrawAccelGuideline();

        if (p_reflect_flag == true && StartCountDownScript.GameStart)
        {
            //反射毎に1度だけ自然反射のベクトルを代入
            natural_vector = Vector3.Reflect(transform.forward, Hit_p.normal);
            input_vector = natural_vector;

            //natural_vと壁の法線とのなす角を取得
            reflect_rad_normal = Vector3.SignedAngle(Hit_p.normal, natural_vector, Vector3.up);
            Debug.Log("角度" + reflect_rad_normal);
            select_rad = reflect_rad_normal; //ラインの制限

            p_reflect_flag = false;
        }

        //////--
        if (Input.GetKeyDown(KeyCode.R))
            Debug.Log("操作角：" + select_rad);
        //////--

        //反射方向の指定
        SelectAngle();

        //反射後のガイドライン表示
        if (holdLine_flag != true)
            DrawReflectGuideline();

        //壁ホールド
        HoldWall();

        if (pause_flag == false) //ポーズ中は更新しない
        {
            this.lastVelocity = rb.velocity;
        }
    }


    //接触判定処理//================================================================================================//
    private void OnCollisionEnter(Collision collision)
    {
        //反射の計算
        Reflect(collision.contacts[0].normal);

        //壁ホールド
        if (holdW_flag == true && reflect_count > holdInterval && collision.gameObject.CompareTag("Wall"))
        {
            toHold_flag = true;
            releaseW_flag = false;
            holdLine_flag = true;
            hold_elapsedTime = 0;
            accel_vector = input_vector;
            this.rb.velocity = Vector3.zero;
        }
        else
        {
            p_reflect_flag = true;
            reflect_count++;
        }

        //↓築花の追筆
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("WallEnemy"))
        {
            //壁、壁敵に衝突したら
            audiosource.PlayOneShot(WallSound);
        }
    }
    //============================================================================================================//

    //スピアによる加速//============================================================================================//
    void Spear()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown("joystick button 1"))
            && (spear_elapsedTime > spearCoolTime) && releaseW_flag == true && !InputPauseScript.Pause)
        {
            spear_elapsedTime = 0.0f; //リセット

            this.rb.velocity = this.rb.velocity.normalized * deltaPower;
            this.rb.drag = 0.5f;　//空気抵抗->on
            Debug.Log("加速！");

            //↓築花の追筆//---------------------------//
            if (InputPauseScript.PauseOutTime != 0f)
            {
                audiosource.PlayOneShot(SpeedUpSound);
            }
            //---------------------------------------//
        }
    }
    //============================================================================================================//

    //反射先の計算//================================================================================================//
    void SelectAngle()
    {
        if (Input.GetAxis("L_Stick_H") > 0 && select_rad > (-90 + 2.25f))
        {
            input_h = 0.0125f;
            select_rad -= 2.25f;
        }
        else if (Input.GetAxis("L_Stick_H") < 0 && select_rad < (90 - 2.25f))
        {
            input_h = -0.0125f;
            select_rad += 2.25f;
        }
        else
        {
            input_h = 0;
        }

        //入力に応じてReflectLineの計算
        newInput_vector.x = natural_vector.x * Mathf.Cos(input_h * Mathf.PI) - natural_vector.z * Mathf.Sin(input_h * Mathf.PI);
        newInput_vector.z = natural_vector.x * Mathf.Sin(input_h * Mathf.PI) + natural_vector.z * Mathf.Cos(input_h * Mathf.PI);

        if ((Input.GetAxis("L_Stick_H") != 0) /*|| (Input.GetAxis("L_Stick_V") != 0)*//*Input.GetAxis("Horizontal")!=0*/)
        {
            natural_vector = newInput_vector;
            input_vector = newInput_vector;
        }
    }
    //============================================================================================================//

    //・反射処理//=================================================================================================//
    void Reflect(Vector3 wallV_normal)
    {
        if (holdLine_flag == true) //壁加速後の反射
        {
            holdLine_flag = false;
            input_vector = Vector3.Reflect(this.lastVelocity, wallV_normal);
            input_vector = input_vector.normalized * initV / 50;
        }
        else //普通の壁反射
        {
            //Vector3 input_v_normal = input_vector.normalized;
            if (this.rb.velocity.magnitude < initV / 50)
            {
                input_vector = input_vector.normalized * this.lastVelocity.magnitude;
                Debug.Log("止まったよ～ん");
            }
            else
            {
                input_vector = input_vector.normalized * this.rb.velocity.magnitude;
                //Debug.Log("a" + this.rb.velocity.magnitude);
            }
        }

        this.rb.velocity = input_vector;
        transform.rotation = Quaternion.LookRotation(input_vector); //進行方向を向かせる
    }
    //===========================================================================================================//

    //・壁張り付き処理//===========================================================================================//
    void HoldWall()
    {
        if (Input.GetKey("joystick button 0") || Input.GetKey("joystick button 3"))
        {
            holdW_flag = true;
        }
        else
        {
            holdW_flag = false;
        }

        if (holdW_flag == true && releaseW_flag == false)
        {
            accel_vector = input_vector;
            transform.rotation = Quaternion.LookRotation(accel_vector);
        }

        if ((holdW_flag == false || hold_elapsedTime >= holdTimelimite) && releaseW_flag == false)
        {
            toSpear_flag = true;
            releaseW_flag = true;
            reflect_count = 0;
            rb.AddForce(transform.forward * initV * 5, ForceMode.Force); //*三つめが加速数
        }

        hold_elapsedTime += Time.deltaTime;
    }
    //===========================================================================================================//

    //・進行中のガイドラインの描画//================================================================================//
    void DrawPlayerGuideline()
    {
        if (Physics.Raycast(this.transform.position, transform.forward, out Hit_p, Mathf.Infinity, layerMask))
        {

            GameObject guideline_p = Instantiate(pGuideline_prefab, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer line_p = guideline_p.GetComponent<LineRenderer>();
            line_p.SetPosition(0, this.transform.position);
            line_p.SetPosition(1, Hit_p.point);
        }
    }
    //===========================================================================================================//

    //・狙い撃ちのガイドラインの描画//==============================================================================//
    void DrawAccelGuideline()
    {
        if (Physics.Raycast(this.transform.position, accel_vector, out Hit_p, Mathf.Infinity, layerMask))
        {

            GameObject guideline_p = Instantiate(aGuideline_prefab, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer line_p = guideline_p.GetComponent<LineRenderer>();
            line_p.SetPosition(0, this.transform.position);
            line_p.SetPosition(1, Hit_p.point);
        }
    }
    //===========================================================================================================//

    //・反射後のガイドラインの描画//================================================================================//
    void DrawReflectGuideline()
    {
        if (Physics.Raycast(Hit_p.point, input_vector, out Hit_r, Mathf.Infinity, layerMask))
        {

            GameObject guideline_r = Instantiate(rGuideline_prefab, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer line_p = guideline_r.GetComponent<LineRenderer>();
            line_p.SetPosition(0, Hit_p.point);
            line_p.SetPosition(1, Hit_r.point);
        }
    }
    //===========================================================================================================//
}