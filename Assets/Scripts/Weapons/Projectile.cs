using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace TheSignal.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody projectileRB;
        
        private Vector3 shootDirection;
        private float projectileSpeed;
        [SerializeField] private ParticleSystem metalHitEffect;
        [SerializeField] private ParticleSystem fleshHitEffect;

        private void Awake()
        {
            projectileRB = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 direction, float speed)
        {
            shootDirection = direction;
            projectileSpeed = speed;

            projectileRB.MoveRotation(Quaternion.LookRotation(shootDirection));
            projectileRB.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.VelocityChange);
            
            Destroy(gameObject, 2);
        }

        private void OnCollisionEnter(Collision collision)
        { 
            var contact = collision.GetContact(0);
            
            if (collision.gameObject.layer == 0)
            {
                Instantiate(metalHitEffect,contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(gameObject);
            }
            else if (collision.gameObject.layer == 8)
            {
                Instantiate(fleshHitEffect, contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(gameObject);
            }
        }
    }
}
