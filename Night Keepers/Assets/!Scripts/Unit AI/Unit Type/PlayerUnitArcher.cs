using NightKeepers;
using UnityEngine;

public class PlayerUnitArcher : PlayerUnit
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _bow;

    public override void DoAttack()
    {
        base.DoAttack();
        var spear = Instantiate(_arrowPrefab, _bow.position, Quaternion.identity);
        spear.GetComponent<ArcProjectile>().Thrown(_bow.position, GetCurrentTargetPosition());
    }
}