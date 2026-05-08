using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject TargetObject;
    [SerializeField] private float FollowSpeed; // [m/s]

    float _diff; // [m]
    void Start()
    {   
        //TargetObjectとの距離を取得
        _diff = TargetObject.transform.position.z - transform.position.z;
    }

    //Update()の処理が終わってから追いかける
    void LateUpdate()
    {
        //スムーズに近づく
        float new_Z = Mathf.Lerp(
            transform.position.z,
            TargetObject.transform.position.z - _diff, /* = 目標 */
            FollowSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.z = new_Z;

        //反映
        transform.position = pos;
    }
}
