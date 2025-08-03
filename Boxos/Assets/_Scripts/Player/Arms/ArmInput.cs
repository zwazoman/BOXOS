using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ArmInput
{
    public event Action<ActionType> OnPerformed;

    public List<Vector2> directions = new();

    [HideInInspector] public int directionCpt = 0;
    [HideInInspector] public float endingTimer = 0;

    public void Reset()
    {
        directionCpt = 0;
        endingTimer = 0;
    }

    public void Perform(ActionType type)
    {
        Debug.Log("perform");
        OnPerformed?.Invoke(type);
    }
}
