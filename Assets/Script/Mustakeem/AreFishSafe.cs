using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreFishSafe : MonoBehaviour
{
    
    [SerializeField] private UnityEvent onSuccess = new UnityEvent();
    [SerializeField] private UnityEvent onFail = new UnityEvent();

    public void CheckCondition()
    {
        if (Condition() == true)
        {
            onSuccess.Invoke();
        }
        else
        {
            onFail.Invoke();
        }
    }

    private bool Condition()
    {
        var seaHorses = FindObjectsOfType<FishBehaviour>();
        var result = false;

        foreach(FishBehaviour seaHorse in seaHorses)
        {
            if (seaHorse.enabled == false)
                result = true;
            else
                result = false;
        }

        return result;
    }
}
