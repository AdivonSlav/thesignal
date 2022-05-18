using UnityEngine;

namespace TheSignal.SFX.Environement
{
    public class SprinklerSoundController : MonoBehaviour
    {
        private AudioSource audioSource;
        
        [SerializeField] private AudioClip sprinklerSound;
        [SerializeField] private ParticleSystem showerParticles;

        private bool played = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (showerParticles.isPlaying && !played)
            {
                audioSource.PlayOneShot(sprinklerSound);
                played = true;
            }

            if (!showerParticles.gameObject.activeInHierarchy)
            {
                audioSource.Stop();
                this.gameObject.SetActive(false);
            }
        }
    }
}
