using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 armInputDelta;

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

    public void FreeHit(Arm attackingArm, int hitID)
    {
        Debug.Log("HIT !");

        switch (hitID)
        {
            default:
                //arm.player.TakeDamage(PlayerStats.LightAttackDamage);
                stateMachine.Stagger();
                break;
            case 1:
                //arm.player.TakeDamage(PlayerStats.HeavyAttackDamage);
                stateMachine.Stagger();
                break;
        }
    }

}
