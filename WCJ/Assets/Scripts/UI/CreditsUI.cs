using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsUI : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
