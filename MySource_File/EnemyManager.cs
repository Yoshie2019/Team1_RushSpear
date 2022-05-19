using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    // 倒した敵の情報を取得し、ゲージ内に敵のアイコンを表示するスクリプト

    // 各種類の敵画像を格納するための変数
    [SerializeField] GameObject dangoImage1;
    [SerializeField] GameObject dangoImage2;
    [SerializeField] GameObject dangoImage3;

    // 敵画像を個々に格納するための配列
    GameObject[] dangoImage = new GameObject[3];

    // 敵を倒した際に加算されるポイント数を格納する変数
    public static int EnemyDestryCount;

    // 倒した際に流れる効果音を格納するための変数
    private AudioSource DestrySound;

    // Start is called before the first frame update
    void Start()
    {
        // 各種類の敵画像を配列へ格納
        dangoImage[0] = dangoImage1;
        dangoImage[1] = dangoImage2;
        dangoImage[2] = dangoImage3;

        // 初期値を設定
        EnemyDestryCount = 0;

        // 効果音を取得
        DestrySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 確認用デバッグログ
        Debug.Log("EnemyDestryCount:" + EnemyDestryCount);
    }

    // 敵を倒した際に敵アイコンを生成するための関数
    public void SpawnImage(int type)
    {
        // 倒した敵の種類をtypeから判別、typeの数値を要素数として扱い敵アイコンを生成
        GameObject d = Instantiate(dangoImage[type], transform);

        // 生成した敵アイコンを、一番最初の子オブジェクトにし、ゲージへ表示にさせる。
        d.transform.SetAsFirstSibling();

        // ポイントが100追加
        EnemyDestryCount += 100;

        // 効果音を鳴らす
        DestrySound.PlayOneShot(DestrySound.clip);

        // 確認用デバッグログ
        Debug.Log("playerhit" + EnemyDestryCount);

        // 子オブジェクトの個数を取得し、変数にcountへ格納
        int count = transform.childCount;

        // 子オブジェクトが3体以上いる場合
        if (count > 3)
        {
            // DestroyImmediateChildObject関数へ移行
            print("３以上");
            DestroyImmediateChildObject(count - 1);
        }
    }

    // 子オブジェクトを消すための関数
    void DestroyImmediateChildObject(int Count)
    {
        // 末尾の子オブジェクトを消す
        Destroy(transform.GetChild(Count).gameObject);
    }

}
