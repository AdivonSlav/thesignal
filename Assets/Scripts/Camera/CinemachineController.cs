using Cinemachine;
using UnityEngine;

namespace TheSignal.Camera
{
    public class CinemachineController : MonoBehaviour
    {
        private CinemachineBrain brain;

        [SerializeField] private CinemachineVirtualCamera freelookCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;

        private void Start()
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
            
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
        }

        public bool Paused()
        {
            return !brain.enabled;
        }
    }
}
