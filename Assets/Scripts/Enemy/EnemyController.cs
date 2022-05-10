using System.Diagnostics;
using TheSignal.Managers;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace TheSignal.Enemy
{
    public class EnemyController : TrackedEntity
    {
        [SerializeField] private int MaxHealth=100;
        [SerializeField] private Transform target;
        [SerializeField] private float enemyDistance = 0.7f;

        private NavMeshAgent agent;
        private Animator anim = null;
        int CurrentHealth;
        
        private void Start()
        {
            GetReferences();
        }
        private void Update()
        {
            agent.velocity = Vector3.zero;
            
            if (!this.isRunning)
            {
                anim.SetFloat("Movement", 0.0f, 0.3f, Time.deltaTime);
                return;
            }

            var targetDistance = Vector3.Distance(transform.position, target.position);
            
            if (targetDistance < enemyDistance)
                anim.Play("Soft Attack");
            else
            {
                MoveToPlayer();
                RotateToTarget();
            }
        }
        private void GetReferences()
        {
            CurrentHealth = MaxHealth;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        private void Die()
        {

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
    }
}
