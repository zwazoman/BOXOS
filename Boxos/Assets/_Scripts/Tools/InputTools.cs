using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections.Generic;

public static class InputTools
{
    public static bool InputAngle(Vector2 angle, Vector2 vector, bool isEntering = true, float distanceToNeutral = PlayerStats.MaxDistanceToNeutral)
    {
        float dotValue = Vector2.Dot(angle, vector);

        if ((isEntering && (1 - dotValue <= PlayerStats.StickInputMargin && vector.magnitude >= distanceToNeutral)) || !isEntering && (1 - dotValue >= PlayerStats.StickInputMargin || vector.magnitude <= distanceToNeutral))
            return true;
        
        return false;
    }

    //public static bool InputAngleExit(Vector2 angle, Vector2 vector, float distanceToNeutral = PlayerStats.MinDistanceToNeutral)
    //{
    //    float dotValue = Vector2.Dot(angle, vector);

    //    if (1 - dotValue >= PlayerStats.StickInputMargin || vector.magnitude <= distanceToNeutral)
    //        return true;

    //    return false;
    //}
}



