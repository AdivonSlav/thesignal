using System;
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
        [SerializeField] private PostProcessing postProcessing;
        [SerializeField] private ScriptableRendererFeature ssao;
        [Header("UI")]
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Dropdown graphicsDropdown;
        [SerializeField] private Toggle bloomToggle;
        [SerializeField] private Toggle vignetteToggle;
        [SerializeField] private Toggle dofToggle;
        [SerializeField] private Toggle ssaoToggle;
        
        private Resolution[] resolutions;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
        }

        private void Start()
        {
            resolutions = Screen.resolutions;
            
            resolutionDropdown.ClearOptions();
            PopulateOptions();
            SetControls();
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

            graphicsDropdown.value = QualitySettings.GetQualityLevel();
        }
        
        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("musicVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("sfxVolume", volume);
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
        
        private void SetControls()
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            
            audioMixer.GetFloat("masterVolume", out var masterVolume);
            audioMixer.GetFloat("musicVolume", out var musicVolume);
            audioMixer.GetFloat("sfxVolume", out var sfxVolume);
            masterVolumeSlider.value = masterVolume;
            musicVolumeSlider.value = musicVolume;
            sfxVolumeSlider.value = sfxVolume;
            
            bloomToggle.isOn = PostProcessingSettings.bloom;
            vignetteToggle.isOn = PostProcessingSettings.vignette;
            dofToggle.isOn = PostProcessingSettings.dof;
            ssaoToggle.isOn = ssao.isActive;
        }
    }
}
