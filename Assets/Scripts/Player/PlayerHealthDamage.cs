using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSignal.Scripts.HealthBarScript;
using UnityEngine.UI;
namespace TheSignal.Scripts.Player
{
    public class PlayerHealthDamage : MonoBehaviour
    {
        private HealthBar healthBar;
        private int CurrentHealth;
        public Slider slider;
        private void Start()
        {
            CurrentHealth = 100;
            slider.value = 100;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fist")
            {
                TakeDamage(20);
            }
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            slider.value = CurrentHealth;
        }
    }
}
