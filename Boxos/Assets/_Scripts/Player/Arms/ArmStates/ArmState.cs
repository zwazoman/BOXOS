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

    public void DamagingHit(Arm attackingArm, int hitID)
    {
        Debug.Log("HIT !");

        switch (hitID)
        {
            default:
                arm.player.UpdateHealth(-PlayerStats.LightAttackDamage);
                stateMachine.Stagger();
                break;
            case 1:
                arm.player.UpdateHealth(-PlayerStats.HeavyAttackDamage);
                stateMachine.Stagger();
                break;
        }
    }

}
