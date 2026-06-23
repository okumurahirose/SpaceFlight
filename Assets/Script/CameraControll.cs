using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private Camera[] Cameras;
    int Now = 0; //現在のメインカメラの配列番号

    void Awake()
    {
        //初めのカメラ以外非稼働状態にする
        for(int i = 1;i < Cameras.Length; i++)
        {
            Cameras[i].enabled = false;
        }
    }

    void Update()
    {
        //cキーでカメラを切り替える
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChangeCamera();
        }
    }

    //次のカメラに切り替える
    void ChangeCamera()
    {   
        Cameras[Now].enabled = false; //今のカメラを非稼働状態に

        Now = (Now + 1) % Cameras.Length; //次のカメラの配列番号
        Cameras[Now].enabled = true; //次のカメラを稼働状態に
    }
}
