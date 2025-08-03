using UnityEngine;

public class ProstheticsHanlder : MonoBehaviour
{
    [SerializeField] Arm _arm;

    [SerializeField] ProsthesisData _prosthesisData;

    private void Awake()
    {
        if(_arm == null)
            TryGetComponent(out _arm);
    }

    private void Start()
    {
        if (_prosthesisData == null)
            return;

        if(_prosthesisData.actionData is AttackData)
        {
            AttackData data = _prosthesisData.actionData as AttackData;
            _arm.stateMachine.attacks.Add(data.type, new AttackTruc(data.type, data));
        }
        else if(_prosthesisData.actionData is DefenseData)
        {
            DefenseData data = _prosthesisData.actionData as DefenseData;
            _arm.stateMachine.defenses.Add(data.type, new DefenseTruc(data.type, data));
        }
            
    }
}
