using UnityEngine;
namespace Player
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip dashClip;
        [SerializeField] private AudioClip hitClip;
        [SerializeField] private AudioClip pauseClip;
        public void PlayJumpSound()
        {
            PlayClip(jumpClip);
        }
        public void PlayDashSound()
        {
            PlayClip(dashClip);
        }
        public void PlayHitSound()
        {
            PlayClip(hitClip);
        }
        public void PlayPauseSound()
        {
            PlayClip(pauseClip);
        }
        private void PlayClip(AudioClip clip)
        {
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
