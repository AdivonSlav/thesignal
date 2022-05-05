using TheSignal.Player;
using TheSignal.Player.Input;
using UnityEngine;
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
        
        private bool isInTheZone;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            cinemachineController = Camera.main.GetComponent<CinemachineController>();
            mainCameraTransform = Camera.main.transform;
            playerJournal = player.GetComponent<PlayerJournal>();
        }
        
        private void Update()
        {
            popup.SetActive(isInTheZone);
            
            if (isInTheZone)
                RotatePopup();
            
            if (isInTheZone && inputManager.isInteracting)
            {
                OpenJournal();
            }
            if (journalImage.activeInHierarchy && inputManager.isExiting)
            {
                CloseJournal();
            }
        }
        
        private void OpenJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);

            headerText.text = journalEntry.name;
            bodyText.text = journalEntry.text;
            journalImage.SetActive(true);
            inputManager.isInteracting = false;
        }
        
        private void CloseJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);
            
            playerJournal.AddEntry(journalEntry);
            journalImage.SetActive(false);
            inputManager.isExiting = false;

            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInTheZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInTheZone = false;
            }
        }

        private void RotatePopup()
        {
            popup.transform.LookAt(mainCameraTransform);
            popup.transform.rotation = Quaternion.LookRotation(mainCameraTransform.forward);
        }
    }
}
