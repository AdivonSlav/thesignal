using System;
using System.Collections;
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

        private void Update()
        {
            transform.position = player.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyPresent = true;
                sceneSoundController.PlayTrack(enemyPresent);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyPresent = false;
                StartCoroutine(Disengage());
            }
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
    }
}
