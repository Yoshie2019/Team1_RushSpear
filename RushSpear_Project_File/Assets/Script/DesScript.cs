using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Random.Range(0, 30), Random.Range(0, 60), Random.Range(0, 90) * Time.deltaTime));


        Destroy(this.gameObject, 8f);
    }
}
