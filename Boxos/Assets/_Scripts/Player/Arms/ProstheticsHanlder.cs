using PurrNet;
using UnityEngine;

public class ProstheticsHanlder : NetworkIdentity
{
    [SerializeField] Arm _arm;

    [SerializeField] ProsthesisData _prosthesisData;

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }

    private void Start()
    {
        if (!isOwner)
            return;

        //if (_arm.side == ArmSide.Right)
        //    _prosthesisData = PlayerInventory.Instance.rightProsthesis;
        //else
        //    _prosthesisData = PlayerInventory.Instance.leftProsthesis;

        if (_prosthesisData == null)
            return;

        if (_prosthesisData.actionData is AttackData)
        {
            AttackData data = _prosthesisData.actionData as AttackData;
            _arm.stateMachine.attacks.Add(data.type, new AttackTruc(data.type, data));
            print("attack added");
        }
        else if (_prosthesisData.actionData is DefenseData)
        {
            DefenseData data = _prosthesisData.actionData as DefenseData;
            _arm.stateMachine.defenses.Add(data.type, new DefenseTruc(data.type, data));
            print("DefenseAdded");
        }

    }
}
