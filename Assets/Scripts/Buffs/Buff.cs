using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public float duration { get; private set; }
    private float _remainingTime;

    private System.Action _applyEffect;
    private System.Action _removeEffect;

    public string type { get; private set; }

    public Buff(string Type, float Duration, System.Action ApplyEffect, System.Action RemoveEffect)
    {
        type = Type;
        duration = Duration;
        _remainingTime = Duration;
        _applyEffect = ApplyEffect;
        _removeEffect = RemoveEffect;
    }

    public bool UpdateBuff(float deltaTime)
    {
        _remainingTime -= deltaTime;
        return _remainingTime <= 0;
    }

    public void Apply()
    {
        _applyEffect?.Invoke();
    }
    
    public void Remove()
    {
        _removeEffect?.Invoke();
    }
}
