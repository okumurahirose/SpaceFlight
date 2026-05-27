using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    BulletPool Pool;
    InputAction ShootAction;

    void Awake()
    {
        Pool = FindFirstObjectByType<BulletPool>();
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
            GameObject target = Pool.Get();
            Vector3 targetposition = transform.position;
            targetposition.z += 2;
            target.transform.position = targetposition;
            target.transform.eulerAngles = new Vector3(0,-90,-90);
    }
}
