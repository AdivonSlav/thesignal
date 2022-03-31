using System;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using TheSignal.Player.Input;
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
        [SerializeField] private LayerMask aimLayer;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private MultiAimConstraint aimIK;
        [SerializeField] private MultiAimConstraint bodyIK;
        [SerializeField] private TwoBoneIKConstraint rightHandIdleIK;
        [SerializeField] private TwoBoneIKConstraint rightHandAimIK;
        [SerializeField] private TwoBoneIKConstraint leftHandIdleIK;
        [SerializeField] private TwoBoneIKConstraint leftHandAimIK;

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
                aimTarget.position = GetHit().point;
                
                aimCamera.gameObject.SetActive(true);
                crosshair.gameObject.SetActive(true);

                SetBodyConstraintWeights(1.0f, 0.4f);
                SetHandConstraintWeights(0.0f, 1.0f, 0.0f,1.0f);
                RotateWithLook();
            }
            else
            {
                aimCamera.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);
                
                SetBodyConstraintWeights(0.0f, 0.0f);
                SetHandConstraintWeights(1.0f, 0.0f, 1.0f,0.0f);
            }
        }

        private void SetBodyConstraintWeights(float aim, float body)
        {
            bodyIK.weight = Mathf.Lerp(bodyIK.weight, body, 20.0f * Time.deltaTime);
            aimIK.weight = Mathf.Lerp(aimIK.weight, aim, 20.0f * Time.deltaTime);
        }

        private void SetHandConstraintWeights(float rightHandIdle, float rightHandAim, float leftHandIdle, float leftHandAim)
        {
            rightHandIdleIK.weight = Mathf.Lerp(rightHandIdleIK.weight, rightHandIdle, 20.0f * Time.deltaTime);
            rightHandAimIK.weight = Mathf.Lerp(rightHandAimIK.weight, rightHandAim, 20.0f * Time.deltaTime);
            leftHandIdleIK.weight = Mathf.Lerp(leftHandIdleIK.weight, leftHandIdle, 20.0f * Time.deltaTime);
            leftHandAimIK.weight = Mathf.Lerp(leftHandAimIK.weight, leftHandAim, 20.0f * Time.deltaTime);
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

        private RaycastHit GetHit()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
            RaycastHit hit;

            Physics.Raycast(ray, out hit, 999.0f, aimLayer);
            
            return hit;
        }
    }
}
