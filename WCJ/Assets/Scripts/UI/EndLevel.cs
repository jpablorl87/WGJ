using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndHandler : MonoBehaviour
{
    public string Victory = "Victory"; 

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Victory");
        }
    }
}

