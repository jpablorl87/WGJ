using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
