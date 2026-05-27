using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletPool Pool;
    [SerializeField] private float ActiveTime;
    [SerializeField] private float Speed;
    float RestActiveTime;

    void Awake()
    {
        Pool = FindFirstObjectByType<BulletPool>();
    }

    void Start()
    {
        RestActiveTime = ActiveTime;
    }

    void Update()
    {
        transform.Translate(0,Speed * Time.deltaTime,0);
        RestActiveTime -= Time.deltaTime;

        if(RestActiveTime < 0)
        {
            Pool.Release(this.gameObject);
            RestActiveTime = ActiveTime;
            
        }
    }
}
