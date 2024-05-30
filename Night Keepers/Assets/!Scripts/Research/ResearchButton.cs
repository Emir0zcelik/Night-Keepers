using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour
{
    public ResearchUpgrades upgrade;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (ResearchPointManager.Instance.SpendResearchPoints(upgrade.cost))
        {
            upgrade.ApplyUpgrade();
            Debug.Log($"{upgrade.researchUpgrade} has been unlocked!");
        }
        else
        {
            Debug.Log("Not enough research points!");
        }
    }
}
