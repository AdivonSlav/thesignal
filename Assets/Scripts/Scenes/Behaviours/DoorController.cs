using UnityEngine;
using TheSignal.Player.Input;
using TheSignal.SFX;
using TMPro;
using UnityEngine.Rendering.Universal.Internal;

namespace TheSignal.Scenes.Behaviours
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject door;
        [SerializeField] private MeshRenderer[] terminalRenderers;
        [SerializeField] private GameObject[] terminalPopups;

        private InputManager inputManager;
        private Animator doorAnimator;
        private DoorSoundController soundController;
        private bool playerPresent;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            doorAnimator = door.GetComponent<Animator>();
            soundController = door.GetComponent<DoorSoundController>();

            // Since we use one material for all terminal renderers in the scene, we must clone the material at runtime in order to change
            // the emission color for just this door
            foreach (var terminalRenderer in terminalRenderers)
            {
                terminalRenderer.material = new Material(terminalRenderer.material);
            }
        }

        void LateUpdate()
        {
            foreach (var popup in terminalPopups)
            {
                popup.SetActive(playerPresent);
            }
            
            if (playerPresent)
            {
                if (inputManager.isInteracting)
                {
                    foreach (var terminalRenderer in terminalRenderers)     
                    {
                        terminalRenderer.material.SetColor("_EmissionColor", Color.green);
                    }
                    
                    doorAnimator.SetBool("isOpening", true);
                    soundController.PlayConfirmation();
                }
            }
            else
            {
                if (doorAnimator.GetBool("isOpening"))
                {
                    foreach (var terminalRenderer in terminalRenderers)     
                    {
                        terminalRenderer.material.SetColor("_EmissionColor", Color.yellow);
                    }
                    
                    doorAnimator.SetBool("isOpening", false);
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerPresent = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerPresent = false;
            }
        }
    }
}
