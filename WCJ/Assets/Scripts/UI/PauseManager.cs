using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    //[SerializeField] private AudioHandler audioHandler;
    [SerializeField] private GameObject pauseMenuUI;
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
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenuUI.SetActive(isPaused);
        //audioHandler.PlayPauseSound();
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
