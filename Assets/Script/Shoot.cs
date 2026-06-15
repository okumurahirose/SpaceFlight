using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;


    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) Fire();
    }

    public void Fire()
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
