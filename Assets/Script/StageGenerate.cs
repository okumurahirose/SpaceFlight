using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject[] Stages; //生成するStageプレハブの配列
    [SerializeField] private GameObject Parent; //生成したStageのクローンをまとめるための親オブジェクト

    [SerializeField] private List<GameObject> GeneratedStages = new List<GameObject>();

    float StageSize = 30;
    float GeneratePosition = 90;
    float CharacterPosition;

    void Start()
    {
        CharacterPosition = Character.transform.position.z;
        
    }

    
    void Update()
    {   
        CharacterPosition = Character.transform.position.z;

        if(GeneratedStages.Count <= 6)
        {
            GenerateStage();
        }
        else if(CharacterPosition > GeneratedStages[2].transform.position.z)
        {
            DestroyStage();
        }

    }
    void GenerateStage()
    {
        int target = Random.Range(0,Stages.Length);

        GameObject Instance = Instantiate(
                                    Stages[target],
                                    new Vector3(0.0f,0.0f,GeneratePosition),
                                    Quaternion.identity);
        GeneratedStages.Add(Instance);
        GeneratePosition += StageSize;
        Instance.transform.SetParent(Parent.transform);
    }

    void DestroyStage()
    {
        GameObject target = GeneratedStages[0];
        GeneratedStages.RemoveAt(0);
        Destroy(target);
    }
}
