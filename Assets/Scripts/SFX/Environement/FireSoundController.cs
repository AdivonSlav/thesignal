using UnityEngine;

namespace TheSignal.SFX.Environement
{
    public class FireSoundController : MonoBehaviour
    {
        private AudioSource audioSource;
        private ParticleSystem fireParticles;

        [SerializeField] private AudioClip fireSound;

        private bool played = false;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            fireParticles = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (fireParticles.isPlaying && !played)
            {
                audioSource.PlayOneShot(fireSound);
                played = true;
            }
        }
        
        private void OnDisable()
        {
            audioSource.Stop();
        }
    }
}
