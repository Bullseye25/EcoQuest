using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent onExecute = new UnityEvent();

    public void ExecuteWithDelay()
    {
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(delay);
            onExecute.Invoke();
        }

        StartCoroutine(Delay());
    }

    public void EmptyParent(Transform target)
    {
        target.SetParent(null);
    }
}
