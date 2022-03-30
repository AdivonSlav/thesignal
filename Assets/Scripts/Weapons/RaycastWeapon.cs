using System.Collections.Generic;
using UnityEngine;

namespace TheSignal.Weapons
{
    internal class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    
    public class RaycastWeapon : MonoBehaviour
    {
        [SerializeField] private int fireRate;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private int bulletDrop;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField] private TrailRenderer tracerEffect;
        [SerializeField] private Transform raycastOrigin;
        [SerializeField] private Transform raycastEnd;

        private Ray ray;
        private RaycastHit hit;
        private float accumulatedTime;
        private float maxLifetime = 3.0f;
        private List<Bullet> bullets = new List<Bullet>();

        public void StartFiring()
        {
            accumulatedTime = 0.0f;
            FireShot();
        }

        public void UpdateFiring(float deltaTime)
        {
            accumulatedTime += deltaTime;
            float fireInterval = 1.0f / fireRate;

            while (accumulatedTime >= 0.0f)
            {
                FireShot();
                accumulatedTime -= fireInterval;
            }
        }
        
        public void UpdateBullets(float deltaTime)
        {
            SimulateBullets(deltaTime);
            DestroyBullets();
        }
        
        private void SimulateBullets(float deltaTime)
        {
            bullets.ForEach(bullet =>
                {
                    Vector3 p0 = GetPosition(bullet);
                    bullet.time += deltaTime;
                    Vector3 p1 = GetPosition(bullet);
                    
                    RaycastSegment(p0, p1, bullet);
                }
            );
        }
        
        private void DestroyBullets()
        {
            bullets.RemoveAll(bullet => bullet.time >= maxLifetime);
        }

        private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
        {
            Vector3 direction = end - start;
            float distance = direction.magnitude;
            ray.origin = start;
            ray.direction = direction;
            
            if (Physics.Raycast(ray, out hit, distance))
            {
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = hit.normal;
                hitEffect.Emit(1);

                bullet.tracer.transform.position = hit.point;
                bullet.time = maxLifetime;
            }
            else
            {
                bullet.tracer.transform.position = end;
            }
        }
        
        private void FireShot()
        {
            muzzleFlash.Emit(1);

            Vector3 velocity = (raycastEnd.position - raycastOrigin.position).normalized * bulletSpeed;
            var bullet = CreateBullet(raycastOrigin.position, velocity);
            bullets.Add(bullet);
        }

        private Vector3 GetPosition(Bullet bullet)
        {
            Vector3 gravity = Vector3.down * bulletDrop;

            // Position * (velocity * time) + (halved gravity * time squared)
            return bullet.initialPosition + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * Mathf.Pow(bullet.time, 2));
        }

        private Bullet CreateBullet(Vector3 position, Vector3 velocity)
        {
            var bullet = new Bullet()
            {
                initialPosition = position,
                initialVelocity = velocity,
                time = 0.0f,
                tracer = Instantiate(tracerEffect, position, Quaternion.identity)
            };

            bullet.tracer.AddPosition(position);
            
            return bullet;
        }
    }
}
