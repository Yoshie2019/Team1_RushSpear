using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public Transform target = null;
    private Vector3 velocity;
    public float speed = 5;
    public float attenuation = 0.5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(target.transform);
        //rb.AddForce(transform.forward * 10.0f, ForceMode.Force);
        velocity += (target.position - transform.position) * speed;
        //m_velocity *= attenuation;
        transform.position += velocity *= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Result");
        }
    }
}
