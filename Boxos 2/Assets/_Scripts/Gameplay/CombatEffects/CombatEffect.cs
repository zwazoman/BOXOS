using System;
using UnityEngine;

public abstract class CombatEffect : MonoBehaviour
{
    public event Action OnApplied;
    public event Action OnRemoved;

    protected bool isApplied = false;

    private void Start()
    {
        Apply();
    }

    public void Apply()
    {
        OnApplied?.Invoke();

        isApplied = true;
    }

    public void Remove()
    {
        OnRemoved?.Invoke();

        isApplied = false;
    }

    private void Update()
    {
        if (isApplied)
            EffectUpdate();
    }

    protected virtual void EffectUpdate()
    {

    }

    private void OnDestroy()
    {
        Remove();
    }

}
