using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ArmInput
{
    public const float baseEndingDuration = .1f;

    public event Action/*<ArmState>*/ OnPerformed;

    public ArmState state;

    public List<Vector2> directions = new();

    public int directionCpt = 0;
    public float endingTimer = 0;

    public void Reset()
    {
        directionCpt = 0;
        endingTimer = 0;
    }

    public void Perform()
    {
        OnPerformed?.Invoke(/*state*/);
    }
}
