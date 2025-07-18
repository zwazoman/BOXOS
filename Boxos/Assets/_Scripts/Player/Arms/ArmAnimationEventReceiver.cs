using UnityEngine;

public class ArmAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] Arm _arm;
    
    public void Hit(int attackID)
    {
        _arm.Hit(attackID);
    }
}
