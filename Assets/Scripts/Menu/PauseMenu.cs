using System.Collections;
using System.Collections.Generic;
using TheSignal.Player.Input;
using TheSignal.Scenes.Behaviours;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheSignal.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        
        private InputManager inputManager;
        private CinemachineController cinemachineController;
        
        [SerializeField] private GameObject player;
        [Header("UI")]
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject deathScreenUI;
        [SerializeField] private GameObject objectiveActive;
        [SerializeField] private GameObject objectiveInactive;
        [SerializeField] private GameObject healthAndMissionUI;
        [SerializeField] private GameObject journalUI;

        private bool objectiveTab;
        
        private void Awake()
        {
            cinemachineController = Camera.main.GetComponent<CinemachineController>();
            inputManager = player.GetComponent<InputManager>();
        }
        void Update()
        {
            if (!deathScreenUI.activeInHierarchy)
            {
                if (inputManager.isExiting && !journalUI.activeInHierarchy && !journalUI.activeInHierarchy)
                {
                    if(GameIsPaused)
                        Resume();
                    else
                        Pause();
                }
                if (inputManager.isPressingTab)
                {
                    ToggleObjectives();
                }
            }
            else
            {
                PauseDead();
            }
        }
        #region ObjectiveTab
        void ToggleObjectives()
        {
            objectiveActive.SetActive(!objectiveActive.gameObject.activeInHierarchy);
            objectiveInactive.SetActive(!objectiveActive.gameObject.activeInHierarchy);
            inputManager.isPressingTab = false;
        }
        #endregion
        
        #region MainPanel
        public void Resume()
        {
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            inputManager.isExiting = false;
            cinemachineController.TogglePause(inputManager.isAiming);
            healthAndMissionUI.SetActive(true);
            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }
        public void MainMenu()
        {
            pauseMenuUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void Load()
        {

        }
        public void Save()
        {

        }
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            inputManager.isExiting = false;
            cinemachineController.TogglePause(inputManager.isAiming);
            healthAndMissionUI.SetActive(false);
        }
        void PauseDead()
        {
            GameIsPaused = true;
            inputManager.isExiting = false;
            cinemachineController.TogglePause(inputManager.isAiming);
            healthAndMissionUI.SetActive(false);
        }
        #endregion
        #region Main Menu Button
        public void MainMenuButtonYes(string MainMenuName)
        {
            GameIsPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(MainMenuName);
        }
        public void MainMenuButtonNo()
        {
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        #endregion
        #region DeadScreen
        public void DeadScreenButtonYes()
        {
            Scene current = SceneManager.GetActiveScene();
            GameIsPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(current.name);
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            deathScreenUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Resume();
        }
        public void DeadScreenButtonNo(string MainMenuName)
        {
            GameIsPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(MainMenuName);
        }
        #endregion
    }
}
