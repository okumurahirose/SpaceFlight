using TMPro;
using UnityEngine;

public class ReadScore : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI MaxScore;
   [SerializeField] private TextMeshProUGUI PreviousScore;

    void Start()
    {
        //セーブデータの読み取り
        int Max = PlayerPrefs.GetInt("MaxScore",0);
        int Previous = PlayerPrefs.GetInt("PreviousScore",0);

        //スコアに反映
        MaxScore.text = "Max : " + Max;
        PreviousScore.text = "Previous : " + Previous;
    }
}
