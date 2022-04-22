using System;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using TheSignal.Player.Input;
using UnityEditor;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace TheSignal.Player.Combat
{
    public class PlayerAiming : MonoBehaviour
    {
        private InputManager inputManager;

        [SerializeField] private CinemachineVirtualCamera normalCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private Image crosshair;
        [SerializeField] private Transform aimTarget;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }

        private void Start()
        {
            normalCamera.ForceCameraPosition(transform.position - Vector3.back, transform.rotation);
            aimCamera.ForceCameraPosition(transform.position - Vector3.back, transform.rotation);
        }

        public void HandleAiming()
        {
            if (inputManager.isAiming)
            {
                aimCamera.gameObject.SetActive(true);
                crosshair.gameObject.SetActive(true);
                
                RotateWithLook();
            }
            else
            {
                aimCamera.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);
            }
        }

        private void RotateWithLook()
        {
            Vector3 worldAimTarget = aimTarget.position;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 15.0f * Time.fixedDeltaTime);

            transform.rotation = targetRotation;
        }
    }
}
