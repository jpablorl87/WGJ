using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Retry(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }


    public void MainMenu()
    {
        Time.timeScale = 1f; 
 
        SceneManager.LoadScene("MainMenu");      

       // SceneManager.LoadScene("MainMenu");

    }

    public void HowToPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HowToPlay");
    }
}
