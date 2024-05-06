using UnityEngine;

namespace NightKeepers
{
    public class BarracksUI : MonoBehaviour
    {
        [SerializeField] private Barracks _selectedBarrack;
        [SerializeField] private RectTransform queueHolder;
        [SerializeField] private GameObject UIHolder;

        private void Start()
        {
            UIHolder.SetActive(false);
        }

        private void OnEnable()
        {
            BarracksButton.onButtonPressed += OnButtonPressed;
            Barracks.onListUpdated += OnListUpdated;
            SelectionManager.onBuildingSelected += OnBuildingSelected;
        }

        private void OnDisable()
        {
            BarracksButton.onButtonPressed -= OnButtonPressed;
            Barracks.onListUpdated -= OnListUpdated;
            SelectionManager.onBuildingSelected -= OnBuildingSelected;
        }

        private void OnBuildingSelected(FunctionalBuilding barracks)
        {
            if (barracks.GetType() == typeof(Barracks))
            {
                _selectedBarrack = (Barracks)barracks;
                UIHolder.SetActive(true);
                OnListUpdated();
            }
            else{
                _selectedBarrack = null;
                UIHolder.SetActive(false);
            }
        }

        private void OnButtonPressed(Unit unitToProduce)
        {
            ProduceUnit(unitToProduce);
        }

        // should update the UI when a different barrack is selected not on validate this causes an error right now at the start because it runs too early
        private void OnValidate()
        {
            //Debug.Log("Barrack has been changed in the inspector! Or something else.");
            //OnListUpdated();
        }

        private void OnListUpdated()
        {
            UpdateImages();
        }

        private void UpdateImages()
        {
            // temporary. Normally units needs their image to put insted of colors. Needs pooling.

            foreach(RectTransform image in queueHolder)
            {
                Destroy(image.gameObject);
            }

            foreach(Unit unit in _selectedBarrack.GetProductionList())
            {
                Instantiate(unit.UnitData.UnitImagePrefab, queueHolder);
            }
        }

        private void ProduceUnit(Unit unitToProduce)
        {
            if (_selectedBarrack.GetCurrentNumberOfProductions() < 5)
            {
                _selectedBarrack.InsertUnitToList(unitToProduce);
                UpdateImages();
            }
        }
    }
}