using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSignal.Player.Input;

namespace TheSignal.Scenes.Behaviours
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject door;
        [SerializeField] private MeshRenderer[] terminalRenderers;
        
        private InputManager inputManager;
        private Animator doorAnimator;
        private bool playerPresent;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            doorAnimator = door.GetComponent<Animator>();
        }

        void LateUpdate()
        {
            if (playerPresent)
            {
                foreach (var terminalRenderer in terminalRenderers)
                    terminalRenderer.enabled = true;

                if (inputManager.isInteracting)
                {
                    doorAnimator.SetBool("isOpening", true);
                }
            }
            else
            {
                foreach (var terminalRenderer in terminalRenderers)
                    terminalRenderer.enabled = false;
                
                doorAnimator.SetBool("isOpening", false);
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
