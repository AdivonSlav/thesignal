using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace TheSignal.Camera
{
    public static class PostProcessingSettings
    {
        public static bool bloom = true;
        public static bool vignette = true;
        public static bool dof = true;
    }
    
    public class PostProcessing : MonoBehaviour
    {
        private Volume volume;

        private Bloom bloom;
        private Vignette vignette;
        private DepthOfField dof;

        private void Awake()
        {
            volume = GetComponent<Volume>();
        }

        private void Start()
        {
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out dof);
            
            bloom.active = PostProcessingSettings.bloom;
            vignette.active = PostProcessingSettings.vignette;
            dof.active = PostProcessingSettings.dof;
        }

        public void SetBloom(bool value)
        {
            bloom.active = value;
            PostProcessingSettings.bloom = value;
        }

        public void SetVignette(bool value)
        {
            vignette.active = value;
            PostProcessingSettings.vignette = value;
        }

        public void SetDOF(bool value)
        {
            dof.active = value;
            PostProcessingSettings.dof = value;
        }
    }
}
