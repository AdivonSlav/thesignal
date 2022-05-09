using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TheSignal.Cutscene
{
    public class CutsceneButtons : MonoBehaviour
    {
        [SerializeField] private GameObject panel1;
        [SerializeField] private GameObject panel2;
        [SerializeField] private GameObject button1;
        [SerializeField] private GameObject button2;
        public void ContinueButton()
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            button1.SetActive(false);
            button2.SetActive(true);
        }
        public void PlayButton()
        {
            SceneManager.LoadScene("First Level");
        }
    }
}
