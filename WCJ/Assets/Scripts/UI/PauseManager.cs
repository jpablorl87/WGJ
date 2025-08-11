using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    //[SerializeField] private AudioHandler audioHandler;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioHandler audioHandler;
    private bool isPaused = false;
    private void Update()
    {
        if (inputHandler.IsPausePressed())
        {
            TogglePause();
        }
    }
    private void TogglePause()
    {
        isPaused = !isPaused;
        //PlayPauseSound();
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true); //Don't erase this line
            audioHandler?.PauseBackground();
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            audioHandler?.PlayBackground();
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        audioHandler?.PlayBackground();
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
