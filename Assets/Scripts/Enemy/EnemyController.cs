using UnityEngine;
using UnityEngine.AI;

namespace TheSignal.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int MaxHealth=100;
        [SerializeField] private Transform target;
        [SerializeField] private float enemyDistance = 0.7f;

        private NavMeshAgent agent = null;
        private Animator anim = null;
        int CurrentHealth;
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
            }
            else
            {
                MoveToPlayer();
                RotateToTarget();
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
