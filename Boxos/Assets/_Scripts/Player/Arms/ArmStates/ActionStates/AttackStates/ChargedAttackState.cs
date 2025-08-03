using UnityEngine;

public class ChargedAttackState  : AttackState
{
    bool AttackLaunched = false;

    float chargeTimer = 0;
    Vector2 endingDirection;

    int damage = 1;
    float incrementOffset = 2;
    

    public override void OnEnter()
    {
        type = ActionType.ChargedAttack;

        base.OnEnter();

        endingDirection = stateMachine.attacks[type].data.inputs.directions[stateMachine.attacks[type].data.inputs.directions.Count - 1];

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

        if (InputTools.InputAngle(endingDirection, armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if(exitTimer > PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
            }
        }

        chargeTimer += Time.deltaTime;

        if (chargeTimer >= incrementOffset)
        {
            damage++;
            chargeTimer = 0;
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
        hitData = new HitData(damage, stats.Value.StaggerDuration);
        hitData.blockHeatCost = damage * 2;

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArmBySide(arm.side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }
}
