using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TheSignal
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject HealthBar;
        [SerializeField] private GameObject TipsUI;
        [SerializeField] private GameObject SlowMoBar;
        [SerializeField] private GameObject laodingScreen;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HealthBar.SetActive(false);
                TipsUI.SetActive(false);
                SlowMoBar.SetActive(false);
                laodingScreen.SetActive(true);
                //string name =SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1).name;
                //SceneManager.LoadScene(name);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
