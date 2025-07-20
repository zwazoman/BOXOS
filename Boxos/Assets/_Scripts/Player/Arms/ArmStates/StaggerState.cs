using UnityEngine;

public class StaggerState : ArmState
{
    public float duration;

    float timer;

    public override void OnEnter()
    {
        arm.animator.SetTrigger("Stagger");

        timer = 0;
        if (duration == 0)
            duration = 2;

    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
            stateMachine.Neutral();
    }


}
