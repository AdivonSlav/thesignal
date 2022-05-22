using System.Linq;
using TheSignal.Camera;
using TheSignal.Player.Input;
using TheSignal.Player.Journal;
using UnityEngine;
using UnityEngine.SceneManagement;
using TheSignal.SaveLoad;
using UnityEngine.UI;

namespace TheSignal.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool GameIsPaused = false;
        
        private InputManager inputManager;
        private CinemachineController cinemachineController;
        private PlayerJournal playerJournal;
        
        [SerializeField] private GameObject player;
        [Header("UI")]
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject settingsUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject deathScreenUI;
        [SerializeField] private GameObject objectiveActive;
        [SerializeField] private GameObject objectiveInactive;
        [SerializeField] private GameObject healthAndMissionUI;
        [SerializeField] private GameObject journalUI;
        [SerializeField] private GameObject HelpUI;
        
        private void Awake()
        {
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
            inputManager = player.GetComponent<InputManager>();
            playerJournal = player.GetComponent<PlayerJournal>();
        }
        void Update()
        {
            if (!deathScreenUI.activeInHierarchy)
            {
                if (inputManager.isExiting)
                {
                    if (inputManager.isExiting && !journalUI.activeInHierarchy && !playerJournal.journalOpened)
                    {
                        if (GameIsPaused)
                            Resume();
                        else
                            Pause();
                    }
                }
                if (inputManager.isPressingTab)
                {
                    ToggleObjectives();
                }
            }
            else if (!GameIsPaused)
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
            settingsUI.SetActive(false);
            GameIsPaused = false; 
            cinemachineController.TogglePause(inputManager.isAiming);
            healthAndMissionUI.SetActive(true);
        }
        public void MainMenu()
        {
            pauseMenuUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }
        public void Help()
        {
            pauseMenuUI.SetActive(false);
            HelpUI.SetActive(true);
        }
        public void HelpBackButton()
        {
            HelpUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        public void Load()
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data != null)
            {
                Resume();
                playerJournal.LoadEntries(data.addedEntries);
                SceneManager.LoadScene(data.level);
            }
        }
        public void Save()
        {
            SaveSystem.SavePlayer(SceneManager.GetActiveScene().buildIndex, AddedEntries.entries.Keys.ToList());
        }
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            GameIsPaused = true;
            cinemachineController.TogglePause(inputManager.isAiming);
            healthAndMissionUI.SetActive(false);
        }
        void PauseDead()
        {
            GameIsPaused = true;
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

        public void SettingsButton()
        {
            settingsUI.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        public void SettingsBackButton()
        {
            settingsUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
    }
}
