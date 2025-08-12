using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Efectos de sonido")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip pauseClip;

    [Header("Música de fondo")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioClip bottomClip;

    private bool isPaused = false;
    private void Start()
    {
        PlayBackground();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlayJumpSound() => PlaySFX(jumpClip);
    public void PlayDashSound() => PlaySFX(dashClip);
    public void PlayHitSound() => PlaySFX(hitClip);
    public void PlayPauseSound() => PlaySFX(pauseClip);

    public void PlayBackground()
    {
        if (backgroundSource != null && bottomClip != null)
        {
            if (backgroundSource.isPlaying)
            {
                // Already playing, do nothing
                return;
            }
            if (backgroundSource.clip == bottomClip && backgroundSource.time > 0f)
            {
                // Was paused, resume playback
                backgroundSource.UnPause();
            }
            else
            {
                // Either stopped or clip not assigned, play from start
                backgroundSource.clip = bottomClip;
                backgroundSource.loop = true;
                backgroundSource.Play();
            }
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        PlayPauseSound();
        if (isPaused)
        {
            Time.timeScale = 0f;
            backgroundSource.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            backgroundSource.UnPause();
        }
    }
    public void PauseBackground()
    {
        if (backgroundSource != null)
        {
            backgroundSource.Pause();
        }
    }
}
