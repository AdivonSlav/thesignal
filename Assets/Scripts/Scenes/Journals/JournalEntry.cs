using System;
using TheSignal.Camera;
using TheSignal.Player.Journal;
using TheSignal.Player.Input;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace TheSignal.Scenes.Behaviours
{
    public class JournalEntry : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject popup;
        [SerializeField] private TextAsset journalEntry;
        [Header("UI")]
        [SerializeField] private GameObject journalImage;
        [SerializeField] private Text headerText;
        [SerializeField] private Text bodyText;

        private InputManager inputManager;
        private CinemachineController cinemachineController;
        private Transform mainCameraTransform;
        private PlayerJournal playerJournal;

        private GameObject entryToDestroy;
        private bool playerPresent;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
            mainCameraTransform = UnityEngine.Camera.main.transform;
            playerJournal = player.GetComponent<PlayerJournal>();
            
            popup.SetActive(false);
        }

        private void Update()
        {
            if (inputManager.isInteracting && playerPresent)
                OpenJournal();
            else if (inputManager.isExiting && journalImage.activeInHierarchy)
                CloseJournal();
        }

        private void OpenJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);

            headerText.text = journalEntry.name;
            bodyText.text = journalEntry.text;
            playerJournal.AddEntry(journalEntry);
            journalImage.SetActive(true);
            inputManager.isInteracting = false;
        }
        
        private void CloseJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);
            journalImage.SetActive(false);
            inputManager.isExiting = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                popup.SetActive(true);
                playerPresent = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                popup.SetActive(false);
                playerPresent = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            RotatePopup();
        }

        private void RotatePopup()
        {
            popup.transform.LookAt(mainCameraTransform);
            popup.transform.rotation = Quaternion.LookRotation(mainCameraTransform.forward);
        }
    }
}
