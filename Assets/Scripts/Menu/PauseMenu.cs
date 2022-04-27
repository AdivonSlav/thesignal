using System.Collections;
using System.Collections.Generic;
using TheSignal.Player.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSignal.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        
        private InputManager inputManager;
        
        [SerializeField] private GameObject player;
        [Header("Cameras")]
        [SerializeField]private GameObject normalCamera;
        [SerializeField]private GameObject aimCamera;
        [Header("UI")]
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject deathScreenUI;
        [SerializeField] private GameObject objectiveActive;
        [SerializeField] private GameObject objectiveInactive;
        [SerializeField] private GameObject healthAndMissionUI;
        [SerializeField] private GameObject JournalUI;

        private bool objectiveTab;
        
        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
        }
        void Update()
        {
            if (!deathScreenUI.activeInHierarchy)
            {
                if (inputManager.isPressingESC && !JournalUI.activeInHierarchy && !inputManager.isAiming)
                {
                    if(GameIsPaused)
                        Resume();
                    else
                        Pause();
                }
                if (inputManager.isPressingI)
                {
                    if (objectiveTab)
                    {
                        ObjectiveTabActive();
                    }
                    else
                    {
                        ObjectiveTabInactive();
                    }
                }

            }
            else
            {
                PauseDead();
            }
        }
        #region ObjectiveTab
        void ObjectiveTabActive()
        {
            objectiveTab = false;
            objectiveActive.SetActive(false);
            objectiveInactive.SetActive(true);
            inputManager.isPressingI = false;
        }
        void ObjectiveTabInactive()
        {
            objectiveTab = true;
            objectiveInactive.SetActive(false);
            objectiveActive.SetActive(true);
            inputManager.isPressingI = false;
        }
        #endregion
        #region MainPanel
        public void Resume()
        {
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            inputManager.isPressingESC = false;
            normalCamera.SetActive(true);
            aimCamera.SetActive(true);
            healthAndMissionUI.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
            Time.timeScale = 0.0f;
            GameIsPaused = true;
            inputManager.isPressingESC = false;
            normalCamera.SetActive(true);
            aimCamera.SetActive(true);
            healthAndMissionUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        void PauseDead()
        {
            Time.timeScale = 0.0f;
            GameIsPaused = true;
            inputManager.isPressingESC = false;
            normalCamera.SetActive(true);
            aimCamera.SetActive(true);
            healthAndMissionUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
