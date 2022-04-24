using System.Collections;
using System.Collections.Generic;
using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Journal
{
    public class Journal : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject JournalUI;
        [SerializeField] private GameObject player;

        private InputManager inputManager;
        private Renderer textE;
        
        void Awake()
        {
            textE = this.GetComponent<Renderer>();
            inputManager = player.GetComponent<InputManager>();
        }
        private void Update()
        {
            if (this.gameObject.activeInHierarchy && inputManager.isInteracting)
            {
                OpenJournal();
            }
            else if (this.gameObject.activeInHierarchy && inputManager.isPressingESC)
            {
                CloseJournal();
            }
        }
        private void OpenJournal()
        {
            JournalUI.SetActive(true);
            Time.timeScale = 0.0f;
            inputManager.isInteracting = false;
            mainCamera.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        private void CloseJournal()
        {
            JournalUI.SetActive(false);
            Time.timeScale = 1.0f;
            mainCamera.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inputManager.isPressingESC = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                textE.enabled = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                textE.enabled=false;
            }
        }
    }
}
