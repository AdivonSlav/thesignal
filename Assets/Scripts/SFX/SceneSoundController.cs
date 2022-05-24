using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheSignal.SFX
{
    
    public class SceneSoundController : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField] private float fadeTime;
        [Header("Tracks")]
        [SerializeField] private AudioClip ambienceTrack;
        [SerializeField] private AudioClip enemyFightTrack;
        [SerializeField] private AudioClip bossFightTrack;

        private bool fadingOut;
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayTrack(bool enemyPresent, bool bossPresent = false)
        {        
            if (audioSource.isPlaying)
                StartCoroutine(FadeOut());

            StartCoroutine(SwitchTrack(enemyPresent, bossPresent));
        }

        private IEnumerator FadeOut()
        {
            fadingOut = true;
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0.0f)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }
            
            audioSource.volume = startVolume;
            audioSource.Stop();
            fadingOut = false;
        }

        private IEnumerator SwitchTrack(bool enemyPresent, bool bossPresent)
        {
            while (fadingOut)
                yield return new WaitForSeconds(0.1f);
            
            audioSource.clip = enemyPresent ? enemyFightTrack : ambienceTrack;
            audioSource.clip = bossPresent ? bossFightTrack : audioSource.clip;
            audioSource.Play();
        }

        public bool IsPlaying()
        {
            return audioSource.isPlaying;
        }
    }
}
