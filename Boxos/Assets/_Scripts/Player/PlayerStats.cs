using System;
using System.Threading.Tasks.Sources;
using UnityEngine;

public static class PlayerStats
{
    //GAMEPLAY STATS

    public const int MaxHealth = 10;
    public const int MaxHeat = 20;

    public const float HeatTimeToCool = 2f;
    public const float CoolingDurationOffset = .6f;
    public const int CoolingPerTick = 1;

    public const float OverheatDuration = 2.5f;

    //INPUT STATS

    public const float StickInputMargin = .1f;
    public const float MaxDistanceToNeutral = .8f;
    public const float MinDistanceToNeutral = .5f;
    public const float InputExitTime = .3f;

    // ACTION STATS

    //block
    public const float BlockWindowDuration = /*2.5*/10f;

    //recovery
    public const float RecoveryTimeOffset = .5f;

    //charged attack
    public const float ChargeAttackTimeOffset = 2;

    //antidefense attack
    public const int ParriedDamageMultiplier = 2;
    public const float ParriedStaggerTimeMultiplier = 1.5f;

    //counter attack
    public const float CounterAttackWindowDuration = 1.5f;


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
