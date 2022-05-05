using UnityEngine;

namespace TheSignal.SFX.Environement
{
    public class DoorSoundController : MonoBehaviour
    {
        private AudioSource audioSource;
        
        [SerializeField] private float volume;
        [SerializeField] private AudioClip doorSlide;
        [SerializeField] private AudioClip terminalConfirmation;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = volume;
        }

        public void PlaySlide()
        {
            audioSource.PlayOneShot(doorSlide);
        }
        
        public void PlayConfirmation()
        {
            audioSource.PlayOneShot(terminalConfirmation);
        }
    }
}
