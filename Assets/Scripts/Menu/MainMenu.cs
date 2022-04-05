using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSignal.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject NewGamePanel;
        public GameObject MainPanel;
        public GameObject QuitGamePanel;
        public GameObject CreditsPanel;
        #region MainPanel
        public void NewGameButton()
        {
            MainPanel.SetActive(false);
            NewGamePanel.SetActive(true);
        }
        public void QuitGameButton()
        {
            MainPanel.SetActive(false);
            QuitGamePanel.SetActive(true);
        }
        public void CreditsButton()
        {
            MainPanel.SetActive(false);
            CreditsPanel.SetActive(true);
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
            NewGamePanel.SetActive(false);
            MainPanel.SetActive(true);
        }
        public void YesNewGame(string lvlName)
        {
            //ovo se mora zamjeniti sa .LoadSceneAync samo kada se napravi pocetna cut scena
            SceneManager.LoadScene(lvlName);
        }
        #endregion
        #region Settings
        #endregion
        #region Credits
        public void BackButtonCredits()
        {
            CreditsPanel.SetActive(false);
            MainPanel.SetActive(true);
        }
        #endregion
        #region Quit
        public void YesQuit()
        {
            Application.Quit();
        }
        public void NoQuit()
        {
            QuitGamePanel.SetActive(false);
            MainPanel.SetActive(true);
        }
        #endregion
    }
}
