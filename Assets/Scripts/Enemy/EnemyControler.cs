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
        private void Start()
        {
            getRefeences();
        }
        private void Update()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            float result = distance - agent.stoppingDistance;
            if (result > 1.0f)
            {
                MoveToPlayer();
                RotateToTarget();
            }
            else
            {
                anim.SetFloat("Movement", 0f,0.2f,Time.deltaTime);
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
