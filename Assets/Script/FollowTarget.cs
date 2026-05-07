using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject TargetObject;
    [SerializeField] private float FollowSpeed;

    Vector3 _diff;

    void Start()
    {   
        //TargetObjectとの距離を取得
        _diff = TargetObject.transform.position - transform.position;
    }

    //Update()の処理が終わってから追いかける
    void LateUpdate()
    {
        //スムーズに近づく
        Vector3 Pos = Vector3.Lerp(
            transform.position,
            TargetObject.transform.position - _diff, /* = 目標 */
            FollowSpeed * Time.deltaTime);

        //反映
        transform.position = Pos;
    }
}
