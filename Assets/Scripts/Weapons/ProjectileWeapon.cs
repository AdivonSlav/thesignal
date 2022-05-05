using TheSignal.SFX.Player;
using UnityEngine;

namespace TheSignal.Weapons
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform target;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float fireDelay;
        [SerializeField] private ParticleSystem muzzleFlash;

        private WeaponSoundController weaponSFX;

        private void Awake()
        {
            weaponSFX = GetComponent<WeaponSoundController>();
        }
        
        // lastShot is continuosly updated in the PlayerShooting and passed here
        // Whenever sustained fire is made, last shot is reset and allowed to tick again in order to achieve a delay in fire
        public void StartFiring(ref float lastShot)
        {
            if (lastShot >= fireDelay)
            {
                var projectile = Instantiate(projectilePrefab, startPoint.position, Quaternion.identity);
                var shootDirection = (target.position - startPoint.position).normalized;
                
                projectile.GetComponent<Projectile>().Init(shootDirection, projectileSpeed);
                muzzleFlash.Emit(1);
                weaponSFX.PlayShot();

                lastShot = 0.0f;
            }
        }
    }
}
