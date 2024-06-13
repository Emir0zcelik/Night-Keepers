using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class Seed : Singleton<Seed>
    {
        public string GameSeed = "Default";
        public int CurrentSeed = 0;

        private void Awake() {
            CurrentSeed = GameSeed.GetHashCode();
            Random.InitState(CurrentSeed);
            
        }
    }
}
