using UnityEngine;
using TheSignal.Player.Input;
using TheSignal.Weapons;

namespace TheSignal.Player.Combat
{
    public class PlayerShooting : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerAiming playerAiming;
        private ProjectileWeapon projectileWeapon;

        private float lastShot = 0.0f;
        
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAiming = GetComponent<PlayerAiming>();
            projectileWeapon = GetComponentInChildren<ProjectileWeapon>();
        }

        public void HandleShooting()
        {
            lastShot += Time.deltaTime;            
            
            if (inputManager.isFiring && inputManager.isAiming && playerAiming.allowedFire && playerAiming.enteredAim)
            {
                projectileWeapon.StartFiring(ref lastShot);
            }
        }
        
    }
}