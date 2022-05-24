using System;
using System.Collections;
using System.Collections.Generic;
using TheSignal.Camera;
using TheSignal.SFX;
using UnityEngine;

namespace TheSignal.Player.Combat
{
    public class CombatChecker : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private SceneSoundController sceneSoundController;
        [SerializeField] private float disengageTime;

        private bool enemyPresent;
        private bool bossPresent;
        private bool enemyMusicTriggered;
        private bool bossMusicTriggered;
        private List<Collider> tempOthers;
        private CinemachineController cinemachineController;
        private GameObject boss;

        private void Awake()
        {
            tempOthers = new List<Collider>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
            boss = GameObject.FindWithTag("Boss");
        }
        private void Update()
        {
            if (cinemachineController.Paused())
                return;
            
            transform.position = player.transform.position;
            
            CheckEnemies();
            CheckToDisengage();
            CheckToPlay();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (cinemachineController.Paused())
                return;

            if (!other.CompareTag("Enemy") && !other.CompareTag("Boss"))
                return;

            if (other.CompareTag("Boss"))
            {
                bossPresent = true;
                if (!tempOthers.Contains(other))
                    tempOthers.Add(other);

                return;
            }
            
            enemyPresent = true;
            tempOthers.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (cinemachineController.Paused())
                return;
            
            if (other.CompareTag("Enemy"))
                tempOthers.Remove(other);
        }

        private IEnumerator Disengage()
        {
            var temp = disengageTime;

            while (disengageTime > 0.0f)
            {
                disengageTime -= Time.deltaTime / disengageTime;
                yield return null;
            }

            disengageTime = temp;
            enemyMusicTriggered = false;
            bossMusicTriggered = false;
            sceneSoundController.PlayTrack(enemyPresent);
        }

        private void CheckEnemies()
        {
            for (var i = 0; i < tempOthers.Count; i++)
            {
                if (!tempOthers[i])
                    tempOthers.Remove(tempOthers[i]);
            }

            if (!boss)
                bossPresent = false;
        }

        private void CheckToDisengage()
        {
            if (enemyPresent && tempOthers.Count == 0)
            {
                enemyPresent = false;
                StartCoroutine(Disengage());
            }
        }

        private void CheckToPlay()
        {
            if (!enemyMusicTriggered && tempOthers.Count != 0)
            {
                enemyMusicTriggered = true;
                sceneSoundController.PlayTrack(enemyPresent, bossPresent);
            }

            if (!bossMusicTriggered && bossPresent)
            {
                bossMusicTriggered = true;
                sceneSoundController.PlayTrack(enemyPresent, bossPresent);
            }
                
        }
    }
}
