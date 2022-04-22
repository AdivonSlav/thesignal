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
        private ProjectileWeapon projectileWeapon;
        
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            projectileWeapon = GetComponentInChildren<ProjectileWeapon>();
        }

        public void HandleShooting() 
        {
            if (inputManager.isFiring && inputManager.isAiming)
            {
                projectileWeapon.StartFiring();
            }
        }
        
    }
}