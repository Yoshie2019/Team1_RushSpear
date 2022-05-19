using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    GameObject[] dangoImage = new GameObject[3];

    [SerializeField] GameObject dangoImage1;
    [SerializeField] GameObject dangoImage2;
    [SerializeField] GameObject dangoImage3;

    public static int EnemyDestryCount;
    private AudioSource DestrySound;

    // Start is called before the first frame update
    void Start()
    {
        dangoImage[0] = dangoImage1;
        dangoImage[1] = dangoImage2;
        dangoImage[2] = dangoImage3;

        EnemyDestryCount = 0;
        DestrySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("EnemyDestryCount:" + EnemyDestryCount);
    }

    public void SpawnImage(int type)
    {
        GameObject d = Instantiate(dangoImage[type], transform);
        d.transform.SetAsFirstSibling();

        EnemyDestryCount += 100;
        DestrySound.PlayOneShot(DestrySound.clip);
        Debug.Log("playerhit" + EnemyDestryCount);

        int count = transform.childCount;

        if (count > 3)
        {
            print("３以上");
            DestroyImmediateChildObject(count - 1);
        }
    }

    void DestroyImmediateChildObject(int Count)
    {
        Destroy(transform.GetChild(Count).gameObject);
    }

}
