using System.Collections;
using TheSignal.Player.Combat;
using TheSignal.Player.Input;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TheSignal.Animation.Rigging
{
    [RequireComponent(typeof(InputManager))]
    public class RigManager : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerAiming playerAiming;

        [Header("Rigs")]
        [SerializeField] private Rig aimingRig;
        [SerializeField] private Rig runningRig;
        [SerializeField] private Rig disallowedAimingRig;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerAiming = GetComponent<PlayerAiming>();
        }

        private void Update()
        {
            StartCoroutine(CheckAll());
        }

        private IEnumerator SetRig(Rig rig, float weight, float timeDelta)
        {
            float elapsedTime = 0.0f;
            float waitTime = 0.25f;
            
            while (elapsedTime < waitTime)
            {
                rig.weight = Mathf.Lerp(rig.weight, weight, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator CheckAim()
        {
            switch (inputManager.isAiming)
            {
                case true when aimingRig.weight < 1.0f:
                    yield return StartCoroutine(SetRig(aimingRig, 1.0f, Time.deltaTime));
                    playerAiming.enteredAim = true;
                    break;
                case false when aimingRig.weight > 0.0f:
                    yield return StartCoroutine(SetRig(aimingRig, 0.0f, Time.deltaTime));
                    playerAiming.enteredAim = false;
                    break;
            }
        }

        private IEnumerator CheckDisallowedAim()
        {
            switch (playerAiming.allowedFire)
            {
                case false when disallowedAimingRig.weight < 1.0f:
                    yield return StartCoroutine(SetRig(disallowedAimingRig, 1.0f, Time.deltaTime));
                    break;
                case true when disallowedAimingRig.weight > 0.0f:
                    yield return StartCoroutine(SetRig(disallowedAimingRig, 0.0f, Time.deltaTime));
                    break;
            }
        }

        private IEnumerator CheckRunning()
        {
            switch (inputManager.isRunning)
            {
                case true when runningRig.weight < 1.0f && inputManager.moveAmount != 0:
                    yield return StartCoroutine(SetRig(runningRig, 1.0f, Time.deltaTime));
                    break;
                case false when runningRig.weight > 0.0f:
                    yield return StartCoroutine(SetRig(runningRig, 0.0f, Time.deltaTime));
                    break;
            }
        }

        private IEnumerator CheckAll()
        {
            yield return StartCoroutine(CheckDisallowedAim());
            yield return StartCoroutine(CheckRunning());
            yield return StartCoroutine(CheckAim());
        }
    }
}
