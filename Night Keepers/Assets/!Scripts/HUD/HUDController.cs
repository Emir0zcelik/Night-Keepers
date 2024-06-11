using System.Collections.Generic;
using NightKeepers.Camera;
using UnityEngine;

namespace NightKeepers
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject buildingUI;
        [SerializeField] private CameraMovement cameraMovement;
        
        public void DeleteBuildingButton()
        {
            BuildingManager.Instance.isDeleteMode = true;
        }

        public void OpenBuildingUI()
        {
            buildingUI.SetActive(true);
        }

        public void CloseBuildingUI()
        {
            buildingUI.SetActive(false);
        }

        public void TownHallFocus()
        {
            List<GameObject> playerBaseList = PlayerBaseManager.Instance.GetPlayerBaseList();
            if (playerBaseList.Count <= 0)  return;
            Vector3 townHallPosition = playerBaseList[0].transform.position;
            cameraMovement.FocusTownHall(townHallPosition);
        }
    }
}