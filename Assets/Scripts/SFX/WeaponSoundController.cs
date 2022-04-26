using System;
using System.Collections;
using System.Collections.Generic;
using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.SFX
{
    public class WeaponSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource weaponAudio;
        [SerializeField] private float volume;
        [SerializeField] private AudioClip shotSound;
        [SerializeField] private AudioClip chargeSound;
        
        private void Start()
        {
            weaponAudio.volume = volume;
        }

        public void PlayShot()
        {
            weaponAudio.PlayOneShot(shotSound);
        }
    }
}
