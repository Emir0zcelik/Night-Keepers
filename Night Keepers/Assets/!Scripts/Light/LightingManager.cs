using NightKeepers;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : Singleton<LightingManager>
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    public float timeLength;

    private void Update()
    {
        if (Preset == null)
            return;

        if (TimeManager.Instance.isTimeStarted)
        {
            UpdateLighting(TimeManager.Instance.GetProgressionRatio());
        }
    }

    

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.fogColor.Evaluate(timePercent);
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.directionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 70f, 0f));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}