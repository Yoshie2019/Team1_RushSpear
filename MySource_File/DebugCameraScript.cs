using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCameraScript : MonoBehaviour
{
    // デバッグ用カメラを管理するスクリプト
    Camera cam;
   
    // 各プレイヤーの情報を表示させるために使用するテキスト
    Text MagtnitudeText;        // プレイヤーのベクトルの大きさ用
    Text NormalizedText;        // プレイヤーのベクトルの方向用

    // Start is called before the first frame update
    void Start()
    {
        // カメラを取得
        cam = GetComponent<Camera>();

        // カメラの優先度を最低値に設定
        cam.depth = 0;

        // テキストを取得
        MagtnitudeText = GameObject.Find("MagtnitudeText").GetComponent<Text>();
        NormalizedText = GameObject.Find("NormalizedText").GetComponent<Text>();

        // テキストは初期段階、非表示にする
        MagtnitudeText.enabled = false;
        NormalizedText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Dキーを押した場合
        if (Input.GetKeyDown(KeyCode.D))
        {

            if(cam.depth == 0)
            {
                // カメラの優先度を最大値に設定
                cam.depth = 2;

                // テキストを表示
                MagtnitudeText.enabled = true;
                NormalizedText.enabled = true;
            }
            else
            {
                // 元の状態へ戻す
                cam.depth = 0;
                MagtnitudeText.enabled = false;
                NormalizedText.enabled = false;
            }
        }

    }

    // 表示するテキストを管理するための関数
    public void DebugText(Vector3 Input_vector, float Speed)
    {
        // 表示するテキストに、必要な情報を代入
        MagtnitudeText.text = "Magtnitude:" + Input_vector.magnitude;
        NormalizedText.text = "Normalized:" + Input_vector.normalized;
    }

}
