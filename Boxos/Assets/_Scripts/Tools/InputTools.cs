using UnityEngine;

public static class InputTools
{
    public static float StickRotationValue(Vector2 stickPos)
    {
        return Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
    }

    public static float DistanceToNeutral(Vector2 stickPos)
    {
        return Vector2.Distance(Vector2.zero, stickPos);
    }
}
