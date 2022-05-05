using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Scenes.Behaviours
{
    public class DoorControllerAlt : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject door;
        [SerializeField] private MeshRenderer[] popupRenderers;
        
        private InputManager inputManager;
        private Animator animator;
        private bool playerPresent = false;
        
        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            animator = door.GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            if (playerPresent)
            {
                foreach (var popup in popupRenderers)
                {
                    popup.enabled = true;
                }
                
                if (inputManager.isInteracting)
                {
                    animator.SetBool("isOpening", true);
                }
            }
            else
            {
                foreach (var popup in popupRenderers)
                {
                    popup.enabled = false;
                }
                
                animator.SetBool("isOpening", false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                playerPresent = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                playerPresent = false;
        }
    }
}
