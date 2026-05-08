using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject[] Stages;

    List<GameObject> GeneratedStages = new List<GameObject>();

    float StageSize = 30;
    float GeneratePosition = 90;
    float CharacterPosition;

    void Start()
    {
        CharacterPosition = Character.transform.position.z;
    }

    
    void Update()
    {
        if(GeneratedStages.Count <= 6)
        {
            GameObject target = Stages[Random.Range(0,Stages.Length)];
            GameObject Instance = Instantiate(
                                        target,
                                        new (0.0f,0.0f,GeneratePosition),
                                        Quaternion.identity);
            GeneratedStages.Add(Instance);
            GeneratePosition += StageSize;
        }
    }
}
