using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

using TheSignal.Player.Input;
using TheSignal.Animation;
using TheSignal.Weapons;
using UnityEngine.Animations.Rigging;

namespace TheSignal.Player.Combat
{
    public class PlayerShooting : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerAiming playerAiming;
        private RaycastWeapon raycastWeapon;
        
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAiming = GetComponent<PlayerAiming>();
            raycastWeapon = GetComponentInChildren<RaycastWeapon>();
        }

        public void HandleShooting() 
        {
            if (inputManager.isFiring && inputManager.isAiming)
            {
                raycastWeapon.StartFiring();
            }
            else
            {
                raycastWeapon.StopFiring();
            }
        }
        
    }
}