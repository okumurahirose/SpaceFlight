using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletPool Pool;
    [SerializeField] private float ActiveTime; //Bulletの存在可能時間
    [SerializeField] private float Speed; //Bulletのスピード
    float RestActiveTime; //残りの存在時間
    Rigidbody _rigidbody;

    void Awake()
    {
        //BulletPoolを取得
        Pool = FindFirstObjectByType<BulletPool>();
    }

    void Start()
    {
        RestActiveTime = ActiveTime;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //直進処理と時間計算
        transform.Translate(0,Speed * Time.deltaTime,0);
        RestActiveTime -= Time.deltaTime;
        
        if(RestActiveTime < 0)
        {
            //Poolに返却する
            Pool.Release(this.gameObject);

            //Bulletを初期状態に戻す
            _rigidbody.linearVelocity = new Vector3();
            _rigidbody.angularVelocity = new Vector3();
            RestActiveTime = ActiveTime;
            
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("LifeDecrement");
        }

        return;
    }
}
