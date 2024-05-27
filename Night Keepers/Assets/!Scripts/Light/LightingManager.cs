using NightKeepers;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : Singleton<LightingManager>
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    private float rotationAngle;

    private void FixedUpdate()
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
        if (TimeManager.Instance.IsDay)
        {
            RenderSettings.ambientLight = Preset.ambientColor.Evaluate(timePercent);
            RenderSettings.fogColor = Preset.fogColor.Evaluate(timePercent);
            DirectionalLight.color = Preset.directionalColor.Evaluate(timePercent);

            rotationAngle = Mathf.Lerp(0f, 180f, timePercent);
        }
        else
        {
            rotationAngle = Mathf.Lerp(180f, 360f, timePercent);
        }

        DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3(rotationAngle, 150f, 0f));
    }
}