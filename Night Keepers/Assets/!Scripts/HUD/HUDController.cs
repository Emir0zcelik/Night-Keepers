using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class HUDController : MonoBehaviour
    {
        public void DeleteBuildingButton()
        {
            BuildingManager.Instance.isDeleteMode = true;
        }
    }
}
