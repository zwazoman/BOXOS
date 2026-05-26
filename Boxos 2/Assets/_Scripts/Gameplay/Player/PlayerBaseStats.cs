using System;
using System.Threading.Tasks.Sources;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStats", menuName = "Player/Stats")]
public class PlayerBaseStats : ScriptableObject
{
    [Header("GAMEPLAY")]

    [field: SerializeField] public int maxHealth { get; private set; } = 10;
    [field: SerializeField] public int maxHeat { get; private set; } = 20;

    [field: SerializeField] public float coolingStartTIme { get; private set; } = 2f;
    [field: SerializeField] public float coolingTimeBetweenTicks { get; private set; } = .6f;
    [field: SerializeField] public int coolingPerTick { get; private set; } = 1;

    [field: SerializeField] public float overheatDuration { get; private set; } = 2.5f;

    [Header("INPUTS")]

    [field: SerializeField] public float stickInputMargin { get; private set; } = .1f;
    [field: SerializeField] public float maxDistanceToNeutral { get; private set; } = .8f;
    [field: SerializeField] public float minDistanceToNeutral { get; private set; } = .5f;
    [field: SerializeField] public float inputExitTime { get; private set; } = .3f;

    [Header("MULTIPLYERS")]

    [field: SerializeField] public float inflictedDamagesMult { get; private set; } = 1;
    [field: SerializeField] public float receivedDamagesMult { get; private set; } = 1;
    [field: SerializeField] public float healthMult { get; private set; } = 1;
    [field: SerializeField] public float heatMult { get; private set; } = 1;
    [field: SerializeField] public float coolingMult { get; private set; } = 1;

    [field: SerializeField] public float stunDurationMult { get; private set; } = 1;

    // ACTION STATS

    //block
    //[field: SerializeField] public float blockWindowDuration { get; private set; } = 10f;

    ////recovery
    //[field: SerializeField] public float recoveryTimeOffset { get; private set; } = .5f;

    ////charged attack
    //[field: SerializeField] public float chargeAttackTimeOffset { get; private set; } = 2;

    ////antidefense attack
    //public float parriedDamageMultiplier = 2;
    //[field: SerializeField] public float parriedStaggerTimeMultiplier { get; private set; } = 1.5f;

    ////counter attack
    //[field: SerializeField] public float counterAttackWindowDuration { get; private set; } = 1.5f;

}

[Serializable]
public struct AttackStats
{
    public float speed;
    public int damages;
    public float StaggerDuration;
    public float blockedStaggerTime;
    public int blockedHeatCost;
    public float parriedStaggerTime;
    public int parriedHeatCost;
}

public struct HitData
{
    public int damage;
    public float staggerDuration;
    public int heatCost;
    public int blockHeatCost;

    public HitData(int damage = 0, float staggerDuration = 0, int heatCost = 0, int blockHeatCost = 0)
    {
        this.damage = damage;
        this.staggerDuration = staggerDuration;
        this.heatCost = heatCost;
        this.blockHeatCost = blockHeatCost;
    }
}

public enum AttackDefensePriority
{
    Attack,
    Defense,
    Both
}
