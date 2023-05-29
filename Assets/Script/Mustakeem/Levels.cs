using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject l1;
    [SerializeField] private GameObject s1;
    [SerializeField] private GameObject s2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(L1S1());
    }

   private IEnumerator L1S1()
    {
        
          
            yield return new WaitForSeconds(2f); // Wait for 1 second
            l1.gameObject.SetActive(false);
            s1.gameObject.SetActive(false);
            s2.gameObject.SetActive(false);
       
    }


   
    public void NextStage()
    {
        StartCoroutine(L1S2());
       
        
    }
     private IEnumerator L1S2()
    {
        
          
           yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }



   
}
