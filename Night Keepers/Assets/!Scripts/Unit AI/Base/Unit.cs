using NightKeepers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitScriptableObject;

public class Unit : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    [field: SerializeField] public string currentStateName { get; set; } = "Idle";

    [field: SerializeField] public int CurrentHealth { get; set; }

    public UnitStateMachine StateMachine { get; set; }
    public UnitIdleState IdleState { get; set; }
    public UnitChaseState ChaseState { get; set; }
    public UnitAttackState AttackState { get; set; }

    [field: SerializeField] public bool isAggroed { get; set; }
    [field: SerializeField] public bool isInAttackingDistance { get; set; }

    private TargetPreference _favouriteTarget;

    [field: SerializeField] private GameObject Target;
    private Unit CurrentTargetUnit;
    private NavMeshAgent navAgent;

    NavMeshPath tempPath;

    [field: SerializeField] public UnitScriptableObject UnitData { get; set; }

    [field: SerializeField] public Animation Animation { get; set; }
    public List<string> AnimationNames = new();

    [field: SerializeField] private FloatingHealthBar _healthBar { get; set; }

    [HideInInspector]
    public LayerMask playerLayer;
    [HideInInspector]
    public LayerMask enemyLayer;

    private void Awake()
    {
        TryGetComponent(out NavMeshAgent agent);
        navAgent = agent;

        StateMachine = new UnitStateMachine();
        IdleState = new UnitIdleState(this, StateMachine);
        ChaseState = new UnitChaseState(this, StateMachine);
        AttackState = new UnitAttackState(this, StateMachine);

        playerLayer = LayerMask.GetMask("PlayerLayer");
        enemyLayer = LayerMask.GetMask("EnemyLayer");
    }

    private void OnEnable()
    {
        if (GetUnitType() == UnitType.Building) return;

        foreach (AnimationState animState in Animation)
        {
            AnimationNames.Add(animState.name);
        }
    }

    private void Start()
    {
        CurrentHealth = UnitData.MaxHealth;

        if (GetUnitType() != UnitType.Building)
        {
            navAgent.speed = UnitData.MovementSpeed;
            _healthBar.UpdateHealthBar(UnitData.MaxHealth, CurrentHealth);
        }

        FindFavouriteTarget();

        StateMachine.Initialize(IdleState);

        tempPath = new NavMeshPath();
    }

    private void Update()
    {
        StateMachine.CurrentUnitState.UpdateState();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentUnitState.PhysicsUpdateState();
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (GetUnitType() != UnitType.Building)
        {
            _healthBar.UpdateHealthBar(UnitData.MaxHealth, CurrentHealth);
        }
        if (CurrentHealth <= 0 )
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void MoveUnit(Vector3 destination)
    {
        //RB.velocity = velocity;
        navAgent.SetDestination(destination);
        //RotateUnit(velocity);
    }

    public void StopUnit(bool stop)
    {
        navAgent.isStopped = stop;
    }

    public enum AnimationTriggerType
    {
        UnitAttack,
        UnitDamaged,
    }

    public UnitType GetUnitType()
    {
        return UnitData.Type;
    }

    public int GetUnitPowerPoints()
    {
        return UnitData.UnitPowerPoints;
    }

    public List<TargetPreference> GetTargetPreferenceList()
    {
        return UnitData.targetPreferences;
    }

    public void FindFavouriteTarget()
    {
        int maxValue = int.MinValue;
        foreach (var item in GetTargetPreferenceList())
        {
            if (item.weight > maxValue)
            {
                maxValue = item.weight;
                _favouriteTarget = item;
            }
        }
    }

    public UnitType GetFavouriteTarget()
    {
        return _favouriteTarget.unitType;
    }

    public int GetFavouriteTargetWeight()
    {
        return _favouriteTarget.weight;
    }

    public GameObject GetCurrentTargetObject()
    {
        return Target;
    }

    public Unit GetCurrentTargetUnit()
    {
        return CurrentTargetUnit;
    }

    public UnitType GetTypeOfOtherUnit(Unit unit)
    {
        return unit.UnitData.Type;
    }

    public Vector3 GetCurrentTargetPosition()
    {
        return Target.transform.position;
    }

    public UnitType GetCurrentTargetUnitType()
    {
        return CurrentTargetUnit.GetUnitType();
    }

    public int GetCurrentTargetWeight()
    {
        TargetPreference targetPreference = GetTargetPreferenceList().Find(T => T.unitType == GetCurrentTargetUnitType());

        return targetPreference != null ? targetPreference.weight : 0;
    }

    public bool IsNewTargetBetter(UnitType newTargetType)
    {
        if (newTargetType == GetFavouriteTarget() && GetCurrentTargetUnitType() != GetFavouriteTarget())
        {
            return true;
        }

        TargetPreference targetPreference = GetTargetPreferenceList().Find(T => T.unitType == newTargetType);

        return targetPreference.weight > GetCurrentTargetWeight() ? true : false;
    }

    public void SetAggroStatusAndTarget(bool isAggroed, Unit target)
    {
        if (IsTargetReachable(target))
        {
            //Debug.Log("Target reachable");
            this.isAggroed = isAggroed;
            Target = target.gameObject;
            CurrentTargetUnit = target;
            MoveUnit(GetValidPositionAroundTarget());
        }
        else
        {
            //Debug.Log("Target is not reachable");
            if (currentStateName == "Attack")
            {
                //Debug.Log("Target is not reachable attack");
                ClearAttackStatusAndTarget();
                StateMachine.ChangeState(IdleState);
            }
        }
    }

    public void ClearAggroStatus()
    {
        isAggroed = false;
    }   

    public void ClearAttackStatusAndTarget()
    {
        isInAttackingDistance = false;
        Target = null;
    }

    public void SetAttackingStatus(bool isInAttackingDistance)
    {
        this.isInAttackingDistance = isInAttackingDistance;
    }

    public Vector3 GetValidPositionAroundTarget()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
        randomDirection += GetCurrentTargetPosition();

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);

        return hit.position;
    }

    public bool IsTargetReachable(Unit unit)
    {
        if (GetTypeOfOtherUnit(unit) == UnitType.Building)
        {
            return navAgent.CalculatePath(unit.transform.position, tempPath);
        }
        navAgent.CalculatePath(unit.transform.position, tempPath);
        return  tempPath.status == NavMeshPathStatus.PathComplete;
    }

    public void LookForNewChaseTarget()
    {
        switch (UnitData.Side)
        {
            case UnitSide.Player:
                LookForNewChaseTargetInLayer(enemyLayer);
                break;
            case UnitSide.Enemy:
                LookForNewChaseTargetInLayer(playerLayer);
                break;
            default:
                break;
        }
    }

    private void LookForNewChaseTargetInLayer(LayerMask targetLayer)
    {
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, UnitData.DetectionRangeRadius, targetLayer);
        Unit bestTarget = null;
        int maxWeight = int.MinValue;

        foreach (Collider col in targetColliders)
        {
            if (col.TryGetComponent(out Unit possibleTarget))
            {
                if (possibleTarget.GetUnitType() == GetFavouriteTarget())
                {
                    SetAggroStatusAndTarget(true, possibleTarget);
                    return;
                }

                TargetPreference targetPreference = GetTargetPreferenceList().Find(T => T.unitType == possibleTarget.GetUnitType());
                if (targetPreference != null && targetPreference.weight > maxWeight)
                {
                    maxWeight = targetPreference.weight;
                    bestTarget = possibleTarget;
                }
            }
        }

        if (bestTarget != null)
        {
            SetAggroStatusAndTarget(true, bestTarget);
        }
        else
        {
            if (StateMachine.CurrentUnitState != IdleState)
            {
                StateMachine.ChangeState(IdleState);
            }
        }
    }

    public bool LookForNewAttackTarget()
    {
        switch (UnitData.Side)
        {
            case UnitSide.Player:
                return LookForNewAttackTargetInLayer(enemyLayer);
            case UnitSide.Enemy:
                return LookForNewAttackTargetInLayer(playerLayer);
            default:
                return false;
        }
    }

    private bool LookForNewAttackTargetInLayer(LayerMask targetLayer)
    {
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, UnitData.AttackRangeRadius, targetLayer);
        foreach (Collider col in targetColliders)
        {
            if (col.TryGetComponent(out Unit possibleTarget))
            {
                SetAggroStatusAndTarget(true, possibleTarget);
                SetAttackingStatus(true);
                return true;
            }
        }
        return false;
    }

    public virtual void DoAttack() { }
}