using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}