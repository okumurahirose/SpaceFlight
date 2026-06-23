using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControll : MonoBehaviour
{   
    [SerializeField] private GameObject Player; 
    [SerializeField] private GameObject sceneControll;
    [SerializeField] private Image[] Lifes;
    [SerializeField] private TextMeshProUGUI Score;

    public bool IsDead = false; //Playerが死亡状態であるかどうか
    const int LifeCount = 3;
    int NowLifeCount;
    int position;
    int BounasScore = 0;

    //EnemyがGameControllを参照するためのプロパティ
    public static GameControll Instance {get;private set;}

    void Awake()
    {   
        //プロパティの設定
        Instance = this;
    }

    void Start()
    {   
        //NowLifeCountの初期化
        NowLifeCount = LifeCount;
        //Scoreの初期化
        position = (int)Player.transform.position.z;
        Score.text = "Score : " + position + "m" + " +" + BounasScore;
    }

    void Update()
    {   
        //スコアの更新
        position = (int)Player.transform.position.z;
        Score.text = "Score : " + position + "m" + " +" + BounasScore;
    }


    public void ManageLife()
    {
        
        Lifes[NowLifeCount-1].gameObject.SetActive(false);
        NowLifeCount--;

        //Lifeが全損したら死亡状態にする
        if(NowLifeCount == 0){
            
            IsDead = true; //死亡状態に変更

            //スコアのセーブ
            int Score = position + BounasScore;
            PlayerPrefs.SetInt("PreviousScore",Score);
            int Max = PlayerPrefs.GetInt("MaxScore",0);
            
            //最大スコアの更新
            if(Max < Score)
            {
                PlayerPrefs.SetInt("MaxScore",Score);
            } 

            PlayerPrefs.Save(); //セーブスコアの確定

            Invoke("CallSceneControll",2.0f); //2秒後にタイトルシーンに戻る
        }

        return;
    }

    public void AddBounasScore(int EnemyScore)
    {
        BounasScore += EnemyScore;
    }

    private void CallSceneControll()
    {
        sceneControll.SendMessage("MainToTitle");
    }
}
