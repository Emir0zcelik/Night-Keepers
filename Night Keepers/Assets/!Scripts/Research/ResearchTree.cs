using UnityEngine;
using UnityEngine.UI;

public class ResearchTree : MonoBehaviour
{
    [SerializeField] private GameObject researchTreeUI;

    public void OpenResearchTree()
    {
        if (researchTreeUI != null)
        {
            researchTreeUI.SetActive(true);
        }
    }

    public void CloseResearchTree()
    {
        if (researchTreeUI != null)
        {
            researchTreeUI.SetActive(false);
        }
    }
}
