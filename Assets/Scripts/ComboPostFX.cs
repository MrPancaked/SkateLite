using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ComboPostFX : MonoBehaviour
{
    public Volume globalVolume; 
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    [Range(0f, 1f)] public float comboIntensity = 0f; 

    void Start()
    {
        if (globalVolume.profile.TryGet(out ChromaticAberration ca))
            chromaticAberration = ca;

        if (globalVolume.profile.TryGet(out LensDistortion ld))
            lensDistortion = ld;
        
    }

    void Update()
    {
        if (chromaticAberration != null)
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 0.5f, comboIntensity);

        if (lensDistortion != null)
            lensDistortion.intensity.value = Mathf.Lerp(0f, -0.3f, comboIntensity);
    }

    public void SetComboLevel(float normalizedCombo)
    {
        comboIntensity = Mathf.Clamp01(normalizedCombo);
    }
}
