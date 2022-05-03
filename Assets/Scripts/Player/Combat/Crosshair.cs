using UnityEngine;

namespace TheSignal.Player.Combat
{
    public class Crosshair : MonoBehaviour
    {
        private Transform cameraTransform;
        private Ray ray;
        private RaycastHit hitInfo;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            ray.origin = cameraTransform.position;
            ray.direction = cameraTransform.forward;
            
            Physics.Raycast(ray, out hitInfo);
            
            transform.position = hitInfo.point;
        }
    }
}
