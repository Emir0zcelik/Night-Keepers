using UnityEngine;

namespace NightKeepers
{
    public class EnemyUnitUndeadSpearThrower : EnemyUnit
    {        
        [SerializeField] private GameObject _spearPrefab;
        [SerializeField] private Transform _rightHand;

        public override void DoAttack()
        {
            base.DoAttack();
            var spear = Instantiate(_spearPrefab, _rightHand.position, Quaternion.identity);
            spear.GetComponent<ArcProjectile>().Thrown(_rightHand.position, GetCurrentTargetPosition());
        }
    }
}