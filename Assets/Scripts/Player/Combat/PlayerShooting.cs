using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

using TheSignal.Player.Input;
using TheSignal.Animation;
using UnityEngine.Animations.Rigging;

namespace TheSignal.Player.Combat
{
    public class PlayerShooting : MonoBehaviour
    {
        private AnimatorManager animatorManager;
        private InputManager inputManager;
        private Camera mainCamera;

        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private Image crosshair;
        [SerializeField] private LayerMask aimLayer;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private MultiAimConstraint aimIK;
        [SerializeField] private MultiAimConstraint bodyIK;
        [SerializeField] private TwoBoneIKConstraint leftHandAimIK;
        [SerializeField] private TwoBoneIKConstraint leftHandIdleIK;
        
        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            mainCamera = Camera.main;
        }

        public void HandleShooting()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 999.0f, aimLayer))
            {
                aimTarget.position = hit.point;
            }
        }

        public void HandleAiming()
        {
            if (inputManager.isAiming)
            {
                aimCamera.gameObject.SetActive(true);
                crosshair.gameObject.SetActive(true);

                SetConstraintWeights(1.0f, 0.4f, 1.0f, 0.0f);
                RotateWithLook();
            }
            else
            {
                aimCamera.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);
                
                SetConstraintWeights(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }

        private void SetConstraintWeights(float aim, float body, float leftHandAim, float leftHandIdle)
        {
            bodyIK.weight = Mathf.Lerp(bodyIK.weight, body, 20.0f * Time.deltaTime);
            aimIK.weight = Mathf.Lerp(aimIK.weight, aim, 20.0f * Time.deltaTime);
            leftHandAimIK.weight = Mathf.Lerp(leftHandAimIK.weight, leftHandAim, 20.0f * Time.deltaTime);
            leftHandIdleIK.weight = Mathf.Lerp(leftHandIdleIK.weight, leftHandIdle, 20.0f * Time.deltaTime);
        }

        private void RotateWithLook()
        {
            Vector3 worldAimTarget = aimTarget.position;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            targetRotation = Quaternion.Lerp(transform.rotation, targetRotation, 15.0f * Time.fixedDeltaTime);

            transform.rotation = targetRotation;
        }
    }
}