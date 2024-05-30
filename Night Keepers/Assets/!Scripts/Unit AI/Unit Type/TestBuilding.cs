using NightKeepers;
using System;

public class TestBuilding : Unit
{
    public static event Action onBuildingDestroyed;
    public override void Die()
    {
        onBuildingDestroyed?.Invoke();
        BuildingManager.Instance.DeleteBuilding(GridManager.Instance._grid.WorldToGridPosition(transform.parent.transform.position));
        RM.Instance.DecreaseBuildingCount(GetComponent<Building>().buildingData.name); 
        Destroy(transform.parent.gameObject);
        base.Die();
    }
}
