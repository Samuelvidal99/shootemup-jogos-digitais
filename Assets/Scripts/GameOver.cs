using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public void ReturnToMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("JogoStart");
        Time.timeScale = 1f;
    }
    public void CloseGame(){
        Application.Quit();
    }
}
