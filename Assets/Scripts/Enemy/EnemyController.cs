using System;
using TheSignal.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace TheSignal.Enemy
{
    public class EnemyController : TrackedEntity
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private Transform target;
        [SerializeField] private float enemyDistance;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private float viewingDistance;

        private NavMeshAgent agent;
        private Animator anim;
        private int currentHealth;
        
        private void Start()
        {
            GetReferences();
        }
        private void Update()
        {
            agent.velocity = Vector3.zero;
            
            if (pauseUI.activeInHierarchy)
            {
                anim.gameObject.SetActive(false);
                anim.gameObject.SetActive(true);
                transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z,transform.rotation.w);
                anim.SetFloat("Movement", 0.0f, 0.3f, Time.deltaTime);
                return;
            }

            var targetDistance = Vector3.Distance(transform.position, target.position);
            
            if (targetDistance < enemyDistance)
                anim.Play("Soft Attack");
            else if(viewingDistance>=targetDistance)
            {
                MoveToPlayer();
                RotateToTarget();
            }
            else
            {
                anim.SetFloat("Movement", 0.0f, 0.3f, Time.deltaTime);
            }
        }
        private void GetReferences()
        {
            currentHealth = maxHealth;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }
        public void TakeDamage(int damageBonus, int maxDamage)
        {
            // Damage bonus is the bonus applied if the enemy was hit in the head, torso etc.
            // Max damage is the maximum damage you can apply based on the projectile type (fetched from the Projectile script)

            int damage = CalculateDamage(maxDamage);
            damage += damageBonus;
            currentHealth -= damage;

            Debug.Log($"Dealt {damage} damage!");
            
            if (currentHealth <= 0)
                Die();
        }
        
        private void Die()
        {
            Destroy(this.gameObject);
        }
        
        private void MoveToPlayer()
        {
            agent.SetDestination(target.position);
            anim.SetFloat("Movement", 1f, 0.3f, Time.deltaTime);
        }
        private void RotateToTarget()
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }

        private int CalculateDamage(int maxDamage)
        {
            var rnd = new System.Random(Guid.NewGuid().GetHashCode());
            var damage = rnd.Next(1, maxDamage + 1);

            // If the damage is at least 75% of the maxDamage, apply a critical bonus
            if (damage >= maxDamage * 0.75f)
                damage += rnd.Next(1, 6);

            return damage;
        }
    }
}
