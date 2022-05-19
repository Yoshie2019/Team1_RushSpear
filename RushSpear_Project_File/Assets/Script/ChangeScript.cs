using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeScript : MonoBehaviour
{

    GameObject[] EnemyObj;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject MoveEnemy;
    EnemyChangeScript EneChan;
    GameObject WallEnemy;

    public bool hit = false;
    Vector3 temp;
    Vector3[] temp2 = new Vector3[5];
    GameObject a;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        //EnemyObj[0] = GameObject.Find("WallEnemy0");
        //EnemyObj[1] = GameObject.Find("WallEnemy1");
        EnemyObj = GameObject.FindGameObjectsWithTag("WallEnemy");
        WallEnemy = GameObject.FindGameObjectWithTag("WallEnemy");
        EneChan = WallEnemy.GetComponent<EnemyChangeScript>();
        temp = WallEnemy.transform.position;

        for (i = 0; i < EnemyObj.Length; i++)
        {
            a = EnemyObj[i];
            temp2[i] = a.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //hit =EneChan.SpereHit;
        //Debug.Log(EneChan.s);
        if (hit)
        {
            Debug.Log("ららららい");
            temp = EneChan.pos;
            Invoke("DA", 0.5f);

            //Debug.Log(temp);
            //Debug.Log(temp2[0]);
            //Debug.Log(temp2[1]);
            //Debug.Log(EneChan.pos);
            //Destroy(EnemyObj[0]);
            EneChan.SpereHit = false;
            hit = false;
        }

    }

    void DA()
    {
        Debug.Log("これでうへい");
        Instantiate(MoveEnemy, temp2[0], Quaternion.identity);
        MoveEnemy.transform.LookAt(Player.transform);
    }
}
