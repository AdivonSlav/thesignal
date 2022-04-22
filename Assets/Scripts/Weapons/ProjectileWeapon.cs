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
        [SerializeField] private float fireRate;
        [SerializeField] private ParticleSystem muzzleFlash;

        public void StartFiring()
        {
            var projectile = Instantiate(projectilePrefab, startPoint.position, Quaternion.identity);
            var shootDirection = (target.position - startPoint.position).normalized;
            
            projectile.GetComponent<Projectile>().Init(shootDirection, projectileSpeed);
            muzzleFlash.Emit(1);
        }
    }
}
