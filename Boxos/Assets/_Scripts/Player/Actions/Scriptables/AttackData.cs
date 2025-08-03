using UnityEngine;


[CreateAssetMenu(fileName = "NewAttack", menuName = "Action/Attack")]
public class AttackData : ActionData
{
    [field : SerializeField]
    public AttackStats stats;
}
