using UnityEngine;

public static class PlayerStats
{
    //Gameplay Stats

    public const int MaxHealth = 10;
    public const int MaxStamina = 20;

    public const float StaminaStaggerDuration = 2;
    public const float StaminaRegenDurationOffset = 1;

    public const float LightAttackSpeed = 500;
    public const float LightAttackDamage = 1;
    public const int LightAttackStaminaCost = 1;
    public const int ParriedLightAttackStaminaCost = 8;
    public const float ParriedLightAttackStaggerDuration = 3;

    public const float HeavyAttackSpeed = 1000;
    public const float HeavyAttackDamage = 2;
    public const int HeavyAttackStaminaCost = 3;
    public const int ParriedHeavyAttackStaminaCost = 7;
    public const float ParriedHeavyAttackStaggerDuration = 1;

    public const float KickSpeed = 1200;
    public const int KickStaminaCost = 4;
    public const int MissedKickSelfDamage = 1;
    public const float MissedKickStaggerDuration = 2;

    public const int LightAttackBlockStaminaCost = 2;
    public const int HeavyAttackBlockStaminaCost = 5;
    public const int KickBlockStaminaCost = 8;

    public const float ParryDuration = .5f;
    public const int ParryStaminaCost = 2;
    public const int KickParryStaminaCost = 15;

    //InputStats

    public const float ArmInputMargin = 25;
    public const float MaxDistanceToNeutral = .8f;
    public const float MinDistanceToNeutral = .5f;

}
