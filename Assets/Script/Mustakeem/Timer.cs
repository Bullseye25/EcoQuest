using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private float value;
    private bool stopped;

    public Timer(float startTime)
    {
        value = startTime;
        stopped = false;
    }

    public void Increment(float deltaTime)
    {
        if (!stopped)
        {
            value += deltaTime;
        }
    }

    public void Decrement(float deltaTime)
    {
        if (!stopped)
        {
            value -= deltaTime;
        }
    }

    public bool HasExpired()
    {
        return value <= 0f;
    }

    public bool HasReached(float targetValue)
    {
        return value >= targetValue;
    }

    public float GetValue()
    {
        return value;
    }

    public void Reset(float startTime)
    {
        value = startTime;
        stopped = false;
    }

    public void Stop()
    {
        stopped = true;
    }

    public bool IsStopped()
    {
        return stopped;
    }

}
