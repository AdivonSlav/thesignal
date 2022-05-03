using System;
using Cinemachine;
using UnityEngine;

using TheSignal.Player.Input;
using TheSignal.Scenes.Behaviours;
using TheSignal.SFX;
using UnityEditor;

namespace TheSignal.Player.Combat
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private Canvas crosshairCanvas;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Transform distanceCheckOrigin;
        
        [Header("Cinemachine cameras")]
        [SerializeField] private CinemachineVirtualCamera normalCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;

        [HideInInspector] public bool allowedFire;
        
        private InputManager inputManager;
        private Rigidbody playerRB;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerRB = GetComponent<Rigidbody>();
            
            allowedFire = true;
        }

        private void Start()
        {
            normalCamera.ForceCameraPosition(transform.position - Vector3.back, transform.rotation);
            aimCamera.ForceCameraPosition(transform.position - Vector3.back, transform.rotation);
        }

        public void HandleAiming()
        {
            aimCamera.enabled = inputManager.isAiming;
            crosshairCanvas.enabled = inputManager.isAiming;
            
            if (inputManager.isAiming)
            {
                CheckDistance();
                RotateWithLook();
            }
        }

        private void RotateWithLook()
        {
            Vector3 worldAimTarget = aimTarget.position;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, 15.0f * Time.fixedDeltaTime);

            playerRB.MoveRotation(targetRotation);
        }

        private void CheckDistance()
        {
            var ray = new Ray
            {
                origin = distanceCheckOrigin.position,
                direction = distanceCheckOrigin.forward
            };
            
            var hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 1.5f))
            {
                allowedFire = false;
            }
            else
                allowedFire = true;
        }
    }
}
