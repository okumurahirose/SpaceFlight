using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class GameControll : MonoBehaviour
{
    [SerializeField] private Image[] Lifes;
    const int LifeCount = 3;
    int NowLifeCount;

    void Start()
    {
        NowLifeCount = LifeCount;
    }


    public void ManageLife()
    {
        if(NowLifeCount <= 0) return;

        Lifes[NowLifeCount-1].gameObject.SetActive(false);
        NowLifeCount--;

        Debug.Log(NowLifeCount);

        return;
    }
}
