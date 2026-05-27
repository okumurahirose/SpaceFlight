using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject[] Stages; //生成するStageプレハブの配列
    [SerializeField] private GameObject Parent; //生成したStageのクローンをまとめるための親オブジェクト

    [SerializeField] private List<GameObject> GeneratedStages = new List<GameObject>(); //生成したステージをまとめるリスト

    float StageSize = 30; //ステージのZ軸幅 [m]
    float GeneratePosition; //ステージの生成位置 [m]
    float CharacterPosition; //機体の位置 [m]

    void Start()
    {
        //機体の位置情報の初期化
        CharacterPosition = Character.transform.position.z;
        //初めに置かれているステージの数に合わせて次に生成するステージ位置の初期化
        GeneratePosition = StageSize * (GeneratedStages.Count + 1) ;
    }

    
    void Update()
    {   
        //機体の位置の更新
        CharacterPosition = Character.transform.position.z;

        //現在生成されたステージ数が６未満だったらステージを作る
        if(GeneratedStages.Count < 6)
        {
            GenerateStage();
        }
        //ステージが６つで機体の位置が特定の位置を超えたら古いステージを消去する
        else if(CharacterPosition > GeneratedStages[2].transform.position.z)
        {
            DestroyStage();
        }

    }
    void GenerateStage()
    {
        //生成するステージをランダムで取得
        int target = Random.Range(0,Stages.Length);

        //生成
        GameObject Instance = Instantiate(
                                    Stages[target],
                                    new Vector3(0.0f,0.0f,GeneratePosition),
                                    Quaternion.identity);

        //生成したステージをリストと親オブジェクトの子オブジェクトに加え、次の生成位置を更新
        GeneratedStages.Add(Instance);
        Instance.transform.SetParent(Parent.transform);
        GeneratePosition += StageSize;
    }

    void DestroyStage()
    {   
        //リストから最も古いステージを取得して消去
        GameObject target = GeneratedStages[0];
        GeneratedStages.RemoveAt(0);
        Destroy(target);
    }
}
