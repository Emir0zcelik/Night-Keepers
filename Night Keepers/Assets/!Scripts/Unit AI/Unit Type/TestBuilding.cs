
using System;
using NightKeepers;

public class TestBuilding : Unit
{
    public static event Action onBuildingDestroyed;
    public override void Die()
    {
        string buildingName = transform.parent.name;
        RM.Instance.DecreaseBuildingCount(buildingName);
        onBuildingDestroyed?.Invoke();
        SelectionManager.Instance.DeleteBuilding(GridManager.Instance._grid.WorldToGridPosition(transform.parent.transform.position));
        Destroy(transform.parent.gameObject);
        base.Die();
    }
}