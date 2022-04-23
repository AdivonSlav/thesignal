using UnityEngine;
using UnityEngine.AI;

namespace TheSignal.Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(Animator))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Collider rightFist;
        [SerializeField] private int maxHealth;

        private Rigidbody enemyRB;
        private NavMeshAgent agent;
        private Animator anim;
        private float enemyDistance = 0.7f;
        private int currentHealth;
        
        private void Start()
        {
            GetReferences();
            currentHealth = maxHealth;
        }
        private void Update()
        {
            if (Vector3.Distance(transform.position, target.position) < enemyDistance)
            {
                agent.velocity = Vector3.zero;
                anim.Play("Soft Attack");
            }
            else
            {
                MoveToPlayer();
                RotateToTarget();
                rightFist.enabled = false;
            }
        }
        private void GetReferences()
        {
            enemyRB = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }
        
        private void TakeDamage(int damage)
        {   
            // To be implemented
        }
        private void Die()
        {
            // To be implemented
        }    
        
        private void MoveToPlayer()
        {
            agent.SetDestination(target.position);
            anim.SetFloat("Movement",1f,0.3f,Time.deltaTime);
        }
        
        private void RotateToTarget()
        {
            Vector3 direction = target.position - transform.position;
            enemyRB.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}
