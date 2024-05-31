using System.Collections;
using UnityEngine;
using TMPro;

public class ResearchPointManager : MonoBehaviour
{
    public static ResearchPointManager Instance;
    public int researchPoints = 0;
    public TMP_Text researchPointsText;
    public bool isStoneMineResearched = false;
    public bool isWallResearched = false;
    public bool isFishingResearched = false;
    public bool isHouseResearched = false;
    public bool isTrapResearched = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGeneratingResearchPoints()
    {
        StartCoroutine(GenerateResearchPoints());
    }

    private IEnumerator GenerateResearchPoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int pointsToAdd = 2;
            researchPoints += pointsToAdd;
            UpdateResearchPointsText();
        }
    }

    private void UpdateResearchPointsText()
    {
        if (researchPointsText != null)
        {
            researchPointsText.text = $" {researchPoints}";
        }
    }

    public bool SpendResearchPoints(int amount)
    {
        if (researchPoints >= amount)
        {
            researchPoints -= amount;
            UpdateResearchPointsText();
            return true;
        }
        return false;
    }
}
