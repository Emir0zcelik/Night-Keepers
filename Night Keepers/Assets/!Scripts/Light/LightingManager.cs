using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    [ExecuteAlways]
    public class LightingManager : Singleton<LightingManager>
    {
        [SerializeField] private Light directionalLight;
        [SerializeField] private LightingPreset preset;
        private float timeOfDay;

        private void Update() {
            if (preset == null)
                return;
            if (Application.isPlaying)
            {
                timeOfDay += Time.deltaTime;
                timeOfDay %= 24;
                UpdateLighting(TimeManager.Instance.GetProgressionRatio());
            }
            else
            {
                UpdateLighting(TimeManager.Instance.GetProgressionRatio());
            }
        }

        private void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);
            
            if (directionalLight != null)
            {
                directionalLight.color = preset.directionalColor.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 70f, 0f));
            }
        }

        private void OnValidate() {
            if (directionalLight != null)
                return;
            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                        return;
                    }
                }
            }
        }    
    }
}
