using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
   [SerializeField] private GameObject BulletPrefab;
   [SerializeField] private int FirstBulletMount;
   private List<GameObject> Pool = new List<GameObject>();

    void Awake()
    {
        //起動時に規定の数までBulletをあらかじめ生成
        for(int i = 0; i < FirstBulletMount; i++)
        {
            CreateNewBullet();

        }
    }

    //新しいBulletを生成する
    public GameObject CreateNewBullet()
    {
        GameObject target = Instantiate(BulletPrefab,
                                        transform.position,
                                        Quaternion.identity);
        Pool.Add(target);
        target.SetActive(false);
        //生成したBulletを子オブジェクトにする
        target.transform.SetParent(transform);

        return target;
    }

    //BulletPoolからBulletを取得する
    public GameObject Get()
    {   
        //非ActiveなBulletをPoolから探す
        foreach(GameObject target in Pool)
        {   
            if (!target.activeInHierarchy)
            {
                target.SetActive(true);
                return target;
            }
        }
        
        //全てActiveだったら新しくBulletを生成
        return CreateNewBullet();
    }

    //使い終わったBulletを返却する
    public void Release(GameObject target)
    {
        target.SetActive(false);
    }
}
