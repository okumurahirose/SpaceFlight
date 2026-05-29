using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    BulletPool bulletPool;
    InputAction ShootAction;

    void Awake()
    {
        //BulletPoolの取得
        bulletPool = FindFirstObjectByType<BulletPool>();
    }

    void Start()
    {
        ShootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (ShootAction.WasPerformedThisFrame()) Fire();
       
    }

    void Fire()
    {  
            //PoolからBulletを取得
            GameObject target = bulletPool.Get();
            //発射位置の設定
            Vector3 targetposition = transform.position;
            targetposition.z += 2;
            target.transform.position = targetposition;
            //Bulletの先端が前を向くようにする
            target.transform.eulerAngles = new Vector3(0,-90,-90);
    }
}
