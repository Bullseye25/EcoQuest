using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOperator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private UnityEvent onEnter = new UnityEvent();
    [SerializeField] private UnityEvent onExit = new UnityEvent();

    private void OnTriggerEnter(Collider _target)
    {
        if (_target.transform == target)
            onEnter.Invoke();
    }

    private void OnTriggerExit(Collider _target)
    {
        if (_target.transform == target)
            onExit.Invoke();
    }
}
