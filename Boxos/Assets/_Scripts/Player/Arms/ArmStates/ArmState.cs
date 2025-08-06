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

    public void DamagingHit(Arm attackingArm, HitData hitData)
    {
        Debug.Log("OUCH");

        arm.player.UpdateHealth(-hitData.damage);

        if(hitData.staggerDuration > 0)
            stateMachine.Stagger(hitData.staggerDuration);
    }

    protected void TransitionWithType(ActionType type)
    {
        Debug.Log(type);
        stateMachine.TransitionTo(stateMachine.actionStatesByTypes[type]);
    }
}
