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
        public GameObject DeadScreen;
        public Slider slider;
        bool dead = false;
        float timeOfDeath = 0;
        private Animator anim = null;
        private void Start()
        {
            anim = GetComponent<Animator>();
            CurrentHealth = 100;
            slider.value = 100;
        }
        private void Update()
        {
            
            if (CurrentHealth<=0 && !dead)
            {
                timeOfDeath = Time.realtimeSinceStartup;
                Time.timeScale = 0.2f;
                anim.SetTrigger("Death");
                dead = true;
            }
            if (Time.realtimeSinceStartup - timeOfDeath > 10 && dead)
            {
                Die();
            }
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
        private void Die()
        {
            dead = false;
            DeadScreen.SetActive(true);
            Time.timeScale = 0.0f;
            CurrentHealth = 100;
            Start();
        }
    }
}
