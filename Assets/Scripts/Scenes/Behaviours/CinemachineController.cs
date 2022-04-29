using Cinemachine;
using UnityEngine;

namespace TheSignal.Scenes.Behaviours
{
    public class CinemachineController : MonoBehaviour
    {
        private CinemachineBrain brain;

        [SerializeField] private CinemachineVirtualCamera freelookCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
        }

        public void TogglePause(bool isAiming)
        {
            brain.enabled = !brain.enabled;

            if (isAiming)
                aimCamera.enabled = !aimCamera.enabled;
            else
                freelookCamera.enabled = !freelookCamera.enabled;
        }
    }
}
