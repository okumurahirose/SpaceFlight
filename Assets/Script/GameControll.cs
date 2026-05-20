using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControll : MonoBehaviour
{
    [SerializeField] private GameObject Player; 
    [SerializeField] private Image[] Lifes;
    [SerializeField] private TextMeshProUGUI Score;
    const int LifeCount = 3;
    int NowLifeCount;
    int position;

    void Start()
    {
        //NowLifeCountの初期化
        NowLifeCount = LifeCount;
        //Scoreの初期化
        position = (int)Player.transform.position.z;
        Score.text = "Score : " + position + "m";
    }

    void Update()
    {
        //Scoreの更新
        position = (int)Player.transform.position.z;
        Score.text = "Score : " + position + "m";
    }


    public void ManageLife()
    {
        
        Lifes[NowLifeCount-1].gameObject.SetActive(false);
        NowLifeCount--;

        //Lifeが全損したら死亡状態を発信
        if(NowLifeCount == 0)  Player.SendMessage("Dead");
        
        Debug.Log(NowLifeCount);

        return;
    }
}
