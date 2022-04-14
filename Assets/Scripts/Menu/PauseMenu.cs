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
        public GameObject PauseMenuUI;
        public GameObject MainMenuUI;
        public GameObject DeadScreenUI;
        private InputManager inputManager;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject camera_;
        [SerializeField] private GameObject HealthAndMissionUI;
        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
        }
        void Update()
        {
            if (!DeadScreenUI.activeInHierarchy)
            {
                if (inputManager.isPressingESC)
                {
                    if(GameIsPaused)
                        Resume();
                    else
                        Pause();
                }
            }
            else
            {
                PauseDead();
            }
        }
        #region MainPanel
        public void Resume()
        {
            MainMenuUI.SetActive(false);
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            inputManager.isPressingESC = false;
            camera_.SetActive(true);
            HealthAndMissionUI.SetActive(true);
        }
        public void MainMenu()
        {
            PauseMenuUI.SetActive(false);
            MainMenuUI.SetActive(true);
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
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0.0f;
            GameIsPaused = true;
            inputManager.isPressingESC = false;
            camera_.SetActive(false);
            HealthAndMissionUI.SetActive(false);
        }
        void PauseDead()
        {
            Time.timeScale = 0.0f;
            GameIsPaused = true;
            inputManager.isPressingESC = false;
            camera_.SetActive(false);
            HealthAndMissionUI.SetActive(false);
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
            MainMenuUI.SetActive(false);
            PauseMenuUI.SetActive(true);
        }
        #endregion
        #region DeadScreen
        public void DeadScreenButtonYes()
        {
            Scene current = SceneManager.GetActiveScene();
            GameIsPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(current.name);
            MainMenuUI.SetActive(false);
            PauseMenuUI.SetActive(false);
            DeadScreenUI.SetActive(false);
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
