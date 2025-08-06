using UnityEngine;

public class CounterState : UtilitaryState
{
    bool _hasCountered;

    HitData _hitData;
    Arm _targetArm;

    public override void OnEnter()
    {
        type = ActionType.Counter;

        base.OnEnter();

        stateDuration = PlayerStats.CounterAttackWindowDuration;

        arm.OnReceiveHit += Counter;

        arm.OnHit += Hit;

        arm.animator.SetTrigger("Block");
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnReceiveHit -= Counter;
        arm.OnReceiveHit -= DamagingHit;

        arm.OnHit -= Hit;
    }

    public override void Update()
    {
        base.Update();

        if(!_hasCountered)
            HandleDurationBasedExit();
    }

    void Counter(Arm attackingArm, HitData hitData)
    {
        _hasCountered = true;

        _hitData = hitData;
        _targetArm = attackingArm;

        attackingArm.Blocked(GameManager.Instance.opponentId);

        arm.animator.SetTrigger("LightAttack");

        arm.OnReceiveHit += DamagingHit;
    }

    void Hit()
    {
        _targetArm.ReceiveHit(GameManager.Instance.opponentId, _targetArm, _hitData);
    }
}
