
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Transform tempTarget;
    [SerializeField] private Transform finalTarget;

    private Vector3 position;
    [SerializeField] private bool challengeSuccess = false;
    [SerializeField] private bool behaviour = false;
    
    [Space]
    [SerializeField] private UnityEvent onSuccess = new UnityEvent();
    [Space]
    [SerializeField] private UnityEvent onFail = new UnityEvent();

    private void Start() { }
    IEnumerator OnActive()
    {
        position = transform.position;
        transform.position = new Vector3
            (position.x,

                challengeSuccess == false ? 
                tempTarget.position.y : 
                finalTarget.position.y, 
            
             position.z);

        yield return new WaitForSeconds(Time.deltaTime);
        if (behaviour == true && challengeSuccess == false)
        {
            StartCoroutine(OnActive());

            if (challengeSuccess == false)
                onFail.Invoke();
        }
        else
        {
            if (challengeSuccess == true)
            {
                onSuccess.Invoke();
            }
        }
    }

    //Call when the player have entered trial zone
    public void BarrierActive()
    {
        behaviour = true;
        StartCoroutine(OnActive());
    }

    //Call when the player have exit trial zone
    public void BarrierDeactive()
    {
        behaviour = false;
    }

    //Call when the player have completed the necessary task
    public void OnSuccess()
    {
        challengeSuccess = true;
    }

    public void OnFail()
    {
        challengeSuccess = false;
    }
}