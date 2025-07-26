using UnityEngine;

public class ArmAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] Arm _arm;

    public void Hit(int attackID)
    {
        _arm.Hit(attackID);
    }

    public void GuardBreak()
    {
        _arm.GuardBreak();
    }

    public void ParryWindow(bool state)
    {
        _arm.ParryWindow(state);
    }
}
