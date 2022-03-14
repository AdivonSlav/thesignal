using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AimStates
{
    public class AimStateManager : MonoBehaviour
    {
        private PlayerInput playerInput;
        [HideInInspector] public InputAction aimAction;

        [HideInInspector] public Animator animator;

        public CinemachineVirtualCamera aimCamera;
        public int cameraPriorityIncr;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            aimAction = playerInput.actions["Aim"];
        }

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            aimAction.performed += _ => EnterADS();
            aimAction.canceled += _ => ExitADS();
        }

        private void OnDisable()
        {
            aimAction.performed -= _ => EnterADS();
            aimAction.canceled -= _ => ExitADS();
        }

        private void EnterADS()
        {
            animator.SetBool("Aiming", true);
            aimCamera.Priority += cameraPriorityIncr;
        }

        private void ExitADS()
        {
            animator.SetBool("Aiming", false);
            aimCamera.Priority -= cameraPriorityIncr;
        }
    }
    
}
