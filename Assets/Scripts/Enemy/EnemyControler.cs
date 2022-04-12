using System.Collections;
using System.Collections.Generic;
using TheSignal.Player;
using TheSignal.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TheSignal.Scripts.Enemy
{
    public class EnemyControler : MonoBehaviour
    {
        private NavMeshAgent agent = null;
        [SerializeField] private Transform target;
        private Animator anim = null;
        public float enemyDistance = 0.7f;
        public int MaxHealth = 100;
        int CurrentHealth;
        [SerializeField] private GameObject rightFist;
        [SerializeField] private GameObject LeftFist;
        private float enemyCooldown = 3;
        private float lastAtack = 0;
        private void Start()
        {
            getRefeences();
        }
        private void Update()
        {

            if (Vector3.Distance(transform.position, target.position) < enemyDistance)
            {
                gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                anim.Play("Soft Attack");
                //if (Time.time-lastAtack>enemyCooldown)
                //{
                //    lastAtack = Time.time;
                //    target.GetComponent<PlayerHealthDamage>().TakeDamage(20);
                //}
            }
            else
            {
                MoveToPlayer();
                RotateToTarget();
                rightFist.GetComponent<Collider>().enabled = false;
            }
        }
        private void getRefeences()
        {
            CurrentHealth = MaxHealth;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth<=0)
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
            anim.SetFloat("Movement",1f,0.3f,Time.deltaTime);
        }
        private void RotateToTarget()
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
