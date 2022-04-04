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
        private InputManager inputManager;
        [SerializeField] private GameObject door;

        private MeshRenderer terminalRenderer;
        private Animator doorAnimator;
        private bool playerPresent;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            terminalRenderer = GetComponentInParent<MeshRenderer>();
            doorAnimator = door.GetComponent<Animator>();
        }

        void LateUpdate()
        {
            if (playerPresent)
            {
                terminalRenderer.enabled = true;

                if (inputManager.isInteracting)
                {
                    doorAnimator.SetBool("isOpening", true);
                }
            }
            else
            {
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
