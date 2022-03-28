using UnityEngine;

namespace TheSignal.Weapons
{
    public class RaycastWeapon : MonoBehaviour
    {
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField] private Transform raycastOrigin;
        [SerializeField] private Transform raycastEnd;

        private Ray ray;
        private RaycastHit hit;
        
        public void StartFiring()
        {
            muzzleFlash.Emit(1);

            ray.origin = raycastOrigin.position;
            ray.direction = raycastEnd.position - raycastOrigin.position;

            if (Physics.Raycast(ray, out hit))
            {
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = hit.normal;
                hitEffect.Emit(1);
            }
        }

        public void StopFiring()
        {
            
        }
    }
}
