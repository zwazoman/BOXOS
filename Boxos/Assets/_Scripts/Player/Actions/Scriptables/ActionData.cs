using UnityEngine;

public class ActionData : ScriptableObject
{
    [field : SerializeField]
    public ActionType type;

    [field : SerializeField]
    public ArmInput inputs;

    [field: SerializeField]
    public int heatCost;

    //[field: SerializeField]
    //public DurationType durationType;
}
