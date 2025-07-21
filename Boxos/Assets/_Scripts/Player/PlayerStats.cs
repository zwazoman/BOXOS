using UnityEngine;

public static class PlayerStats
{
    //Gameplay Stats

    public const int MaxHealth = 10;
    public const int MaxStamina = 20;

    public const float StaminaStaggerDuration = 2;
    public const float StaminaRegenDurationOffset = 1;

    public const float LightAttackSpeed = 500;
    public const int LightAttackDamage = 1;
    public const int LightAttackStaminaCost = 1;
    public const int ParriedLightAttackStaminaCost = 8;
    public const float ParriedLightAttackStaggerDuration = 3;

    public const float HeavyAttackSpeed = 1000;
    public const int HeavyAttackDamage = 2;
    public const int HeavyAttackStaminaCost = 3;
    public const int ParriedHeavyAttackStaminaCost = 7;
    public const float ParriedHeavyAttackStaggerDuration = 1;

    public const float BlockWindowDuration = 2.5f;
    public const int LightAttackBlockStaminaCost = 2;
    public const int HeavyAttackBlockStaminaCost = 5;

    public const float ParryDuration = .5f;
    public const int ParryStaminaCost = 2;

    //InputStats

    public const float StickInputMargin = .1f;
    public const float MaxDistanceToNeutral = .8f;
    public const float MinDistanceToNeutral = .5f;
    public const float InputExitTime = .15f;

}
