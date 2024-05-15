
using System;

public class TestBuilding : Unit
{
    public static event Action onBuildingDestroyed;
    public override void Die()
    {
        onBuildingDestroyed?.Invoke();
        Destroy(gameObject.transform.parent.gameObject);
        base.Die();
    }
}