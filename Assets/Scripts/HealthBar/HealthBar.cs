using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TheSignal.Scripts.HealthBarScript
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public void SetMaxHealth()
        {
            slider.maxValue = 100;
            slider.value = 100;
        }
        public void SetHealth(float Health)
        {
            slider.value = Health;
        }
        
    }
}
