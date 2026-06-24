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

    public void QuitGame()
    {
        Debug.Log("ゲーム終了"); //確認用
        Application.Quit();
    }
}
