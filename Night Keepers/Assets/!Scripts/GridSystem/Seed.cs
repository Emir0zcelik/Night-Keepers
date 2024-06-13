using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class Seed : Singleton<Seed>
    {
        public string GameSeed = "Default";
        public int CurrentSeed = 0;

        
        protected override void Awake(){
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void ChangeSeed()
        {
            CurrentSeed = GameSeed.GetHashCode();
            Random.InitState(CurrentSeed);
        }
    }
}
