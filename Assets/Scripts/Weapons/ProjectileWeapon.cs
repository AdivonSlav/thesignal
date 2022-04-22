using System;
using System.Collections;
using System.Collections.Generic;
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

        private float lastShot = 0.0f;
        
        public void StartFiring()
        {
            // We track how much time has passed since the player last fired
            // If it is over the fire delay that is set, a new shot can be fired
            lastShot += Time.deltaTime;
            
            if (lastShot >= fireDelay)
            {
                var projectile = Instantiate(projectilePrefab, startPoint.position, Quaternion.identity);
                var shootDirection = (target.position - startPoint.position).normalized;
                
                projectile.GetComponent<Projectile>().Init(shootDirection, projectileSpeed);
                muzzleFlash.Emit(1);

                lastShot = 0.0f;
            }
        }
    }
}
