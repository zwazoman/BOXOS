using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 armInputDelta;

    public float stateDuration;

    protected float exitTimer;

    public virtual void OnEnter()
    {
        exitTimer = 0;
    }

    public virtual void Update()
    {

    }

    public virtual void OnExit() { }

    protected void HandleDurationBasedExit()
    {
        exitTimer += Time.deltaTime;

        if (exitTimer >= stateDuration)
        {
            stateMachine.Neutral();
        }
    }

    public void DamagingHit(Arm attackingArm, AttackStats attackStats)
    {
        Debug.Log("OUCH");

        arm.player.UpdateHealth(-attackStats.damage);
        stateMachine.Stagger(attackStats.StaggerDuration);
    }

    protected void TransitionWithType(ActionType type)
    {
        stateMachine.TransitionTo(stateMachine.actionStatesByTypes[type]);
    }
}
