using UnityEngine;

public static class InputTools
{
    public static float ArmInputValue(Vector2 stickPos)
    {
        return Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
    }

    public static bool CheckInputAngleEnter(float angle, Vector2 vector, float distanceToNeutral = PlayerStats.MaxDistanceToNeutral)
    {
        float value = ArmInputValue(vector);

        if (value > angle - PlayerStats.ArmInputMargin && value < angle + PlayerStats.ArmInputMargin && vector.magnitude >= distanceToNeutral)
            return true;
        return false;
    }

    public static bool CheckInputAngleExit(float angle, Vector2 vector, float distanceToNeutral = PlayerStats.MinDistanceToNeutral)
    {
        float value = ArmInputValue(vector);

        if ((value < angle - PlayerStats.ArmInputMargin || value > angle + PlayerStats.ArmInputMargin) || vector.magnitude <= distanceToNeutral)
            return true;
        return false;
    }
}



