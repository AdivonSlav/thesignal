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
        private Camera mainCamera;

        [SerializeField] private CinemachineVirtualCamera normalCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private Image crosshair;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Rig aimRigLayer;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            mainCamera = Camera.main;
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
                SetAimRig(1.0f);
            }
            else
            {
                aimCamera.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);
                SetAimRig(0.0f);
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

        private void SetAimRig(float weight)
        {
            aimRigLayer.weight = Mathf.Lerp(aimRigLayer.weight, weight, 15.0f);
        }

        // private RaycastHit GetHit()
        // {
        //     Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        //     Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
        //     RaycastHit hit;
        //
        //     Physics.Raycast(ray, out hit, 999.0f, aimLayer);
        //     
        //     return hit;
        // }
    }
}
