using System;
using Cinemachine;
using UnityEngine;

namespace TheSignal.Camera
{
    public class CinemachineController : MonoBehaviour
    {
        private CinemachineBrain brain;

        private void Start()
        {
            brain = GetComponent<CinemachineBrain>();
        }

        public void TogglePause(bool isAiming)
        {
            brain.enabled = !brain.enabled;

            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public bool Paused()
        {
            return !brain.enabled;
        }
    }
}
