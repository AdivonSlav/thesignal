using System;
using UnityEngine;

namespace TheSignal
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerStep : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] stepWalkClips;
        [SerializeField] private AudioClip[] stepRunClips;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // StepWalk and StepRun are called with animation events (Walk and Run)
        private void StepWalk()
        {
            var clip = GetRandomClip(ref stepWalkClips);
            audioSource.PlayOneShot(clip);
        }

        private void StepRun()
        {
            var clip = GetRandomClip(ref stepRunClips);
            audioSource.PlayOneShot(clip);
        }

        private AudioClip GetRandomClip(ref AudioClip[] clips)
        {
            var rnd = new System.Random(Guid.NewGuid().GetHashCode());

            return clips[rnd.Next(0, clips.Length)];
        }
    }
}
