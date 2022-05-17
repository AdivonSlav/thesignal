using System.Collections.Generic;
using TheSignal.Camera;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace TheSignal.Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private PostProcessing postProcessing;
        [SerializeField] private ScriptableRendererFeature ssao;
        
        private Resolution[] resolutions;

        private void Start()
        {
            resolutions = Screen.resolutions;
            
            resolutionDropdown.ClearOptions();
            PopulateOptions();
        }

        private void PopulateOptions()
        {
            var resOptions = new List<string>();

            var currentRes = 0;
            
            for (var i = 0; i < resolutions.Length; i++)
            {
                var resOption = $"{resolutions[i].width} x {resolutions[i].height}";
                resOptions.Add(resOption);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                    currentRes = i;
            }
            
            resolutionDropdown.AddOptions(resOptions);
            resolutionDropdown.value = currentRes;
            resolutionDropdown.RefreshShownValue();
        }
        
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }

        public void SetQualityPreset(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resIndex)
        {
            var res = resolutions[resIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }

        public void SetBloom(bool bloom)
        {
            postProcessing.SetBloom(bloom);
        }

        public void SetVignette(bool vignette)
        {
            postProcessing.SetVignette(vignette);
        }

        public void SetDOF(bool dof)
        {
            postProcessing.SetDOF(dof);
        }

        public void SetSSAO(bool ssaoToggle)
        {
            ssao.SetActive(ssaoToggle);
        }
    }
}
