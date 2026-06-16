using System;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int DefalutLifeCount;
    [SerializeField] private int BounasScore;
    int RestLifeCount;
    void Start()
    {
        RestLifeCount = DefalutLifeCount;
    }

    public void LifeDecrement()
    {
        RestLifeCount--;
        if(RestLifeCount <= 0)
        {   
            //ボーナススコアを加算して消滅
            GameControll.Instance.AddBounasScore(BounasScore);
            Destroy(this.gameObject);
        }
    }
}
