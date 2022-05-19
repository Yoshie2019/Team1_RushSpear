using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsHanaScript : MonoBehaviour
{
    public GameObject hanabira;
    public GameObject hanabira2;
    public GameObject ba;
    Transform tm;
    private float time = 0;
    private float limit = 0.1f;
    private float x = 0;
    private float y = 30;
    private float z = 0;
    int a = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //tm = ba.transform;
        time += Time.deltaTime;
        if (time >= limit)
        {
            x = Random.Range(-84, 200);
            z = Random.Range(-160, 160);
            ba.transform.Translate(x, y, z);
            tm = ba.transform;

            if (a == 1)
            {
                Instantiate(hanabira, new Vector3(x, y, z), Quaternion.identity);
                a = 2;

            }
            else if (a == 2)
            {
                Instantiate(hanabira2, new Vector3(x, y, z), Quaternion.identity);
                a = 1;
            }

            time = 0;
        }
    }
}
