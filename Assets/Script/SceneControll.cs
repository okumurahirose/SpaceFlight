using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{

    public void MainToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void TitleToMain()
    {
        SceneManager.LoadScene("Main");
    } 
}
