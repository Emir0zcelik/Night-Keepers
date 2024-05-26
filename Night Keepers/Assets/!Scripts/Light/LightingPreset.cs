using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Lighting Preset")]
    public class LightingPreset : ScriptableObject
    {
        public Gradient ambientColor;
        public Gradient directionalColor;
        public Gradient fogColor;
        
    }
}
