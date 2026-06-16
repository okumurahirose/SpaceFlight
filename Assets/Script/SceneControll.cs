using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public void TitleToMain()
    {
        SceneManager.LoadScene("Main");
    } 
}
