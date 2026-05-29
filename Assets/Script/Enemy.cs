using System;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private  int DefalutLifeCount;
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
            Destroy(this.gameObject);
        }
    }
}
