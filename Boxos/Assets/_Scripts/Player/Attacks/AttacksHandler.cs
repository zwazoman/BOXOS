using PurrNet;
using System.Collections.Generic;
using UnityEngine;

public class AttacksHandler : MonoBehaviour
{
    [SerializeField] Arm _arm;

    public Dictionary<AttackType, AttackStats> attacks = new();

    [SerializeField] List<AttacksScriptable> attacksScriptables = new();

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }

    void Start()
    {
        foreach(AttacksScriptable truc in attacksScriptables)
        {
            attacks.Add(truc.attackType, truc.stats);
        }


    }


}
