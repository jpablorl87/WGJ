using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MoritaEscene");
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
