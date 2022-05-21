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
        private List<Collider> tempOthers;
        private CinemachineController cinemachineController;

        private void Awake()
        {
            tempOthers = new List<Collider>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
        }
        private void Update()
        {
            transform.position = player.transform.position;
            CheckEnemies();
            
            if (enemyPresent && tempOthers.Count == 0)
            {
                enemyPresent = false;
                StartCoroutine(Disengage());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (cinemachineController.Paused())
                return;
            
            if (other.CompareTag("Enemy"))
            {
                enemyPresent = true;
                
                if (tempOthers.Count == 0)
                    sceneSoundController.PlayTrack(enemyPresent);
                
                tempOthers.Add(other);
            }
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
            sceneSoundController.PlayTrack(enemyPresent);
        }

        private void CheckEnemies()
        {
            for (var i = 0; i < tempOthers.Count; i++)
            {
                if (!tempOthers[i])
                    tempOthers.Remove(tempOthers[i]);
            }
        }
    }
}
