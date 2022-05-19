using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject MoveEnemy;
    ChangeScript Chan;
    //public GameObject obj = (GameObject)Resources.Load("Enemy");
    public bool SpereHit = false;
    public Vector3 pos;
    public int s;
    int i = 0;
    public GameObject EnemyChan;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = (GameObject)Resources.Load("Enemy");
        //temp = this.gameObject.transform;
        Chan = EnemyChan.GetComponent<ChangeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpereHit)
        {
            //Instantiate(MoveEnemy,new Vector3(0,0,0),Quaternion.Euler(0,0,1));
            //MoveEnemy.transform.LookAt(Player.transform); 

            //if (this.gameObject == GameObject.Find("WallEnemy0"))
            //{
            //    s = 0;
            //}
            //else if(this.gameObject == GameObject.Find("WallEnemy1"))
            //{
            //    s = 1;
            //}

            Chan.hit = true;
            pos = this.gameObject.transform.position;
            Debug.Log(pos);
            //Destroy(this.gameObject);
            Destroy(this.gameObject, 0.5f);
            //Invoke("AAA",1f);

            SpereHit = false;
        }
    }

    private void OnTriggerEnter(Collider other)      //playerに対する判定、だがこれはプレイヤー側に実装するべきかこちらにするべきか(part2)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpereHit = true;
            //this.tag = "Enemy";
            Debug.Log("playerhit2");
        }
    }

    //void AAA()
    //{
    //    Destroy(this.gameObject, 1f);
    //}

}