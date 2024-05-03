using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NightKeepers
{
    public class BarracksUI : MonoBehaviour
    {
        [SerializeField] private Barracks _selectedBarrack;
        [SerializeField] private List<Image> _queueImageList = new List<Image>();
        [SerializeField] private RectTransform queueHolder;

        private void Start()
        {
            foreach (RectTransform image in queueHolder)
            {
                _queueImageList.Add(image.GetComponent<Image>());
                image.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            BarracksButton.onButtonPressed += OnButtonPressed;
            Barracks.onListUpdated += OnListUpdated;
        }

        private void OnDisable()
        {
            BarracksButton.onButtonPressed -= OnButtonPressed;
            Barracks.onListUpdated -= OnListUpdated;
        }

        private void OnButtonPressed(Unit unitToProduce)
        {
            ProduceUnit(unitToProduce);
        }

        // should update the UI when a different barrack is selected not on validate this causes an error right now at the start because it runs too early
        private void OnValidate()
        {
            //Debug.Log("Barrack has been changed in the inspector! Or something else.");
            //OnQueueUpdated();
        }

        private void OnListUpdated()
        {
            foreach (Image image in _queueImageList)
            {
                image.gameObject.SetActive(false);
            }

            for (int i = 0; i < _selectedBarrack.GetCurrentListCount(); i++)
            {
                _queueImageList[i].gameObject.SetActive(true);
            }
        }

        private void UpdateImages()
        {
            // temporary. Normally units needs their image to put insted of colors
            if (_selectedBarrack.GetLastElementOfList().UnitData.UnitName == "Green")
            {
                _queueImageList[_selectedBarrack.GetCurrentListCount() - 1].color = Color.green;
            }
            else if (_selectedBarrack.GetLastElementOfList().UnitData.UnitName == "Purple")
            {
                _queueImageList[_selectedBarrack.GetCurrentListCount() - 1].color = Color.magenta;
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