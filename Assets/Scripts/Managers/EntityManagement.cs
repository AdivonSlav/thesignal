using System.Collections.Generic;
using TheSignal.Player.Input;
using UnityEngine;

namespace TheSignal.Managers
{
    public class EntityManagement : MonoBehaviour
    { 
        public List<TrackedEntity> trackedEntities;

        private InputManager inputManager;
        
        private void Awake()
        {
            inputManager = trackedEntities[0].GetComponent<InputManager>();
        }

        public void Update()
        {
            Debug.Log(inputManager.isExiting);
            
            if (!inputManager.isExiting)
                return;

            
            foreach (var t in trackedEntities)
            {
                t.isRunning = !t.isRunning;

                inputManager.isExiting = false;
            }
        }
    }
}
