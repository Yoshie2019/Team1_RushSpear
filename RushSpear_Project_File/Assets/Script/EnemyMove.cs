using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour　　//ファイルの共有・反映がしやすいので一つにまとめている
{
    [SerializeField] private float EnemySpeed = 1.0f;
    [SerializeField] private GameObject Enemy;

    private GameObject player;
    private Rigidbody rb;
    private Vector3 lastVelocity;

    public Vector3 Point1;
    public Vector3 Point2;
    public float time;

    private Vector3 deltaPos;
    private float elapsedTime;
    private bool hanbetu = true;
    private PlayerScript playerScript;
    public static bool playerHit = false;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private bool patternA = false;  //Enemy
    private bool patternB = false;  //FixedMoveEnemy
    private bool patternC = false;  //FixedEnemy
    private bool patternD = false;  //FixedEnemy
    private Vector3 pos;

    private Transform po1;
    private Transform po2;
    private Transform po3;
    private Transform po4;

    public GameObject HitEffect;
    private bool hita = false;
    LayerMask layer;
    int change = 0;
    //↓築花の追筆
    private EnemyManager enemyManager;
    //↑築花の追筆
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<PlayerScript>();

        layer = this.gameObject.layer;
        if (this.gameObject.CompareTag("Enemy"))
        {
            patternA = true;
            transform.LookAt(player.transform);
            Invoke("ChangeLayer", 2f);//2秒間の無敵時間
        }

        if (this.gameObject.CompareTag("FixedMoveEnemy")&&this.gameObject.name!="FE1(Clone)" && this.gameObject.name != "FE2(Clone)")
        {
            patternB = true;
         
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            GotoNextPoint();
            
            //固定動き代替え案
            //transform.position = Point1;
            //deltaPos = (Point2 - Point1) / time;
            //elapsedTime = 0;
            Invoke("ChangeLayer", 2f);//2秒間の無敵時間
        }

        if (this.gameObject.CompareTag("FixedEnemy"))
        {
            patternC = true;
            Invoke("ChangeLayer", 2f);//2秒間の無敵時間
            Debug.Log("dfreyufheu");
        }

        if (this.gameObject.CompareTag("WallEnemy"))
        {
            pos = this.gameObject.transform.position;
        }


        //if (layer == 12)
        //{

        //}

        if (this.gameObject.name == "FE1(Clone)"|| this.gameObject.name == "FE2(Clone)")
        {
            Debug.Log("5464564");
            patternD = true;
            //Point1 = new Vector3(23, -1, -86);
            //Point2 = new Vector3(-49, -1, -49);
            //固定動き代替え案
            transform.position = Point1;
            deltaPos = (Point2 - Point1) / time;
            elapsedTime = 0;
            Invoke("ChangeLayer", 2f);//2秒間の無敵時間
        }




        //↓築花の追筆
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        //↑築花の追筆
        //Debug.Log("はじまり");
    }

    // Update is called once per frame
    void Update()
    {
        


        //↓築花の追筆
        if (StartCountDownScript.GameStart && !InputPauseScript.Pause)
        {//↑築花の追筆

            if (patternA)
            {

                if (rb.velocity.magnitude < 50.0f)
                {
                    rb.AddForce(transform.forward * 5.0f * EnemySpeed, ForceMode.Force);
                }

                this.lastVelocity = rb.velocity;

            }

            if (patternB)
            {
                agent.enabled = true;
                //this.transform.LookAt(player.transform);
                //transform.position += deltaPos * Time.deltaTime;
                //elapsedTime += Time.deltaTime;

                //if (elapsedTime > time)
                //{
                //    if (hanbetu)
                //    {
                //        deltaPos = (Point1 - Point2) / time;
                //        transform.position = Point2;
                //    }
                //    else
                //    {
                //        deltaPos = (Point2 - Point1) / time;

                //        transform.position = Point1;
                //    }

                //    hanbetu = !hanbetu;

                //    elapsedTime = 0;
                //}

                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
            }

            if (patternC)
            {
                this.transform.LookAt(player.transform);
            }

            if (patternD)
            {
                this.transform.LookAt(player.transform);
                transform.position += deltaPos * Time.deltaTime;
                elapsedTime += Time.deltaTime;

                if (elapsedTime > time)
                {
                    if (hanbetu)
                    {
                        deltaPos = (Point1 - Point2) / time;
                        transform.position = Point2;
                    }
                    else
                    {
                        deltaPos = (Point2 - Point1) / time;

                        transform.position = Point1;
                    }

                    hanbetu = !hanbetu;

                    elapsedTime = 0;
                }
            }

            if (hita)
            {
                hita = false;
                Instantiate(Enemy, pos, Quaternion.identity);
                Destroy(this.gameObject);
            }




        }  //↓築花の追筆
        else if (InputPauseScript.Pause || !StartCountDownScript.GameStart)
        {
            
            if (patternA)//←林
            {
                this.rb.velocity = Vector3.zero;//←築花
            }

            //↑築花の追筆
            //↓林
            if (patternB)
            {
                agent.enabled = false;
            }
           

        }

    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;


        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }

    void ChangeLayer()
    {
        //if (patternA)
        //{
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        //}
        //else if (patternB)
        //{
        //    this.gameObject.layer = LayerMask.NameToLayer("FixedMoveEnemy");
        //}
        //else if (patternC)
        //{
        //    this.gameObject.layer = LayerMask.NameToLayer("FixedEnemy");
        //}
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall"))  //壁に衝突したとき(瞬間)
        {
            Debug.Log("WallHit");　　//確認用
            rb.velocity = Vector3.zero;  //止める
            this.transform.LookAt(player.transform);　//プレイヤー
            Debug.Log("じぇいｆｊ");

        }
        else if (collision.gameObject.CompareTag("WallEnemy"))
        {
            Vector3 reflectedV = Vector3.Reflect(this.lastVelocity, collision.contacts[0].normal);  //Playerのを流用
            //Vector3のベクトルをLogで表示させる。確認させる。
            Debug.Log(reflectedV);
            this.rb.velocity = reflectedV;
            //↑直でvelocityに反映させるのは危ない。
            transform.rotation = Quaternion.LookRotation(reflectedV); //進行方向を向かせる


            if (rb.velocity.magnitude < 100.0f)
            {
                rb.AddForce(transform.forward * 20.0f * EnemySpeed, ForceMode.Force);
            }

        }

        if (collision.gameObject.name == "player" && this.gameObject.CompareTag("WallEnemy"))
        {
            hita = true;
            //Destroy(this.gameObject,0.1f);
        }

    }

    private void OnTriggerEnter(Collider other)      //playerに対する判定
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerHit = true;

            //↓築花の追筆
            if (this.gameObject.CompareTag("FixedMoveEnemy"))
            {
                enemyManager.SpawnImage(0);
            }
            else if (this.gameObject.CompareTag("Enemy"))
            {
                enemyManager.SpawnImage(1);
            }
            else if (this.gameObject.CompareTag("FixedEnemy"))
            {
                enemyManager.SpawnImage(2);
            }
            //↑築花の追筆


            if (patternA || patternB || patternC||patternD)
            {
                Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }




        }
    }



}