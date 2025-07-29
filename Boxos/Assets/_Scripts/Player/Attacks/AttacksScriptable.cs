using UnityEngine;


[CreateAssetMenu(fileName = "NewAttack", menuName = "Attack")]
public class AttacksScriptable : ScriptableObject
{
    [field: SerializeField]
    public AttackType attackType;

    [field : SerializeField]
    public AttackStats stats;
}
