using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class InputTools
{
    public static bool InputAngleEnter(Vector2 angle, Vector2 vector, float distanceToNeutral = PlayerStats.MaxDistanceToNeutral)
    {
        float dotValue = Vector2.Dot(angle, vector);

        if (1 - dotValue <= PlayerStats.StickInputMargin && vector.magnitude >= distanceToNeutral)
            return true;
        
        return false;
    }

    public static bool InputAngleExit(Vector2 angle, Vector2 vector, float distanceToNeutral = PlayerStats.MinDistanceToNeutral)
    {
        float dotValue = Vector2.Dot(angle, vector);

        if (1 - dotValue >= PlayerStats.StickInputMargin || vector.magnitude <= distanceToNeutral)
            return true;

        return false;
    }

}



