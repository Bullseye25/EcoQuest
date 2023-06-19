using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsHammerCollected : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private int currentAmount = 0;

    [Space]
    [SerializeField] private UnityEvent onSuccess = new UnityEvent();
    [Space]
    [SerializeField] private UnityEvent onFail = new UnityEvent();

    private void Start()
    {
        currentAmount = 0;
    }

    public void Proceed()
    {
        if (currentAmount == amount)
        {
            onSuccess.Invoke();
        }
        else
        {
            onFail.Invoke();
        }
    }

    public void CollectedItem()
    {
        currentAmount++;
    }
}
