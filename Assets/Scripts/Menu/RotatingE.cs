using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheSignal.Menu
{
    public class RotatingE : MonoBehaviour
    {
        [SerializeField] private Transform target;
        void Update()
        {
            RotateToTarget();
        }
        private void RotateToTarget()
        {
            transform.LookAt(transform.position+Camera.main.transform.rotation*Vector3.forward,Camera.main.transform.rotation*Vector3.up);
        }
    }
}
