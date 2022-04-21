using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TheSignal.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 shootDirection;
        private float projectileSpeed;
        private ParticleSystem metalHitEffect;
        private ParticleSystem fleshHitEffect;
        private LayerMask hitMask;

        private void Awake()
        {
            hitMask = LayerMask.GetMask("Default", "Enemy");
        }

        private void Update()
        {
            Translate(Time.deltaTime);
        }

        public void Init(Vector3 shootDirection, float projectileSpeed, ParticleSystem metalHitEffect, ParticleSystem fleshHitEffect)
        {
            this.shootDirection = shootDirection;
            this.projectileSpeed = projectileSpeed;
            this.metalHitEffect = metalHitEffect;
            this.fleshHitEffect = fleshHitEffect;
            
            transform.rotation = Quaternion.LookRotation(shootDirection);
        }

        public void Translate(float deltaTime)
        {
            transform.position += shootDirection * projectileSpeed * deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Layer 0 - Default
            // Layer 8 - Enemy
            if (other.gameObject.layer == 0 || other.gameObject.layer == 8)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit,100.0f, hitMask);

                if (other.gameObject.layer == 8)
                {
                    fleshHitEffect.transform.position = hit.point;
                    fleshHitEffect.transform.forward = hit.normal;
                    fleshHitEffect.Emit(1);
                }
                else
                {
                    metalHitEffect.transform.position = hit.point;
                    metalHitEffect.transform.forward = hit.normal;
                    metalHitEffect.Emit(1);
                }
                
                
                Destroy(gameObject);
            }
        }
    }
}
