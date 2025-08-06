using UnityEngine;

public class ChargedAttackState  : AttackState,ITickingState
{
    bool AttackLaunched;

    float timer = 0;
    int damage;
    

    public override void OnEnter()
    {
        type = ActionType.ChargedAttack;

        base.OnEnter();

        damage = stats.damages;

        arm.animator.SetTrigger("Exhaust"); // la préparation
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnAnimationEnd -= stateMachine.Neutral;
    }

    public override void Update()
    {
        base.Update();

        if (AttackLaunched)
            return;

        if(InputTools.InputAngle(Vector2.up, armInputDelta))
        {
            LaunchAttack();
        }

        if (InputTools.InputAngle(stateMachine.attacks[type].data.inputs.directions[stateMachine.attacks[type].data.inputs.directions.Count - 1], armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if(exitTimer > PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
            }
        }

        timer += Time.deltaTime;

        if (timer >= PlayerStats.ChargeAttackTimeOffset)
        {
            damage++;
            timer = 0;
        }

    }

    void LaunchAttack()
    {
        Debug.Log(damage);
        AttackLaunched = true;
        arm.animator.SetTrigger("LightAttack");
        arm.OnAnimationEnd += stateMachine.Neutral;
    }

    protected override void Hit()
    {
        hitData = new HitData(damage, stats.StaggerDuration);
        hitData.blockHeatCost = damage * 2;

        Arm targetArm = FightManager.Instance.opponent.GetOpposedArmBySide(arm.side);

        targetArm.ReceiveHit(FightManager.Instance.opponentId, arm, hitData);
    }
}
