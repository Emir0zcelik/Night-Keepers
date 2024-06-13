using UnityEngine;

namespace NightKeepers
{
    public class TutorialManager : Singleton<TutorialManager>
    {
        [SerializeField] private GameObject cursor;
        [SerializeField] private Transform backButton;
        [SerializeField] private Transform buildMenu;
        [SerializeField] private Transform generalBuilding;
        [SerializeField] private Transform resourceBuilding;
        [SerializeField] private Transform militaryBuilding;
        [SerializeField] private Transform townHall;
        [SerializeField] private Transform lumberjack;
        [SerializeField] private Transform farm;
        [SerializeField] private Transform barrack;

        public bool isBuildingMainMenu = false;
        public bool isGeneralBuilding = false;
        public bool isResourceBuilding = false;
        public bool isMilitaryBuilding = false;
        public bool isTownHall = false;
        public bool isBackButton = false;
        public bool isCloseButton = false;

        void Update()
        {
            AllCursorTransforms();
        }

        private void AllCursorTransforms()
        {
            if (!BuildingManager.Instance.isTownHallPlaced)
            {
                TownHallCursorTransforms();
            }
            else
            {
                if (!BuildingManager.Instance.isLumberjackPlaced)
                {
                    LumberjackCursorTransforms();
                }
                else
                {
                    if (!BuildingManager.Instance.isFarmPlaced)
                    {
                        FarmCursorTransforms();
                    }
                    else
                    {
                        if (!BuildingManager.Instance.isBarrackPlaced)
                        {
                            BarrackCursorTransforms();
                        }
                        else
                        {
                            cursor.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        private void TownHallCursorTransforms()
        {
            if (!isBuildingMainMenu)
            {
                cursor.transform.position = new Vector3(buildMenu.transform.position.x + 50f, buildMenu.transform.position.y - 50f, buildMenu.transform.position.z);
            }
            else
            {
                if (!isGeneralBuilding)
                {
                    cursor.transform.position = new Vector3(generalBuilding.transform.position.x + 300f, generalBuilding.transform.position.y - 75f, generalBuilding.transform.position.z);
                }
                else
                {
                    cursor.transform.position = new Vector3(townHall.transform.position.x + 150f, townHall.transform.position.y - 50f, townHall.transform.position.z);
                }
            }
        }

        private void LumberjackCursorTransforms()
        {
            if (!isBackButton)
            {
                cursor.transform.position = new Vector3(backButton.transform.position.x, backButton.transform.position.y -55f, backButton.transform.position.z);
            }
            else
            {
                if (!isBuildingMainMenu)
                {
                    cursor.transform.position = new Vector3(buildMenu.transform.position.x + 50f, buildMenu.transform.position.y - 50f, buildMenu.transform.position.z);
                }
                else
                {
                    if (!isResourceBuilding)
                    {
                        cursor.transform.position = new Vector3(resourceBuilding.transform.position.x + 300f, resourceBuilding.transform.position.y - 75f, resourceBuilding.transform.position.z);
                    }
                    else
                    {
                        cursor.transform.position = new Vector3(lumberjack.transform.position.x + 150f, lumberjack.transform.position.y - 50f, lumberjack.transform.position.z);
                    }
                }
            }    
        }

        private void FarmCursorTransforms()
        {
            if (!isBuildingMainMenu)
            {
                cursor.transform.position = new Vector3(buildMenu.transform.position.x + 50f, buildMenu.transform.position.y - 50f, buildMenu.transform.position.z);
            }
            else
            {
                if (!isResourceBuilding)
                {
                    cursor.transform.position = new Vector3(resourceBuilding.transform.position.x + 300f, resourceBuilding.transform.position.y - 75f, resourceBuilding.transform.position.z);
                }
                else
                {
                    cursor.transform.position = new Vector3(farm.transform.position.x + 150f, farm.transform.position.y - 50f, farm.transform.position.z);
                    isBackButton = false;
                }
            }
           
        }

        private void BarrackCursorTransforms()
        {
            if (!isBackButton)
            {
                cursor.transform.position = new Vector3(backButton.transform.position.x, backButton.transform.position.y -55f, backButton.transform.position.z);
            }
            else
            {
                if (!isBuildingMainMenu)
                {
                    cursor.transform.position = new Vector3(buildMenu.transform.position.x + 50f, buildMenu.transform.position.y - 50f, buildMenu.transform.position.z);
                }
                else
                {
                    if (!isMilitaryBuilding)
                    {
                        cursor.transform.position = new Vector3(militaryBuilding.transform.position.x + 300f, militaryBuilding.transform.position.y - 75f, militaryBuilding.transform.position.z);
                    }
                    else
                    {
                        cursor.transform.position = new Vector3(barrack.transform.position.x + 150f, barrack.transform.position.y - 50f, barrack.transform.position.z);
                    }
                }
            }   
        }

        

        private void SetCursorTransform(Vector3 position)
        {
            cursor.transform.position = position;
        }
    }
}
