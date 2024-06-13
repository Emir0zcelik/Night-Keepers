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
            buildingUI.GetComponent<BuildingUI>().MainMenu();
            buildingUI.SetActive(true);
            TutorialManager.Instance.isBuildingMainMenu = true;
            TutorialManager.Instance.isCloseButton = true;
        }

        public void CloseBuildingUI()
        {
            buildingUI.SetActive(false);
            TutorialManager.Instance.isCloseButton = false;
            TutorialManager.Instance.isBuildingMainMenu = false;
            TutorialManager.Instance.isBackButton = true;
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