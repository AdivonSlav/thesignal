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
        [SerializeField] private GameObject[] terminals;

        private InputManager inputManager;
        private Animator doorAnimator;
        private MeshRenderer[] terminalRenderers = new MeshRenderer[2];
        private bool playerPresent;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            doorAnimator = door.GetComponent<Animator>();

            for (var i = 0; i < terminals.Length; i++)
            {
                // [1] in order to skip the renderer returned from the actual GameObject terminal renderer
                terminalRenderers[i] = terminals[i].GetComponentsInChildren<MeshRenderer>()[1];
                
                // Cloning the material for the renderer so we can update it for each object separately
                var terminalMaterial = terminals[i].GetComponent<Renderer>().material;
                terminals[i].GetComponent<Renderer>().material = new Material(terminalMaterial);
            }
        }

        void LateUpdate()
        {
            if (playerPresent)
            {
                foreach (var terminalRenderer in terminalRenderers)
                    terminalRenderer.enabled = true;

                if (inputManager.isInteracting)
                {
                    foreach (var terminal in terminals)
                    {
                        terminal.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        terminal.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        DynamicGI.UpdateEnvironment();
                    }
                    
                    doorAnimator.SetBool("isOpening", true);
                }
            }
            else
            {
                for (var i = 0; i < terminals.Length; i++)
                {
                    terminalRenderers[i].enabled = false;
                    terminals[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
                    terminals[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    DynamicGI.UpdateEnvironment();
                }
                
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
