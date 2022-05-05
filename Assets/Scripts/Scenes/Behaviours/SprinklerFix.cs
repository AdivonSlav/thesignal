using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Scenes.Behaviours
{
    public class SprinklerFix : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject fixPopup;
        [SerializeField] private ParticleSystem fuxeboxSparks;
        [SerializeField] private ParticleSystem[] sprinklerShowers;

        [HideInInspector] public bool sprinklersStarted = false;
        private InputManager inputManager;
        private bool playerPresent = false;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
        }

        private void FixedUpdate()
        {
            if (playerPresent)
            {
                fixPopup.SetActive(true);
                
                if (inputManager.isInteracting)
                {
                    fuxeboxSparks.Stop(true);

                    foreach (var sprinkler in sprinklerShowers)
                    {
                        sprinkler.Play(true);
                    }

                    sprinklersStarted = true;
                    fixPopup.SetActive(false);
                    this.enabled = false;
                }
            }
            else
            {
                fixPopup.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                playerPresent = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                playerPresent = false;
        }
    }
}
