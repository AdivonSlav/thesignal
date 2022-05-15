using TheSignal.Weapons;
using UnityEngine;

namespace TheSignal.Enemy
{
    public class HitPointBoss : MonoBehaviour
    {
        private BossControler bossControler;

        [SerializeField] private int damageBonus;

        private void Awake()
        {
            bossControler = GetComponentInParent<BossControler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                int maxDamage = other.gameObject.GetComponent<Projectile>().maxDamage;
                bossControler.TakeDamage(damageBonus, maxDamage);
            }
        }
    }
}
