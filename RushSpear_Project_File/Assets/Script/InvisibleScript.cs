using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class InvisibleScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<string> coverLayerNameList_;
    private int layerMask;

    public List<Renderer> rendererHitsList = new List<Renderer>();
    public List<GameObject> gameObjectsHitList = new List<GameObject>();

    public Renderer[] rendererHits;
    public GameObject[] gameObjectsHits;
    public GameObject temple;

    void Start()
    {
        // 遮蔽物のレイヤーマスクを、レイヤー名のリストから合成する。
        layerMask = 0;

        foreach (string _layerName in coverLayerNameList_)
        {
            layerMask |= 1 << LayerMask.NameToLayer(_layerName);
        }

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 difference = (player.transform.position - this.transform.position);
        Vector3 direction = difference.normalized;
        Ray ray = new Ray(this.transform.position, direction);

        // 今回の遮蔽物のリストを取得する
        RaycastHit[] hits = Physics.RaycastAll(ray, difference.magnitude, layerMask);


        rendererHits = rendererHitsList.ToArray();
        gameObjectsHits = gameObjectsHitList.ToArray();
        rendererHitsList.Clear();
        gameObjectsHitList.Clear();

        // 遮蔽物は一時的に描画機能を無効にする。
        foreach (RaycastHit hit in hits)
        {
            // 遮蔽物が被写体の場合は例外とする
            if (hit.collider.gameObject == player)
            {
                continue;
            }

            // 遮蔽物の Renderer コンポーネントを無効にする
            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
            GameObject gameObject = hit.collider.gameObject;

            if (renderer != null)
            {
                rendererHitsList.Add(renderer);
                renderer.enabled = false;
                //temple.SetActive(false);
            }
            //if(gameObject)
            //{
            //    gameObjectsHitList.Add(_gameObject);
            //    gameObject.SetActive(false);
            //}
        }

        //前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer renderer in rendererHits.Except<Renderer>(rendererHitsList))
        {
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (renderer != null)
            {
                renderer.enabled = true;
                //temple.SetActive(true);
            }
        }

        //foreach(GameObject _gameObject in gameObjectsHitsPrevs.Except(gameObjectsHitList))
        //{
        //    if (_gameObject)
        //    {
        //        _gameObject.SetActive(true);
        //    }



        //}
        //Vector3 temp = Player.transform.position - this.transform.position;
        //Vector3 normal = temp.normalized;


        //if (Physics.Raycast(this.transform.position, normal, out hit, 100))
        //{


        //     if (hit.transform.gameObject == Player)
        //     {
        //         // Playerを見つけた
        //         Debug.Log("たげ");
        //         temple.SetActive(true);
        //         //TempleRenderer=hit.collider.gameObject.GetComponent<Renderer>();
        //         //TempleRenderer.enabled = true;

        //     }
        //     else
        //     {
        //         //TempleRenderer = hit.collider.gameObject.GetComponent<Renderer>();
        //        // hit.collider.gameObject.SetActive(false);
        //         //TempleRenderer.enabled = false;
        //         //Destroy(hit.collider.gameObject);
        //         Debug.Log(hit.collider.gameObject.name);
        //     }


        //     if (hit.collider.gameObject.CompareTag("Invisible"))
        //     {
        //         temple.SetActive(false);
        //         //Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
        //         //renderer.enabled = false;
        //         Debug.Log(hit.collider.gameObject.name);
        //         //Destroy(hit.collider.gameObject);
        //         //hit.collider.gameObject.SetActive(false);

        //     }

        // }

    }
}