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
        [SerializeField] private Transform aimTarget;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private ParticleSystem metalHitEffect;
        [SerializeField] private ParticleSystem fleshHitEffect;

        public void StartFiring()
        {
            var projectile = Instantiate(this.projectilePrefab, startPoint.position, Quaternion.identity);
            var shootDirection = (aimTarget.position - startPoint.position).normalized;
            
            projectile.GetComponent<Projectile>().Init(shootDirection, projectileSpeed, metalHitEffect, fleshHitEffect);   
        }
    }
}
