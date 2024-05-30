using NightKeepers;
using System;

public class TestBuilding : Unit
{
    public static event Action onBuildingDestroyed;
    public override void Die()
    {
        onBuildingDestroyed?.Invoke();
        string buildingName = transform.name;
        RM.Instance.DecreaseBuildingCount(buildingName);
        BuildingManager.Instance.DeleteBuilding(GridManager.Instance._grid.WorldToGridPosition(transform.parent.transform.position));
        Destroy(transform.parent.gameObject);
        base.Die();
    }
}
