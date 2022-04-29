using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Scenes.Behaviours
{
    public class Journal : MonoBehaviour
    {
        [SerializeField] private GameObject journalUI;
        [SerializeField] private GameObject player;
        
        private bool isInTheZone;

        private InputManager inputManager;
        private CinemachineController cinemachineController;

        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
            cinemachineController = Camera.main.GetComponent<CinemachineController>();
        }
        
        private void Update()
        {
            if (isInTheZone && inputManager.isInteracting)
            {
                OpenJournal();
            }
            if (journalUI.activeInHierarchy && inputManager.isPressingESC)
            {
                CloseJournal();
            }
        }
        
        private void OpenJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);
            
            journalUI.SetActive(true);
            Time.timeScale = 0.0f;
            inputManager.isInteracting = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void CloseJournal()
        {
            cinemachineController.TogglePause(inputManager.isAiming);
            
            journalUI.SetActive(false);
            Time.timeScale = 1.0f;
            inputManager.isPressingESC = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInTheZone = true;
            }
        }
    }
}
