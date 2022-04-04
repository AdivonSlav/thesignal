using System.Collections;
using System.Collections.Generic;
using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject PauseMenuUI;
        private InputManager inputManager;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject camera;
        private void Awake()
        {
            inputManager = player.GetComponent<InputManager>();
        }
        void Update()
        {
            if (inputManager.isPressingESC)
            {
                if(GameIsPaused)
                    Resume();
                else
                    Pause();
            }
        }
        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            inputManager.isPressingESC = false;
            camera.SetActive(true);

        }
        public void MainMenu()
        {
            //here we will load the main menu.
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
            camera.SetActive(false);
            
        }
    }
}
