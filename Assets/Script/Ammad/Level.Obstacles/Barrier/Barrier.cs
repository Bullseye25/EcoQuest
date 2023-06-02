using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Transform tempTarget;

    [SerializeField] private Transform finalTarget;

    private Vector3 position;
    private bool challengeSuccess = false;
    private bool behaviour = false;

    IEnumerator OnActive()
    {
        position = transform.position;
        transform.position = new Vector3
            (position.x,

                challengeSuccess == false ? 
                tempTarget.position.y : 
                finalTarget.position.y, 
            
             position.z);

        if (behaviour == true)
            yield return null;
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