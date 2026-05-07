using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControll : MonoBehaviour
{
    //最大速度の設定
    [SerializeField] private float RotateRate; //横回転の速度の割合
    [SerializeField] private float Speed_Y; //縦方向
    [SerializeField] private float Speed_Z; //前方向

    [SerializeField] private float Acceleration_Z; //前方向の加速度
    [SerializeField] private float ReRotateRate; //水平に戻る速度の割合


    Vector3 MoveDirection = Vector3.zero;

    InputAction _moveAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }


    void Update()
    {
        //自動で前に進む
        float Accelerated_Z = MoveDirection.z + (Acceleration_Z * Time.deltaTime);
        MoveDirection.z = Mathf.Clamp(Accelerated_Z,0,Speed_Z);

        //縦方向への移動
        MoveDirection.y = _moveAction.ReadValue<Vector2>().y * Speed_Y;


        if(_moveAction.ReadValue<Vector2>().x == 0.0f)
        {
            ReturnRotate();
            
        }
        else
        {
            //入力されていない場合、機体を水平にする
            float abs = -(_moveAction.ReadValue<Vector2>().x);
            SetRotate(abs);
        }

        Vector3 GlobalDirection = transform.position + transform.TransformDirection(MoveDirection);

        transform.position = GlobalDirection;
    }

    //入力に対して機体の角度を調整
    void SetRotate(float abs)
    {
        //機体の角度をいくら傾けるか
        float target = abs * RotateRate;
        transform.Rotate(0.0f,0.0f,target);
    }

    //機体の角度を自動で水平に戻す
    void ReturnRotate()
    {
        float current = transform.rotation.z;
        Debug.Log("Re" + current);

        //機体の角度を水平にいくら近づけるか
        float target = -Mathf.Lerp(0.0f,current,ReRotateRate);
        transform.Rotate(0.0f,0.0f,target);
    }
}
