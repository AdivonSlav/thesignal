using TheSignal.Weapons;
using UnityEngine;

namespace TheSignal.Enemy
{
    public class HitPoint : MonoBehaviour
    {
        private EnemyController enemyController;

        [SerializeField] private int damageBonus;
        
        private void Awake()
        {
            enemyController = GetComponentInParent<EnemyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                int maxDamage = other.gameObject.GetComponent<Projectile>().maxDamage;
                enemyController.TakeDamage(damageBonus, maxDamage);
            }
        }
    }
}
