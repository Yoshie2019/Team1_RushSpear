using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    Animator animator;

    private PlayerScript playerScript;

    float hold_time;
    bool hold_count = false;
    float defaultSpeed = 1.0f;
    [SerializeField] float sprearSpeed = 50f;

    public bool startCamera = true;
    float speartime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        playerScript = GetComponent<PlayerScript>();
        animator.SetFloat("Speed", defaultSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (startCamera == false /*StartCameraScript.swing_flag == 3*/)
            animator.SetTrigger("toFly");

        if (playerScript.toHold_flag == true)
        {
            playerScript.toHold_flag = false;
            animator.SetTrigger("toHold");
        }

        if (playerScript.toSpear_flag == true)
        {
            //animator.SetFloat("Speed", sprearSpeed);
            //Debug.Log("再生速度S：" + animator.speed);
            playerScript.toSpear_flag = false;
            animator.SetTrigger("toSpear");
        }

        //if (playerScript.toHold_flag == true)
        //{
        //    //animator.SetFloat("Speed", sprearSpeed);
        //    //Debug.Log("再生速度S：" + animator.speed);
        //    playerScript.toHold_flag = false;
        //    animator.SetTrigger("toSpear");
        //    hold_time = 0f;
        //    hold_count = true;
        //}

        //if (hold_time < 1.2)
        //{
        //    hold_time += Time.deltaTime;
        //}
        //else if (hold_time >= 1.2 && hold_count == true)
        //{
        //    animator.SetFloat("Speed", 0.0f);
        //    hold_count = false;
        //}

        //if (playerScript.toSpear_flag == true)
        //{
        //    playerScript.toSpear_flag = false;
        //    animator.SetFloat("Speed", defaultSpeed);
        //}

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spear"))
        //{
        //    speartime += Time.deltaTime;
        //}

        //Debug.Log("モーション時間：" + speartime);
    }
}