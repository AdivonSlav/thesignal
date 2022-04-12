using System.Collections;
using System.Collections.Generic;
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
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
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
