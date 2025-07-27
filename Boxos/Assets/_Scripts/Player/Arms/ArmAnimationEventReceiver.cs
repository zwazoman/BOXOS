using UnityEngine;

public class ArmAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] Arm _arm;

    public void Hit(int attackID)
    {
        switch (attackID)
        {
            case 0:
                AttackStats lightAttackStats = new(
                    PlayerStats.LightAttackSpeed,
                    PlayerStats.LightAttackDamage,
                    PlayerStats.LightAttackHitStaggerDuration,
                    PlayerStats.BlockedLightAttackStaggerDuration,
                    PlayerStats.ParriedLightAttackStaggerDuration,
                    false
                    );
                _arm.Hit(lightAttackStats);
                break;
            case 1:
                AttackStats heavyAttackStats = new(
                    PlayerStats.LightAttackSpeed,
                    PlayerStats.LightAttackDamage,
                    PlayerStats.LightAttackHitStaggerDuration,
                    PlayerStats.BlockedLightAttackStaggerDuration,
                    PlayerStats.ParriedLightAttackStaggerDuration,
                    false
                    );
                _arm.Hit(heavyAttackStats);
                break;
        }
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

}
