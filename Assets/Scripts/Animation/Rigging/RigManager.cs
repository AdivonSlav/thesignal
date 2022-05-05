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
            if (inputManager.isAiming && aimingRig.weight < 1.0f)
                StartCoroutine(SetRig(aimingRig, 1.0f, Time.deltaTime));
            else if (!inputManager.isAiming && aimingRig.weight > 0.0f)
                StartCoroutine(SetRig(aimingRig, 0.0f, Time.deltaTime));

            if (!playerAiming.allowedFire && disallowedAimingRig.weight < 1.0f)
                StartCoroutine(SetRig(disallowedAimingRig, 1.0f, Time.deltaTime));
            else if (playerAiming.allowedFire && disallowedAimingRig.weight > 0.0f)
                StartCoroutine(SetRig(disallowedAimingRig, 0.0f, Time.deltaTime));

            if (inputManager.isRunning && runningRig.weight < 1.0f && inputManager.moveAmount!=0)
                StartCoroutine(SetRig(runningRig, 1.0f, Time.deltaTime));
            else if (!inputManager.isRunning && runningRig.weight > 0.0f)
                StartCoroutine(SetRig(runningRig, 0.0f, Time.deltaTime));
        }

        IEnumerator SetRig(Rig rig, float weight, float timeDelta)
        {
            float waitTime = 0.5f;
            
            for (float elapsed = 0.0f; elapsed < waitTime; elapsed += timeDelta)
            {
                rig.weight = Mathf.Lerp(rig.weight, weight, elapsed / waitTime);
            }

            yield return null;
        }
    }
}
