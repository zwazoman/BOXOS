using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 armInputDelta;

    public float stateDuration;

    protected float exitTimer;
    protected bool update = true;

    public virtual void OnEnter()
    {
        exitTimer = 0;
        update = true;
    }

    public virtual void Update()
    {

    }

    public virtual void OnExit() { }

    public void StopUpdate()
    {
        update = false;
    }

    protected void HandleDurationBasedExit()
    {
        exitTimer += Time.deltaTime;

        if (exitTimer >= stateDuration)
        {
            stateMachine.Neutral();
            StopUpdate();
        }
    }

    public void DamagingHit(Arm attackingArm, AttackStats attackStats)
    {
        Debug.Log("OUCH");

        arm.player.UpdateHealth(-attackStats.damage);
        stateMachine.Stagger(attackStats.StaggerDuration);
    }
}
