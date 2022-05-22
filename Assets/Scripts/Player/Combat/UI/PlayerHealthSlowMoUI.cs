using UnityEngine;
using UnityEngine.UI;
using TheSignal.Player.Input;
using UnityEngine.SceneManagement;

namespace TheSignal.Player.Combat.UI
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerHealthSlowMoUI : MonoBehaviour
    {
        private InputManager inputManager;

        [SerializeField] GameObject PauseScreenUI;
        public GameObject deathScreen;
        public GameObject DamageScreen;
        public Slider Healthslider;
        public Slider SlowMoSlider;
        public int CurrentLevel;
        private Animator anim;

        private float changedTime;
        private float timeOfSlowMoDeactivation;
        
        [HideInInspector] public float currentHealth;
        private float maxSlowMo;
        private float currentSlowMo;
        private float timeOfSlowMo;
        private bool slowMo;
        private bool dead;
        private float timeOfDeath;
        private float timeOfDamage;
        private bool isDamageAnimPlaying;
        
        private void Start()
        {
            inputManager = GetComponent<InputManager>();
            anim = GetComponent<Animator>();
            currentHealth = 100;
            maxSlowMo = SlowMoSlider.maxValue;
            currentSlowMo = maxSlowMo;
            SlowMoSlider.value = maxSlowMo;
            Healthslider.value = 100;
        }
        private void Update()
        {
            CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentHealth <= 0 && !dead)
            {
                timeOfDeath = Time.realtimeSinceStartup;
                Time.timeScale = 0.2f;
                anim.SetTrigger("Death");
                dead = true;
            }
            if (Time.realtimeSinceStartup - timeOfDeath > 0.5 && dead)  
                Die();

            if (inputManager.isPressingK && !slowMo && currentSlowMo == maxSlowMo)
            {
                SlowMoTriggered();
            }
            if (slowMo && currentSlowMo > 0)
            {
                if (timeOfSlowMo + maxSlowMo > Time.realtimeSinceStartup && ((int)changedTime)+1== ((int)Time.realtimeSinceStartup))
                {
                    currentSlowMo -= 1;
                    SlowMoSlider.value = currentSlowMo;
                    changedTime += 1;
                }
            }
            if ((Time.realtimeSinceStartup - timeOfSlowMo) > maxSlowMo && slowMo)
            {
                SlowMoEnded();
            }
            if (!slowMo && currentSlowMo < maxSlowMo)
            {
                if (timeOfSlowMoDeactivation+80>Time.realtimeSinceStartup&& ((int)changedTime) + 1 == ((int)Time.realtimeSinceStartup))
                {
                    currentSlowMo += 0.25f;
                    SlowMoSlider.value = currentSlowMo;
                    changedTime += 1;
                }
            }
            if (DamageScreen.activeInHierarchy)
            {
                if (Time.realtimeSinceStartup - timeOfDamage>2)
                {
                    isDamageAnimPlaying = false;
                    DamageScreen.SetActive(false);
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
            if (!PauseScreenUI.activeInHierarchy)
            {

               if (other.CompareTag("Fist"))
               {
                TakeDamage(20);
               }
            if (other.CompareTag("FirstAid") && currentHealth<100)
            {
                Heal();
                other.gameObject.SetActive(false);
            }
            if (other.CompareTag("SlowMoKit") && currentSlowMo<maxSlowMo && !slowMo)
            {
                RefilSlowMo();
                other.gameObject.SetActive(false);
            }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (!PauseScreenUI.activeInHierarchy)
            {
                if (other.CompareTag("Flames"))
                {
                    TakeDamage(0.5f);
                }
            }
        }
        private void RefilSlowMo()
        {
            currentSlowMo = maxSlowMo;
            SlowMoSlider.value = currentHealth;
        }
        private void Heal()
        {
                if (currentHealth<75)
                {
                    currentHealth += 25;
                    Healthslider.value = currentHealth;
                }
                else
                {
                    currentHealth = 100;
                    Healthslider.value = currentHealth;
                }
        }
        public void TakeDamage(float damage)
        {
            if (!isDamageAnimPlaying)
            {
                isDamageAnimPlaying = true;
                DamageScreen.SetActive(true);
            }
            timeOfDamage = Time.realtimeSinceStartup;
            currentHealth -= damage;
            Healthslider.value = currentHealth;
        }
        private void Die()
        {
            dead = false;
            deathScreen.SetActive(true);
            currentHealth = 100;
            Start();
        }
    }
}
