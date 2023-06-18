using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterToAppear : MonoBehaviour
{
    [SerializeField] private int counterLimit;
    [SerializeField] private UnityEvent onComplete = new UnityEvent();
    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
    }

    public void Execute()
    {
        if(currentIndex < counterLimit)
        {
            currentIndex++;
        }
        else
        {
            onComplete.Invoke();
        }
    }
}