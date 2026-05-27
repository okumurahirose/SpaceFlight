using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class FlightControll : MonoBehaviour
{
    //機体の最大移動幅
    [SerializeField] private Vector2 LaneSize; //[(x,y)]

    //最大速度の設定
    [SerializeField] private float Speed_X; //横方向 [m/s]
    [SerializeField] private float Speed_Y; //縦方向 [m/s]
    [SerializeField] private float Speed_Z; //前方向 [m/s]

    [SerializeField] private float Acceleration_Z; //前方向の加速度 [m/s^2]
    float AcceleratedSpeed; //加速されている前方向の速度 [m/s]

    [SerializeField] private float RotateRate; //横回転の速度の割合 [定数]
    [SerializeField] private float ReRotateRate; //水平に戻る速度の割合 [m/s]

    Vector3 MoveDirection = Vector3.zero;
    InputAction _moveAction;
    Animator _animator;

    const float StunTime = 0.5f; //Stun状態の長さ
    float RecoverTime = 0.0f; //残りのStun状態時間
    bool IsDead = false; //Lifeが０になったか
    [SerializeField] private GameObject GameController;
    
    

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        AcceleratedSpeed = 0.0f;

        //Animation,Animatorは子オブジェクトにアタッチされているため、子オブジェクトを探して取得
        GameObject child = transform.GetChild(0).gameObject;
        _animator = child.GetComponent<Animator>();
        
    }


    void Update()
    {   
        if (IsStun())
        {   
            //Stun状態ならすべての移動を停止
            AcceleratedSpeed = 0.0f;
            MoveDirection = Vector3.zero;
            RecoverTime -= Time.deltaTime;
        }
        else
        {   
            //自動で前に進む
            AcceleratedSpeed += Acceleration_Z * Time.deltaTime;
            MoveDirection.z = Mathf.Clamp(AcceleratedSpeed,0,Speed_Z) * Time.deltaTime;

            //横方向への移動
            MoveDirection.x = _moveAction.ReadValue<Vector2>().x * Speed_X * Time.deltaTime;
            //縦方向への移動
            MoveDirection.y = _moveAction.ReadValue<Vector2>().y * Speed_Y * Time.deltaTime;

            //機体をZ軸回転させる（q:左　e:右）
            if(Keyboard.current.qKey.isPressed){
                float abs = 1;
                SetRotate(abs);
            }
            else if(Keyboard.current.eKey.isPressed)
            {
                float abs = -1;
                SetRotate(abs);
            }
            //rKeyで機体を水平に戻す
            else if(Keyboard.current.rKey.wasPressedThisFrame)
            {
                StartCoroutine(ReturnRotate());
            }
        }

        //機体の移動後の位置
        Vector3 GlobalDirection = transform.position + transform.TransformDirection(MoveDirection);
        
        //機体の移動幅の制限
        GlobalDirection.x = Mathf.Clamp(GlobalDirection.x,-LaneSize.x,LaneSize.x);
        GlobalDirection.y = Mathf.Clamp(GlobalDirection.y,-LaneSize.y,LaneSize.y);
        
        //移動の反映
        transform.position = GlobalDirection;
    }

     //入力に対して機体の角度を調整
    void SetRotate(float abs)
    {
        //機体の角度をいくら傾けるか
        float target = abs * RotateRate;
        transform.Rotate(0,0,target);
    }

    //機体の角度を自動で水平に戻す
    IEnumerator ReturnRotate()
    {
        float current = transform.eulerAngles.z;

        while(current > ReRotateRate){

            //機体の角度を水平にいくら近づけるか　（完全に水平にはならない）
            float target = Mathf.Sign(current - 180) * ReRotateRate;
            transform.Rotate(0,0,target);
            current = transform.eulerAngles.z;
            yield return null;
        }

        //完全に水平にする
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
    }

    //Stun状態であるか
    bool IsStun()
    {
        //死んでいたら強制スタン状態
        if(IsDead) return true;

        return RecoverTime > 0.0f;
    }

    //呼び出されたら死んでいる状態にする
    void Dead()
    {
        IsDead = true;
    }

    //Enemyに接触したら衝突イベントを実行
    void OnCollisionEnter(Collision other)
    {   
        //Stun状態なら何もしない
        if(IsStun()) return;

        if(other.gameObject.tag == "Enemy"){
            RecoverTime = StunTime;
            _animator.SetTrigger("Collision");
            Destroy(other.gameObject);
        }

        GameController.SendMessage("ManageLife");
    }
}