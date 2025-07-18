using UnityEngine;

public class AttackState : ArmState
{
    public int attackID;

    public override void OnEnter()
    {
        switch (attackID)
        {
            default:
                arm.animator.SetTrigger("LightAttack");
                break;
            case 1:
                arm.animator.SetTrigger("HeavyAttack");
                break;
        }

        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += FreeHit;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= FreeHit;
    }
}
