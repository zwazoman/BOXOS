using UnityEngine;

public class ArmAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] Arm _arm;

    public void Hit()
    {
        _arm.Hit();
    }

    public void GuardBreak()
    {
        _arm.GuardBreak();
    }

    public void ParryWindow(int state)
    {
        _arm.ParryWindow(state == 1);
    }

    public void CancelWindow(int state)
    {
        _arm.CancelWindow(state == 1);
    }

    public void AnimationEnd()
    {
        _arm.AnimationEnd();
    }
}
