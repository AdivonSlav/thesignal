using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheSignal.Player.Input;

namespace TheSignal.Scripts.Player
{
    public class PlayerHealthSlowMoUI : MonoBehaviour
    {
        private int CurrentHealth;
        private float CurrentSlowmo;
        public GameObject DeadScreen;
        public Slider Healthslider;
        public Slider SlowMoSlider;
        private InputManager inputManager;
        bool dead = false;
        bool slowMo = false;
        float timeOfDeath = 0;
        float timeOfSlowMo = 0;
        private Animator anim = null;
        float changedTime;
        float timeOfSlowMoDeactivation = 0f;
        private float MaxSlowMo;
        private void Start()
        {
            inputManager = GetComponent<InputManager>();
            anim = GetComponent<Animator>();
            CurrentHealth = 100;
            MaxSlowMo = SlowMoSlider.maxValue;
            CurrentSlowmo = MaxSlowMo;
            SlowMoSlider.value = MaxSlowMo;
            Healthslider.value = 100;
        }
        private void Update()
        {

            if (CurrentHealth <= 0 && !dead)
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
            if (inputManager.isPressingK && !slowMo && CurrentSlowmo==MaxSlowMo)
            {
                SlowMoTriggered();
            }
            if (slowMo && CurrentSlowmo > 0)
            {
                if (timeOfSlowMo + MaxSlowMo > Time.realtimeSinceStartup && ((int)changedTime)+1== ((int)Time.realtimeSinceStartup))
                {
                    CurrentSlowmo -= 1;
                    SlowMoSlider.value = CurrentSlowmo;
                    changedTime += 1;
                }
            }
            if ((Time.realtimeSinceStartup - timeOfSlowMo) > MaxSlowMo && slowMo)
            {
                SlowMoEnded();
            }
            if (!slowMo && CurrentSlowmo < MaxSlowMo)
            {
                if (timeOfSlowMoDeactivation+80>Time.realtimeSinceStartup&& ((int)changedTime) + 1 == ((int)Time.realtimeSinceStartup))
                {
                    CurrentSlowmo += 0.25f;
                    SlowMoSlider.value = CurrentSlowmo;
                    changedTime += 1;
                }
            }
        }
        private void SlowMoTriggered()
        {
            timeOfSlowMo =Time.realtimeSinceStartup;
            Time.timeScale = 0.2f;
            slowMo = true;
            changedTime = timeOfSlowMo;
        }
        private void SlowMoEnded()
        {
            Time.timeScale = 1f;
            timeOfSlowMoDeactivation = Time.realtimeSinceStartup;
            changedTime = timeOfSlowMoDeactivation;
            slowMo = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fist")
            {
                TakeDamage(20);
            }
            if (other.gameObject.tag=="FirstAid" && CurrentHealth<100)
            {
                Heal();
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.tag == "SlowMoKit" && CurrentSlowmo<MaxSlowMo && !slowMo)
            {
                RefilSlowMo();
                other.gameObject.SetActive(false);
            }
        }
        private void RefilSlowMo()
        {
            CurrentSlowmo = MaxSlowMo;
            SlowMoSlider.value = CurrentHealth;
        }
        private void Heal()
        {
                if (CurrentHealth<75)
                {
                    CurrentHealth += 25;
                    Healthslider.value = CurrentHealth;
                }
                else
                {
                    CurrentHealth = 100;
                    Healthslider.value = CurrentHealth;
                }
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Healthslider.value = CurrentHealth;
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
