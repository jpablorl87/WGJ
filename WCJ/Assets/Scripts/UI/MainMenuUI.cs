using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
