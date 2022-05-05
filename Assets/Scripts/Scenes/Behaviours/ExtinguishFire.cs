using UnityEngine;

namespace TheSignal.Scenes.Behaviours
{
    public class ExtinguishFire : MonoBehaviour
    {
        [SerializeField] private GameObject sprinklerFusebox;
        [SerializeField] private float healthLoss;
        [SerializeField] private ParticleSystem smoke;
        [SerializeField] private ParticleSystem steam;

        private ParticleSystem fire;
        private SprinklerFix sprinklerFix;
        private float fireHealth = 100.0f;
        private bool fireExtinguished = false;
        
        private void Awake()
        {
            fire = GetComponent<ParticleSystem>();
            
            sprinklerFix = sprinklerFusebox.GetComponentInChildren<SprinklerFix>();
        }

        private void LateUpdate()
        {
            if (sprinklerFix.sprinklersStarted)
            {
                while (!fireExtinguished)
                {
                    smoke.Stop(true);
                    steam.Play(true);
                    fireHealth -= healthLoss;

                    if (fireHealth <= 0.0f)
                    {
                        fireExtinguished = true;
                        fire.Stop(true);
                    }
                }
            }
        }
    }
}
