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
        //this needs to be scripted after we implement load/save
        public void LoadGameButton()
        {

        }
        //this needs to be scripted when we make our minds up about settings
        public void SettingsButton()
        {

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
            //ovo se mora zamjeniti sa .LoadSceneAync samo kada se napravi pocetna cut scena
            SceneManager.LoadScene("Cutscene Level");
        }
        #endregion
        
        #region Settings
        #endregion
        
        #region Credits
        public void BackButtonCredits()
        {
            creditsPanel.SetActive(false);
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
