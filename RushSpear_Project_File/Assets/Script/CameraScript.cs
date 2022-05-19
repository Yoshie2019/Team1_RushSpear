using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject center;
    [SerializeField] float rotateSpeed = 4.0f; //カメラの回転速度
    float inputAngle;

    int rotate_R;
    int rotate_L;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //親になっているEmptyのPosをPlayerに合わせる
        center.transform.position = player.transform.position;

        //カメラ操作の入力を受け取る
        if (Input.GetKey("joystick button 4")||Input.GetKey(KeyCode.J))
            rotate_L = -1;
        else
            rotate_L = 0;

        if (Input.GetKey("joystick button 5")|| Input.GetKey(KeyCode.K))
            rotate_R = 1;
        else
            rotate_R = 0;

        inputAngle = Input.GetAxis("Horizontal") * rotateSpeed;
        inputAngle = (rotate_R + rotate_L) * rotateSpeed;


        //プレイヤーを軸に公転
        Vector3 playerPos = player.transform.position;
        transform.RotateAround(playerPos, Vector3.up, inputAngle);

    }
}
