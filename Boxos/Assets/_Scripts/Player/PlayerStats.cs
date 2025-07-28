using System.Threading.Tasks.Sources;
using UnityEngine;

public static class PlayerStats
{
    //Gameplay Stats

    public const int MaxHealth = 10;
    public const int MaxHeat = 20;

    public const float HeatTimeToCool = 2f;
    public const float StaminaRegenDurationOffset = .6f;
    public const int StaminaRegenPerTick = 1;

    public const float LightAttackSpeed = 500;
    public const int LightAttackDamage = 1;
    public const int LightAttackHeatCost = 3;
    public const float LightAttackHitStaggerDuration = .3f;
    public const float BlockedLightAttackStaggerDuration = 1.5f;
    public const int ParriedLightAttackHeatCost = 8;
    public const float ParriedLightAttackStaggerDuration = 3;

    public const float HeavyAttackSpeed = 1000;
    public const int HeavyAttackDamage = 2;
    public const int HeavyAttackHeatCost = 5;
    public const float HeavyAttackHitStaggerDuration = .7f;
    public const float BlockedHeavyAttackStaggerDuration = 1.5f;
    public const int ParriedHeavyAttackHeatCost = 6;
    public const float ParriedHeavyAttackStaggerDuration = 1;

    public const float BlockWindowDuration = 2.5f;
    public const int LightAttackBlockHeatCost = 2;
    public const int HeavyAttackBlockHeatCost = 5;

    public const int BlockHeatCost = 2;

    public const int GuardBreakHeatCost = 4;

    public const float OverheatDuration = 2.5f;

    //InputStats

    public const float StickInputMargin = .1f;
    public const float MaxDistanceToNeutral = .8f;
    public const float MinDistanceToNeutral = .5f;
    public const float InputExitTime = .15f;

}

public struct AttackStats
{
    public float speed;
    public int damage;
    public float StaggerDuration;
    public float blockedStaggerTime;
    public float parriedStaggerTime;
    public bool isCancelable;

    public AttackStats(float speed, int damage, float opponentStaggerTime, float blockedStaggerTime, float parriedStaggerTime, bool isCancelable)
    {
        this.speed = speed;
        this.damage = damage;
        this.StaggerDuration = opponentStaggerTime;
        this.blockedStaggerTime = blockedStaggerTime;
        this.parriedStaggerTime = parriedStaggerTime;
        this.isCancelable = isCancelable;
    }
}
