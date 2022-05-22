using TheSignal.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSignal.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject newGamePanel;
        public GameObject mainPanel;
        public GameObject quitGamePanel;
        public GameObject creditsPanel;
        public GameObject loadingPanel;
        public GameObject settingsPanel;
        public GameObject helpPanel;
        
        #region MainPanel
        public void NewGameButton()
        {
            mainPanel.SetActive(false);
            newGamePanel.SetActive(true);
        }
        public void QuitGameButton()
        {
            mainPanel.SetActive(false);
            quitGamePanel.SetActive(true);
        }
        public void CreditsButton()
        {
            mainPanel.SetActive(false);
            creditsPanel.SetActive(true);
        }
        public void LoadGameButton()
        {
            loadingPanel.SetActive(true);
            PlayerData data = SaveSystem.LoadPlayer();
            if (data!=null)
            {
                SceneManager.LoadScene(data.level);
            }
            else
                SceneManager.LoadScene("Cutscene Level");
        }
        public void SettingsButton()
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }

        #endregion
        
        #region Newgame
        public void CancelNewGame()
        {
            newGamePanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        public void YesNewGame(string lvlName)
        {
            SceneManager.LoadScene("Cutscene Level");
        }
        #endregion
        
        #region Settings
        public void BackSettingsButton()
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
        #endregion
        
        #region Credits
        public void BackButtonCredits()
        {
            creditsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        #endregion

        #region Help
        public void HelpButton()
        {
            mainPanel.SetActive(false);
            helpPanel.SetActive(true);
        }
        public void HelpBackButtonCredits()
        {
            helpPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        #endregion

        #region Quit
        public void YesQuit()
        {
            Application.Quit();
        }
        public void NoQuit()
        {
            quitGamePanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        #endregion
    }
}
